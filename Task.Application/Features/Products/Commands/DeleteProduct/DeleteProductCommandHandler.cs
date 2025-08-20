using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Application.Interfaces.UnitOfWork;
using Task.Core.Entities;

namespace Task.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await unitOfWork.GetReadRepository<Product>().GetByIdAsync(request.Id);
            if (product == null)
                return false;

            unitOfWork.GetWriteRepository<Product>().Delete(product);
            var result = await unitOfWork.SaveAsync(cancellationToken);

            return result > 0;
        }
    }
}
