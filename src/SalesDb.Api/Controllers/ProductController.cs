namespace SalesDb.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using SalesDb.Application.Common;
using SalesDb.Application.Products;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {

        var res = await _productService.GetAllAsync();
        return ToActionResult(res);
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return ToActionResult(await _productService.GetWithImages(id));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductCreateDto dto)
    {
        return ToActionResult(await _productService.CreateAsync(dto));
    }



    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductCreateDto dto)
    {
        return ToActionResult(await _productService.UpdateAsync(id, dto));
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return ToActionResult(await _productService.DeleteAsync(id));
    }
    // THEEM HINH
    [HttpPost("{productId:int}/images")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> AddImage(int productId, IFormFile files, bool isPrimary)
    {
        // giowis han dung luong
        const long maxFileSize = 5 * 1024 * 1024; // 5MB
        // nhan loai gi 
        string[] allowedTypes = [".jpg", ".jpeg", ".png", ".webp"];
        // nhóm hình, nhóm file text, excel, 
        if (files.Length == 0 || files.Length > maxFileSize)
            return ToActionResult(ApiResponse<string>.Fail("File không hợp lệ hoặc quá lớn", 400));
        string type = Path.GetExtension(files.FileName).ToLowerInvariant();// lay phan mo rong cua file
        if (!allowedTypes.Contains(type))
            return ToActionResult(ApiResponse<string>.Fail("File không hợp lệ, chỉ chấp nhận các định dạng: .jpg, .jpeg, .png, .webp", 400));
        // luu file vao thu muc wwwroot
        string relativePath = Path.Combine("uploads", "products", productId.ToString()); // uploads/products/{productId}
        string absolutePath = Path.Combine("wwwroot", relativePath);
        if (!Directory.Exists(absolutePath))
            Directory.CreateDirectory(absolutePath);



        //Directory.GetCurrentDirectory() tạo foler đường dẫn
        // 25-10-2026-16-38-12-234-logo.png
        string storedFileName = $"{DateTime.Now:dd-MM-yyyy-HH-mm-ss-fff}-{files.FileName}";
        string imgUrl = $"uploads/products/{productId}/{storedFileName}";
        string physicalAdd = Path.Combine(absolutePath, storedFileName);
        // copy file to physical path
        await using (FileStream ds = System.IO.File.Create(physicalAdd))
        {
            await files.CopyToAsync(ds, HttpContext.RequestAborted);
        }
        // lưu dữ liệu vào cơ sở dữ liệu
        var addOnj = new AddProductImageDto(imgUrl, storedFileName, isPrimary);
        var res = await _productService.AddImageAsync(productId, addOnj);
        // thành công thì thôi, thâts bại thì xoá file trong wwwroot để không giữ file rác
        if (!res.Success && System.IO.File.Exists(physicalAdd))
        {
            System.IO.File.Delete(physicalAdd);
        }
        return ToActionResult(res);
    }








    private ObjectResult ToActionResult<T>(ApiResponse<T> response)
    {
        return StatusCode(response.StatusCode, response);
    }
}
