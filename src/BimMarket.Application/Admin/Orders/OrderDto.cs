namespace BimMarket.Application.Admin.Orders;

public record OrderItemDto(
    string Id,
    string ProductId,
    string? ProductName,
    int Quantity,
    double UnitPrice,
    double TotalPrice);

public record OrderStatusHistoryDto(string Status, string CreatedAt, string? Notes);

public record OrderDto(
    string Id,
    string OrderNumber,
    string UserId,
    string BranchId,
    string Status,
    double SubTotal,
    double DiscountAmount,
    double Total,
    string PaymentMethod,
    string CreatedAt,
    string? CustomerName,
    string? BranchName);

public record OrderDetailDto(
    string Id,
    string OrderNumber,
    string Status,
    List<OrderItemDto> Items,
    double SubTotal,
    double DiscountAmount,
    double Total,
    string PaymentMethod,
    string CreatedAt,
    List<OrderStatusHistoryDto>? StatusHistory,
    string? CustomerName,
    string? DeliveryAddress);

public record UpdateOrderStatusRequest(string Status, string? Notes);
