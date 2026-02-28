namespace BimMarket.Application.Admin.Products;

public record ProductImageDto(string Id, string Url, string? ThumbnailUrl, int SortOrder, bool IsMain);

public record ProductDto(
    string Id,
    string CategoryId,
    string Name,
    string Slug,
    string? Description,
    string? Brand,
    double BasePrice,
    string Sku,
    bool IsActive,
    bool IsFeatured,
    List<ProductImageDto>? Images = null);
