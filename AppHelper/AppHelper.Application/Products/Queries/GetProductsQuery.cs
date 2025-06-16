using AppHelper.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace AppHelper.Application.Products.Queries;

public class GetProductsQuery : IRequest<IEnumerable<Product>>
{
}
