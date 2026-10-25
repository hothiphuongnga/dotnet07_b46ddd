namespace SalesDb.Application.Products;

public class ProductDto
{
    public int Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public List<ProductImageDto> ProductImages { get; set; } = new();
    public ProductDto(){}
    public ProductDto(int id, string productName, decimal price, int stock)
    {
        Id = id;
        ProductName = productName;
        Price = price;
        Stock = stock;
    }
}

public class ProductCreateDto
{
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
}
public class ProductImageDto
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsPrimary { get; set; }
}

public record AddProductImageDto(string ImageUrl, string StoredFileName, bool IsPrimary);