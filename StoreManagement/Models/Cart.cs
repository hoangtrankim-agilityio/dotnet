namespace StoreManagement.Models;
public class Cart : BaseEntity
{
    public CartStatus Status { get; set; }
    public Guid UserId { get; set; }
    // public User User { get; set; }
    public virtual ICollection<CartItem>? CartItems { get; set; }
}

public enum CartStatus
{
    NewCart = 1,
    Checkout = 2,
    Paid = 3,
    Complete = 4,
    Abandoned = 5
}