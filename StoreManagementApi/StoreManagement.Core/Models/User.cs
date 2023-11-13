using Microsoft.AspNetCore.Identity;

namespace StoreManagement.Core.Models;
public class User : IdentityUser
{
    public string DoB { get; set; }

    public Cart? Cart { get; set; }
    public ICollection<Order>? Orders { get; set; }
}
