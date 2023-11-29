using StoreManagementApiCA.Application.Common.Models;
using StoreManagementApiCA.Application.Products.Commands.CreateProduct;

namespace StoreManagementApiCA.Web.Endpoints;

public class Products : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            // .RequireAuthorization()
            .MapPost(CreateProduct);
    }

    public async Task<int> CreateProduct(ISender sender, CreateProductCommand command)
    {
        return await sender.Send(command);
    }
}
