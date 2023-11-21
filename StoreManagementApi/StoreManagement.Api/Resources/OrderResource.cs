namespace StoreManagement.Api.Resources;
using StoreManagement.Core.Models;

public class OrderResource
{
    public Guid Id { get; set; }
    public float TotalPrice { get; set; }
    public float Discount { get; set; }
    public float Tax { get; set; }
    public float Shipping { get; set; }
    public string ShippingAddress { get; set; }
    public string UserId { get; set; }
    public OrderStatus Status { get; set; }
    public virtual ICollection<OrderItemResource> OrderItems { get; set; }
}
