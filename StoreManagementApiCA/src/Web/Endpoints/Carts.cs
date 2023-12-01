using StoreManagementApiCA.Application.Common.Models;
using StoreManagementApiCA.Application.Carts.Commands.CreateCart;
using StoreManagementApiCA.Application.Carts.Queries.GetCartByUserId;

namespace StoreManagementApiCA.Web.Endpoints;

public class Carts : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetCartByUserId, "GetCartByUserId")
            .MapPost(CreateCart);
    }

    public async Task<CartDto> GetCartByUserId(ISender sender, [AsParameters] GetCartWithUserIdQuery query)
    {
        return await sender.Send(query);
    }

    public async Task<int> CreateCart(ISender sender, CreateCartCommand command)
    {
        return await sender.Send(command);
    }
}
