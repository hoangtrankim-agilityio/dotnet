namespace StoreManagement.Models;
public class User : BaseEntity
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string DoB { get; set; }

    public Role Role { get; set; }

    public Cart? Cart { get; set; }
}

public enum Role
{
    Admin = 1,
    Customer = 2
}