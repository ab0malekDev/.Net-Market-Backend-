namespace BimMarket.Application.Admin.Reviews;

public record ReviewDto(
    string Id,
    string ProductId,
    string? ProductName,
    string UserId,
    string? UserName,
    int Rating,
    string? Title,
    string? Comment,
    bool IsApproved,
    string CreatedAt);
