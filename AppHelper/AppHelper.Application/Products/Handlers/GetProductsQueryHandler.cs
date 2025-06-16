using AppHelper.Application.Products.Queries;
using AppHelper.Domain.Entities;
using AppHelper.Domain.Interfaces;
using MediatR;

namespace AppHelper.Application.Products.Handlers;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<Product>> Handle(GetProductsQuery request,
        CancellationToken cancellationToken)
    {
        return await _productRepository.GetProductsAsync();
    }
}
