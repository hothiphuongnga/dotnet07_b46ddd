using SalesDb.Domain.Common;

namespace SalesDb.Domain.Entities;
public class Order : BaseEntity<int> {
    public int? CustomerId { get; set; }
    public DateTime? OrderDate { get; set; }
    public decimal? TotalAmount { get; set; }
    public int? EmployeeID { get; set; }
    public string Status { get; set; } = "";
    public Customer? Customer { get; set; }
    public Employee? Employee { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}