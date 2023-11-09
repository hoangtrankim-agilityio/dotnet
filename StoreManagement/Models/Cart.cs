namespace StoreManagement.Models;
public class Cart : BaseEntity
{
    public CartStatus Status { get; set; }
    public string UserId { get; set; }
    public User? User { get; set; }
    public ICollection<CartItem> CartItems { get; } = new List<CartItem>();
}

public enum CartStatus
{
    NewCart = 1,
    Checkout = 2,
    Paid = 3,
    Complete = 4,
    Abandoned = 5
}