using SalesDb.Domain.Common;

namespace SalesDb.Domain.Entities;
public class Employee : BaseEntity<int> {   // PK EmployeeID -> map HasColumnName("EmployeeID")
    public string Name { get; set; } = "";
    public string Role { get; set; } = "";
    public string? Phone { get; set; }
    public string? Password { get; set; }   // lưu hash
}