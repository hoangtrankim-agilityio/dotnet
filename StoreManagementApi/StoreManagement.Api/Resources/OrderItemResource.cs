namespace StoreManagement.Api.Resources;

public class OrderItemResource
{
    public Guid Id { get; set; }
    public float Price { get; set; }
    public int Quantity { get; set; }
    public float Discount { get; set; }
}
