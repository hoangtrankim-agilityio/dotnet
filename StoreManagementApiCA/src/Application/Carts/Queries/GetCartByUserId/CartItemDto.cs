using StoreManagementApiCA.Domain.Entities;

namespace StoreManagementApiCA.Application.Carts.Queries.GetCartByUserId;

public class CartItemDto
{
    public int Id { get; set; }
    public float Price { get; set; }
    public bool Active { get; init; }
    public int Quantity { get; init; }
    public int CartId { get; set; }
    public int ProductId { get; set; }
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<CartItem, CartItemDto>();
        }
    }
}
