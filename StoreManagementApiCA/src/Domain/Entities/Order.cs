namespace StoreManagementApiCA.Domain.Entities;

public class Order : BaseAuditableEntity
{
    public float TotalPrice { get; set; }
    public float Discount { get; set; }
    public float Tax { get; set; }

    public float Shipping { get; set; }

    public string? ShippingAddress { get; set; }

    public int UserId { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.NewOrder;

    public IList<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();
}
