namespace StoreManagementApiCA.Domain.Entities;

public class Cart : BaseAuditableEntity
{
    public string? Name { get; set; }
    public CartStatus Status { get; set; } = CartStatus.NewCart;
    public string? UserId { get; set; }
    public IList<CartItem> CartItems { get; private set; } = new List<CartItem>();
}
