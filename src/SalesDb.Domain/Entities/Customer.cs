using SalesDb.Domain.Common;

namespace SalesDb.Domain.Entities;

public class Customer : BaseEntity<int>
{
    public string Name { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}