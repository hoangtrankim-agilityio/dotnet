using StoreManagement.Core.Models;
namespace StoreManagement.Api.Resources;

public class CartResource
{
    public Guid Id { get; set; }
    public CartStatus Status { get; set; }

}