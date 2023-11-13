namespace StoreManagement.Core.Models;
public class OrderItem : BaseEntity
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public float Price { get; set; }
    public int Quantity { get; set; }
    public float Discount { get; set; }
    public Order? Order { get; set; }
    public Product? Product { get; set; }
}