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
        return ToActionResult(await _productService.GetByIdAsync(id));
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

    private ObjectResult ToActionResult<T>(ApiResponse<T> response)
    {
        return StatusCode(response.StatusCode, response);
    }
}
