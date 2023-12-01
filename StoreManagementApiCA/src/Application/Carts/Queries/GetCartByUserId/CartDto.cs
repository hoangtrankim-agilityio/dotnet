using StoreManagementApiCA.Domain.Entities;

namespace StoreManagementApiCA.Application.Carts.Queries.GetCartByUserId;

public class CartDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int CartStatus { get; init; }
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Cart, CartDto>();
        }
    }
}
