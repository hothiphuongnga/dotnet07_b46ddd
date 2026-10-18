using SalesDb.Domain.Common;

namespace SalesDb.Domain.Entities;
public class Product : BaseEntity<int> {
    public string ProductName { get; set; } = "";
    public decimal Price { get; set; }
    public int Stock { get; set; }
}