namespace StoreManagement.Api.Resources;

public class UserResource
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public CartResource Cart { get; set; }

}
