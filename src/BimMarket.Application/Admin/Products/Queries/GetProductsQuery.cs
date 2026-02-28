using BimMarket.Application.Common.Models;
using MediatR;

namespace BimMarket.Application.Admin.Products.Queries;

public record GetProductsQuery(int Page = 1, int PageSize = 20, string? CategoryId = null, string? Search = null)
    : IRequest<PagedResponse<ProductDto>>;

public record GetProductByIdQuery(string Id) : IRequest<ProductDto?>;
