using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Application.DTOs.Product;
using Task.Application.Interfaces.UnitOfWork;
using Task.Core.Entities;

namespace Task.Application.Features.Products.Queries.GetAllProduct
{
    public class GetAllProductsQueryHandler:IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetAllProductsQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var readRepository = await unitOfWork.GetReadRepository<Product>().GetAllAsync();

            var productDtos = readRepository.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CreatedDate=p.CreatedDate,
            }).ToList();

            return productDtos;
        }
    }
}
