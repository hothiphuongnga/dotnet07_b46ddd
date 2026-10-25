using SalesDb.Domain.Common;

namespace SalesDb.Domain.Entities;

public class ProductImage : BaseEntity<int>
{
    public int ProductId { get; private set; }
    public string ImageUrl { get; private set; } = string.Empty;
    public string StoredFileName { get; private set; } = string.Empty;
    public bool IsPrimary { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public Product Product { get; private set; } = null!;

    public ProductImage() { }
    public ProductImage(int productId, string imageUrl, string storedFileName, bool isPrimary)
    {
        ProductId = productId;
        ImageUrl = imageUrl;
        StoredFileName = storedFileName;
        IsPrimary = isPrimary;
        CreatedAt = DateTime.UtcNow;
    }
    // setPrimary
    internal void SetPrimary(bool isPrimary)
    {
        IsPrimary = isPrimary;
    }
    // usnet Primary Image
    internal void UnsetPrimary() => IsPrimary = false;


}