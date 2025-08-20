using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Application.DTOs.Product;

namespace Task.Application.Features.Products.Queries.GetAllProduct
{
    public class GetAllProductsQuery:IRequest<IEnumerable<ProductDto>>
    {
    }
}
