using Microsoft.AspNetCore.Identity;

namespace StoreManagement.Models;
public class User : IdentityUser
{
    public string Phone { get; set; }
    public string DoB { get; set; }

    public Cart? Cart { get; set; }
    public ICollection<Order>? Orders { get; set; }
}
