using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Application.Interfaces.UnitOfWork;
using Task.Core.Entities;

namespace Task.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler:IRequestHandler<UpdateProductCommand,Guid>
    {
        private readonly IUnitOfWork unitOfWork;
        public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var writeRepository = await unitOfWork.GetReadRepository<Product>().GetByIdAsync(request.Id);

            if (writeRepository is null)
                throw new KeyNotFoundException($"Guncellenecek id bulunamadi.");

            writeRepository.Name = request.Name;
            writeRepository.Description = request.Description;
            writeRepository.Price = request.Price;  
            writeRepository.CreatedDate =writeRepository.CreatedDate;

            unitOfWork.GetWriteRepository<Product>().Update(writeRepository);
            await unitOfWork.SaveAsync(cancellationToken);

            //await _cache.RemoveAsync("productList", cancellationToken);
            return writeRepository.Id;
        }
    }
}
