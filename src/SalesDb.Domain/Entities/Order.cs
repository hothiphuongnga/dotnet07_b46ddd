using SalesDb.Domain.Common;
using SalesDb.Domain.Enums;
using SalesDb.Domain.Exceptions;

// aregate root: Order là thực thể gốc của aggregate Order
namespace SalesDb.Domain.Entities;

public class Order : AggregateRoot<int>
{
    private readonly List<OrderDetail> _orderDetails = []; // ds nội bộ dùng
    public int? CustomerId { get; set; }
    public DateTime? OrderDate { get; set; }
    public decimal? TotalAmount { get; set; } // chỗ này cần lưu tổng tiền hay không , có thể tính từ các OrderDetail. 
    // nếu chỉ update order detail mà không biết đến order -> tổng tiền sai số
    public int? EmployeeID { get; set; }
    public OrderStatus Status { get; private set; } // nhận bất cứ trạng thái nào, ví dụ: Pending, Completed, Cancelled, hhhhh, sudhfjsdf, sfhsd . Enum
    public Customer? Customer { get; set; }
    public Employee? Employee { get; set; }
    // mã coupon 
    public IReadOnlyCollection<OrderDetail> OrderDetails => _orderDetails.AsReadOnly();
    //  _orderDetails ds chi tiết đơn àng thực sự , có thể crud
    // OrderDetails đửa ra chỉ để đọc, không thể trực tiếp thêm, xóa, sửa từ bên ngoài.

    // contructor
    public Order() { }
    public Order(int? customerId, int? employeeId, DateTime? orderDate)
    {
        CustomerId = customerId;
        EmployeeID = employeeId;
        OrderDate = orderDate ?? DateTime.Now;
        Status = OrderStatus.PENDING;
        TotalAmount = 0;
    }
    //nhìn theo góc CRUD Create -> Read -> Update -> Delete
    // nhìn theo góc nghiệp vụ 
    // createOrdể, AddItem, confirmOrder, cancelOrder, completeOrder, shipOrder ....

    // chỉ thêm sp khi  đơn đang pending
    public void IsPending()
    {
        if (Status != OrderStatus.PENDING)
        {
            throw new DomainException("Chỉ có thể thực hiện thao tác khi đơn đang pending");
        }
    }
    // tính tổng tiền
    private void RecalculateTotal() => TotalAmount = _orderDetails.Sum(x => x.Price * x.Quantity);
    // thêm chi tiết đơn
    public void AddItem(int productId, decimal unitPrice, int quantity)
    {
        IsPending(); // nếu đơn không pending
        // sp này có trong order hay chưa 
        // chưa có thì thêm mới 

        var exisItem = _orderDetails.SingleOrDefault(x => x.ProductID == productId);
        if (exisItem is null)
        {
            _orderDetails.Add(new OrderDetail(productId, unitPrice,quantity));
        }
        // có thì tăng số lượng 
        else
        {
            exisItem.IncreaseQuantity(quantity);
        }
        RecalculateTotal();
        // tinh tong tien sau khi them hoac cap nhat chi tiet don
    }
    // xóa chi tiết đơn RemoveItem
    public void RemoveItem(int productId)
    {
        IsPending(); // chỉ xóa khi đơn đang pending
        var item = _orderDetails.SingleOrDefault(x => x.ProductID == productId)
            ?? throw new DomainException("Sản phẩm không tồn tại trong đơn hàng.");
        _orderDetails.Remove(item);
        RecalculateTotal();
    }
    // xacs nhan don  đơn không được rỗng
    public void ConfirmOrder()
    {
        IsPending();
        if (!_orderDetails.Any())
        {
            throw new DomainException("Đơn hàng không được rỗng khi xác nhận.");
        }
        Status = OrderStatus.APPROVED; // đơn đã được xác nhận
    }
    // ship : ship đơn đã xác nhận 
    public void ShipOrder()
    {
        if (Status != OrderStatus.APPROVED)
        {
            throw new DomainException("Chỉ có thể ship đơn đã được xác nhận.");
        }
        Status = OrderStatus.SHIPPING; // đơn đã được ship
    }
    // hoàn thành
    public void CompleteOrder()
    {
        if (Status != OrderStatus.SHIPPING)
        {
            throw new DomainException("Chỉ có thể hoàn thành đơn đang trong trạng thái shipping.");
        }
        Status = OrderStatus.COMPLETED; // đơn đã hoàn thành
    }
    // huỷ: đơn đã hoàn thành -> không huỷ dc , đơn đã huỷ thì không huỷ nữa
    public void CancelOrder()
    {
        if (Status == OrderStatus.COMPLETED)
        {
            throw new DomainException("Không thể huỷ đơn đã hoàn thành.");
        }
        if (Status == OrderStatus.Cancelled)
        {
            throw new DomainException("Đơn đã bị huy truoc đó.");
        }
        Status = OrderStatus.Cancelled; // đơn đã bị huỷ
    }
    // ....

}