namespace StoreManagement.Models;
public class Order : BaseEntity
{
    public Guid UserId { get; set; }
    public float TotalPrice { get; set; }
    public float Discount { get; set; }
    public float Tax { get; set; }

    public float Shipping { get; set; }

    public string ShippingAddress { get; set; }

    public OrderStatus Status { get; set; }
    public User User { get; set; } = null!;
    public virtual ICollection<OrderItem> OrderItems { get; set; }
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