namespace BimMarket.Application.Admin.Categories;

public record CategoryDto(
    string Id,
    string? ParentId,
    string Name,
    string Slug,
    string? Description,
    string? ImageUrl,
    int SortOrder,
    bool IsActive,
    List<CategoryDto>? Children = null);
