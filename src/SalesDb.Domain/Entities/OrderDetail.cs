using SalesDb.Domain.Common;

namespace SalesDb.Domain.Entities;
public class OrderDetail : BaseEntity<int> {
    public int OrderID { get; set; }
    public int ProductID { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public Order Order { get; set; } = null!;
    public Product Product { get; set; } = null!;
}