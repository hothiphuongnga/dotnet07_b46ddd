namespace SalesDb.Api.Controllers;



using Microsoft.AspNetCore.Mvc;
using SalesDb.Application.Common;
using SalesDb.Application.Orders;

[Route("api/[controller]")]
[ApiController]
public class OrderController(IOrderService _orders) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return ToActionResult(await _orders.GetByIdAsync(id));

    }

    [HttpPost("{id:int}/items")]
    public async Task<IActionResult> AddItem(int id, AddOrderItemDto dto)
    { 
        return ToActionResult(await _orders.AddItemAsync(id, dto));
    }
        
        // quantity âm , productId <= 0, price < 0 sẽ bị chặn ở OrderDetail constructor
        // order id không tồn tại
        // đơn shipping







    private ObjectResult ToActionResult<T>(ApiResponse<T> response) =>
           StatusCode(response.StatusCode, response);
}
