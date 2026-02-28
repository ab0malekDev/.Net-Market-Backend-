using MediatR;

namespace BimMarket.Application.Admin.Categories.Queries;

public record GetCategoriesQuery(bool IncludeChildren = true) : IRequest<List<CategoryDto>>;
