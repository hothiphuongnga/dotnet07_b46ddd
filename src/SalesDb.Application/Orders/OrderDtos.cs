using SalesDb.Domain.Enums;

namespace SalesDb.Application.Orders;



public sealed record OrderDto( int Id,
    int? CustomerId,
    int? EmployeeId,
    DateTime? OrderDate,
    decimal TotalAmount,
    OrderStatus Status,
    IReadOnlyCollection<OrderItemDto> Items);


public sealed record OrderItemDto(
    int ProductId,
    int Quantity,
    decimal UnitPrice
);

public sealed record CreateOrderDto(int? CustomerId,
    int? EmployeeId);
    
public sealed record AddOrderItemDto(int ProductId, int Quantity, decimal UnitPrice);