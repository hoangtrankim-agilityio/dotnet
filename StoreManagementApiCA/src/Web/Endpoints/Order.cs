using StoreManagementApiCA.Application.Common.Models;
using StoreManagementApiCA.Application.Orders.Commands.CreateOrder;
using StoreManagementApiCA.Application.Orders.Queries.GetOrderByUserId;
using StoreManagementApiCA.Application.Orders.Queries.GetOrder;

namespace StoreManagementApiCA.Web.Endpoints;

public class Orders : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetOrderByUserId, "GetOrderByUserId")
            .MapGet(GetOrder, "{id}")
            .MapPost(CreateOrder);
    }

    public async Task<OrderDto> GetOrderByUserId(ISender sender, [AsParameters] GetOrderByUserIdQuery query)
    {
        return await sender.Send(query);
    }

    public async Task<OrderDto> GetOrder(ISender sender, [AsParameters] GetOrderQuery query)
    {
        return await sender.Send(query);
    }
    public async Task<int> CreateOrder(ISender sender, CreateOrderCommand command)
    {
        return await sender.Send(command);
    }
}
