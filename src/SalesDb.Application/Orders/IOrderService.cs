namespace SalesDb.Application.Orders;

using SalesDb.Application.Common;
using SalesDb.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IOrderService
{
    Task<ApiResponse<OrderDto>> AddAsync(CreateOrderDto order);
    Task<ApiResponse<List<OrderDto>>>? GetAllAsync();
    Task<ApiResponse<OrderDto?>> GetByIdAsync(int id);
    Task<ApiResponse<OrderDto>> ConfirmAsync(int id);
    Task<ApiResponse<OrderDto>> CancelAsync(int id);

    Task<ApiResponse<OrderDto>> AddItemAsync(int orderId, AddOrderItemDto dto);



}