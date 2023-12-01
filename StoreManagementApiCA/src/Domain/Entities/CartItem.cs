namespace StoreManagementApiCA.Domain.Entities;

public class CartItem : BaseAuditableEntity
{

    public int CartId { get; set; }
    public int ProductId { get; set; }
    public float Price { get; set; }
    public int Quantity { get; set; }
    public bool Active { get; set; }

    public Cart Cart { get; set; } = null!;
    public Product Product { get; set; } = null!;
}
