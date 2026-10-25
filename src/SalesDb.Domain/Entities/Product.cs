using SalesDb.Domain.Common;

namespace SalesDb.Domain.Entities;
public class Product : BaseEntity<int> {
    // 
    private readonly List<ProductImage> _images = [];
    public string ProductName { get; set; } = "";
    public decimal Price { get; set; }
    public int Stock { get; set; }
    // ds hình
    public IReadOnlyCollection<ProductImage> ProductImages  => _images.AsReadOnly();
    // thêm hình ảnh
    public ProductImage AddImage(string imageUrl, string storedFileName, bool isPrimary)
    {
        // hình đầu sẽ là hình chính nếu isPrimary = true
        bool  shouldBePrimary = isPrimary || _images.Count == 0;
        if (shouldBePrimary)
        {
            // đặt tắt cả các hình của sp này về isPrimary = false
            foreach (var image in _images) image.UnsetPrimary();
        }
        var newImage = new ProductImage(Id, imageUrl, storedFileName, isPrimary);
        _images.Add(newImage);
        return newImage;
    }
    // clear Primary Image
    public void ClearPrimaryImage()
    {
        foreach (var image in _images) image.UnsetPrimary();
    }
    // set Primary Image
    // remove Image


}