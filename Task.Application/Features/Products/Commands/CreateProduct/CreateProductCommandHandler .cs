using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Application.Interfaces.UnitOfWork;
using Task.Core.Entities;

namespace Task.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
            };

            await unitOfWork.GetWriteRepository<Product>().AddAsync(product);
            await unitOfWork.SaveAsync();

            // Ürün listesi cache'ini temizle
            //await _cache.RemoveAsync("productList", cancellationToken);

            return product.Id;
        }
    }
}
