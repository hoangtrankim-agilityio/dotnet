namespace StoreManagement.Core.Models;
public class Order : BaseEntity
{
    public float TotalPrice { get; set; }
    public float Discount { get; set; }
    public float Tax { get; set; }

    public float Shipping { get; set; }

    public string ShippingAddress { get; set; } = null!;

    public string UserId { get; set; }
    public OrderStatus Status { get; set; }
    public User? User { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; } = null!;
}

public enum OrderStatus
{
    NewOrder = 1,
    Checkout = 2,
    Paid = 3,
    Failed = 4,
    Shipped = 5,
    Delivered = 6,
    Returned = 7,
    Complete = 8
}