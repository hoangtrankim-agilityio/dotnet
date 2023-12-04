using StoreManagementApiCA.Application.Common.Models;
using StoreManagementApiCA.Application.CartItems.Commands.CreateCart;
using StoreManagementApiCA.Application.Carts.Queries.GetCartByUserId;

namespace StoreManagementApiCA.Web.Endpoints;

public class CartItems : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            // .MapGet(GetCartByUserId, "GetCartByUserId")
            .MapPost(CreateCartItem);
    }

    // public async Task<CartDto> GetCartByUserId(ISender sender, [AsParameters] GetCartWithUserIdQuery query)
    // {
    //     return await sender.Send(query);
    // }

    public async Task<int> CreateCartItem(ISender sender, CreateCartItemCommand command)
    {
        return await sender.Send(command);
    }
}
