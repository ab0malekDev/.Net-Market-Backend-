using BimMarket.Application.Admin.Categories;
using BimMarket.Application.Common.Abstractions;
using MediatR;

namespace BimMarket.Application.Admin.Categories.Queries;

public class GetCategoriesQueryHandler(ICategoryRepository repo) : IRequestHandler<GetCategoriesQuery, List<CategoryDto>>
{
    public Task<List<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken ct) =>
        repo.GetCategoriesAsync(request.IncludeChildren, ct);
}
