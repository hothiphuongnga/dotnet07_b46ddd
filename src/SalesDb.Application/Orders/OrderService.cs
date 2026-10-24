using AutoMapper;
using SalesDb.Application.Common;
using SalesDb.Domain.Entities;
using SalesDb.Domain.Interfaces;

namespace SalesDb.Application.Orders;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orders;
    private readonly IProductRepository _products;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;


    public OrderService(IOrderRepository orders, IProductRepository products, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _orders = orders;
        _products = products;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ApiResponse<OrderDto>> AddAsync(CreateOrderDto obj)
    {

        var order = new Order(obj.CustomerId, obj.EmployeeId, DateTime.Now);
        await _orders.AddAsync(order);
        await _unitOfWork.SaveChangesAsync();
        var res = _mapper.Map<OrderDto>(order);
        return ApiResponse<OrderDto>.Ok(res, "Order created successfully");
    }

    public Task<ApiResponse<OrderDto>> CancelAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<OrderDto>> ConfirmAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<List<OrderDto>>>? GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<ApiResponse<OrderDto?>> GetByIdAsync(int id)
    {
        var order = await _orders.GetByIdAsync(id);
        if (order == null)
        {
            return ApiResponse<OrderDto?>.Fail("Order not found");
        }
        return ApiResponse<OrderDto?>.Ok(Map(order));

    }

    public async Task<ApiResponse<OrderDto>> AddItemAsync(int orderId, AddOrderItemDto dto)
    {
        // lấy order ra 
        var order = await _orders.GetByIdAsync(orderId);
        if (order == null) return ApiResponse<OrderDto>.Fail("Order không tồn tại");
        // check product tôn tại

        order.AddItem(dto.ProductId, dto.UnitPrice, dto.Quantity);
        await _unitOfWork.SaveChangesAsync();
        return ApiResponse<OrderDto>.Ok(Map(order));


    }
    private static OrderDto Map(Order order) => new(
    order.Id,
    order.CustomerId,
    order.EmployeeID,
    order.OrderDate,
    order.TotalAmount ?? 0,
    order.Status,
    order.OrderDetails.Select(x => new OrderItemDto(
        x.ProductID,
        x.Quantity,
        x.Price)).ToArray());
}