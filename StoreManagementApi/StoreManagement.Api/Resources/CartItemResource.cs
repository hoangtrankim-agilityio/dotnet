namespace StoreManagement.Api.Resources;

public class CartItemResource
{
    public Guid Id { get; set; }
    public float Price { get; set; }
    public int Quantity { get; set; }
    public bool Active { get; set; }
    public ProductResource? Product { get; set; }
}
