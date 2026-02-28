using BimMarket.Application.Admin.Products;
using BimMarket.Application.Common.Abstractions;
using BimMarket.Application.Common.Models;
using MediatR;

namespace BimMarket.Application.Admin.Products.Queries;

public class GetProductsQueryHandler(IProductRepository repo) : IRequestHandler<GetProductsQuery, PagedResponse<ProductDto>>
{
    public Task<PagedResponse<ProductDto>> Handle(GetProductsQuery request, CancellationToken ct) =>
        repo.GetProductsAsync(request.Page, request.PageSize, request.CategoryId, request.Search, ct);
}

public class GetProductByIdQueryHandler(IProductRepository repo) : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    public Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken ct) =>
        Guid.TryParse(request.Id, out var guid)
            ? repo.GetByIdAsync(guid, ct)
            : Task.FromResult<ProductDto?>(null);
}
