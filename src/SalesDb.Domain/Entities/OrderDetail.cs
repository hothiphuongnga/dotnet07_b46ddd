using SalesDb.Domain.Common;
using SalesDb.Domain.Exceptions;

namespace SalesDb.Domain.Entities;

public class OrderDetail : BaseEntity<int>
{
    public int OrderID { get; private set; }
    public int ProductID { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
    public Order Order { get; private set; } = null!;
    public Product Product { get; private set; } = null!;

    public OrderDetail(){}
    public OrderDetail(int productId, decimal price, int quantity)
    {
        if (quantity <= 0) throw new DomainException("Số lượng phải lớn hơn 0.");
        if (productId <= 0) throw new DomainException("Sản phẩm không hợp lệ.");
        if (price < 0) throw new DomainException("Đơn giá phải lớn hơn 0.");


        ProductID = productId;
        Price = price;
        Quantity = quantity;
    }

    // update Quantity = 0;
    // tang so luong
    internal void IncreaseQuantity(int quantity)
    {
        if (quantity <= 0) throw new DomainException("Số lượng phải lớn hơn 0.");
        Quantity += quantity;
    }
}

