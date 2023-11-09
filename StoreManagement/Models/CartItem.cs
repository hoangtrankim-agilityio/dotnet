namespace StoreManagement.Models;
public class CartItem : BaseEntity
{
    public Guid CartId { get; set; }
    public Guid ProductId { get; set; }
    public float Price { get; set; }
    public int Quantity { get; set; }
    public bool Active { get; set; }

    public Cart? Cart { get; set; }
    public Product? Product { get; set; }
}