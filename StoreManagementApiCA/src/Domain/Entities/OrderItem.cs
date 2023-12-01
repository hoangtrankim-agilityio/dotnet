namespace StoreManagementApiCA.Domain.Entities;
public class OrderItem : BaseEntity
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public float Price { get; set; }
    public int Quantity { get; set; }
    public float Discount { get; set; }
    public Order Order { get; set; } = null!;
    public Product Product { get; set; } = null!;
}
