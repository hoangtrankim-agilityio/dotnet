using StoreManagementApiCA.Application.CartItems.Commands.CreateCart;
using StoreManagementApiCA.Application.CartItems.Commands.UpdateCartItem;

namespace StoreManagementApiCA.Web.Endpoints;

public class CartItems : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPut(UpdateCartItem, "{id}")
            .MapPost(CreateCartItem);
    }

    public async Task<int> CreateCartItem(ISender sender, CreateCartItemCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<IResult> UpdateCartItem(ISender sender, int id, UpdateCartItemCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }
}
