namespace SalesDb.Domain.Common;

// ID la PK
public abstract class BaseEntity<TKey>
{
    public TKey Id { get; set; } = default!;


    // Các thuộc tính chung khác có thể được thêm vào đây, ví dụ: CreatedDate, UpdatedDate, CreatedBy, UpdatedBy, IsDeleted v.v.
    // public string CreatedBy { get; set; } = null!;
    // public string UpdatedBy { get; set; } = null!;
}