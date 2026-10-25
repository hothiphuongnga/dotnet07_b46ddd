// phân biệt Iqueryable và IEnumrable/ List
// viết document cho swagger
// xư lý upload và lưu trữ file
namespace SalesDb.Api.Controllers;


using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesDb.Domain.Entities;
using SalesDb.Infrastructure.Persistence;
using Swashbuckle.AspNetCore.Annotations;


// gom thao tác hết trong file 
[Route("api/[controller]")]
[ApiController]
public class DemoController(AppDbContext db) : ControllerBase
{

    [HttpGet("products")]
    [SwaggerOperation(Summary = "Lấy 10 sản phẩm đầu tiên", Description = "Trả về danh sách 10 sản phẩm đầu tiên theo Id")]
    public async Task<IActionResult> GetTop100()
    {
        IQueryable<Product> query = db.Products.AsNoTracking().OrderBy(p => p.Id); // 
                                                                                   // IQueryable: chưa thực hiện truy vấn, có thể tiếp tục thêm điều kiện
                                                                                   // thêm điều kiện
        query = query.Take(10); // bổ sung query để lấy 100 bản ghi đầu tiên
        // select dữ liệu từ query, lấy ra Id , ProductNam, Price
        var res = await query.Select(p => new { p.Id, p.ProductName, p.Price }).ToListAsync();
        // khi chạy ToList, ToListAsync, Count,... thì truy vấn mới được thực hiện trên cơ sở dữ liệu
        return Ok(res);
    }
    // sản phẩm có giá tối thiểu
    [HttpGet("products-min")]
    public async Task<IActionResult> GetTop10MinPrice( [FromQuery, SwaggerParameter("Giá nhỏ nhất, phải lớn hơn hoặc bằng 0")]decimal minPrice)
    {
        IQueryable<Product> query = db.Products.AsNoTracking().Where(p => p.Price >= minPrice).Select(p => new Product()
        {
            Id = p.Id,
            ProductName = p.ProductName,
            Price = p.Price,
            Stock = p.Stock
        });

        // chưa có lấy dữ liệu từ DB, Where, orderby , select xây dựng exprestion tree
        // tiếp tục thực hiện truy vấn, ghép thêo DK,  ví dụ lấy 10 sản phẩm có giá tối thiểu
        query = query.Take(10); // ghép thêm dk
        var sql = query.ToQueryString(); // xem câu lệnh SQL được sinh ra bởi IQueryable
        Console.WriteLine("[QUERY] " + sql);
        var res = await query.ToListAsync();
        return Ok(res);
        // nhunwgx terminal oparation thuong gajp
        /*
        ToListAsync(), ToArrayAsync()
        FirstAsync(), FirstOrDefaultAsync()
        SingleAsync(), SingleOrDefaultAsync()
        CountAsync()
        AnyAsync(), AllAsync()
        */
    }

    [HttpGet("execution-examples")]
  
    public async Task<ActionResult<QueryExecutionLessonDto>> GetExecutionExamples()
    {
       
       var inStockQuery = db.Products
            .AsNoTracking()
            .Where(product => product.Stock > 0); // chuaw chay query


        int count = await inStockQuery.CountAsync();    // dem xem co bn => chayj quer    y select COunt(*) from .....     
        bool any = await inStockQuery.AnyAsync();                  // kiểm tra có bản ghi nào thỏa điều kiện => chay query SELECT CASE WHEN EXISTS ...
        string? firstName = await inStockQuery
            .OrderBy(product => product.Id)
            .Select(product => product.ProductName)
            .FirstOrDefaultAsync(); // laasy ra ten sp dau tien

        return Ok(new QueryExecutionLessonDto(count, any, firstName));
    }

    //  A là tập hợp ID khách hàng có email like ('noidung')
    // B là tập hợp đơn hàng của những khách hàng trong A
    // api nhận noidung 
    // thực thi 1 lần duy nhất
    [HttpGet("order-by-email")]
    [SwaggerOperation(Summary = "Lấy đơn hàng theo email", Description = "Trả về danh sách đơn hàng của những khách hàng có email chứa nội dung cho trước")]
    public async Task<IActionResult> GetOrdersByEmail([FromQuery, SwaggerParameter("Nội dung email cần tìm")]string noidung)
    {
        // lấy ds id khách 
        var customerIds = db.Customers.Where(c => c.Email.Contains(noidung)).Select(c => c.Id); // 
        // lấy ra ds order từ id khach hàng
        var orders = db.Orders.Where(o => o.CustomerId.HasValue && customerIds.Contains(o.CustomerId.Value));
        // select * from orders wwhere customerId in (....)
        var query = orders.ToQueryString(); // xem câu lệnh SQL được sinh ra bởi IQueryable
        Console.WriteLine("[QUERY] " + query);
        // thực thi
        var result = await orders.ToListAsync();


        return Ok(result);
    }


// Query 1: lấy toàn bộ ID về RAM trước.
// List<int> customerIds = await db.Customers
//     .Where(/* ... */)
//     .Select(x => x.Id)
//     .ToListAsync(); // thuc thi

// // Query 2: gửi danh sách ID trở lại database.
// List<Order> orders = await db.Orders
//     .Where(x => customerIds.Contains(x.CustomerId!.Value))
//     .ToListAsync(); // thuc thi query lan 2





    public record QueryExecutionLessonDto(int Count, bool Any, string? FirstProductName);
}



