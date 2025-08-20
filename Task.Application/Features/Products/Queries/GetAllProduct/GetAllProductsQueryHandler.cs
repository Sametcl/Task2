using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Task.Application.DTOs.Product;
using Task.Application.Interfaces.UnitOfWork;
using Task.Core.Entities;

namespace Task.Application.Features.Products.Queries.GetAllProduct
{
    public class GetAllProductsQueryHandler:IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IDistributedCache cache;
        private const string CacheKey = "productList";
        public GetAllProductsQueryHandler(IUnitOfWork unitOfWork, IDistributedCache cache)
        {
            this.unitOfWork = unitOfWork;
            this.cache = cache;

        }

        public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            string? cachedProductsJson = await cache.GetStringAsync(CacheKey, cancellationToken);

            if (!string.IsNullOrEmpty(cachedProductsJson))
            {
               
                var cachedProducts = JsonSerializer.Deserialize<List<ProductDto>>(cachedProductsJson);
                return cachedProducts!;
            }

            var productsFromDb = await unitOfWork.GetReadRepository<Product>().GetAllAsync();

            var productDtos = productsFromDb.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CreatedDate = p.CreatedDate,
            }).ToList();

            var options = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(5)) 
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(30)); 

            string productsToCacheJson = JsonSerializer.Serialize(productDtos);
            await cache.SetStringAsync(CacheKey, productsToCacheJson, options, cancellationToken);

            return productDtos;
        }
    }
}
