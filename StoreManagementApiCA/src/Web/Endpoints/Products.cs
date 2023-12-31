using StoreManagementApiCA.Application.Common.Models;
using StoreManagementApiCA.Application.Products.Commands.CreateProduct;
using StoreManagementApiCA.Application.Products.Commands.DeleteProduct;
using StoreManagementApiCA.Application.Products.Commands.UpdateProduct;
using StoreManagementApiCA.Application.Products.Queries.GetProductsWithPagination;

namespace StoreManagementApiCA.Web.Endpoints;

public class Products : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetProductsWithPagination)
            .MapPost(CreateProduct)
            .MapPut(UpdateProduct, "{id}")
            .MapDelete(DeleteProduct, "{id}");
    }

    public async Task<PaginatedList<ProductDto>> GetProductsWithPagination(ISender sender, [AsParameters] GetProductsWithPaginationQuery query)
    {
        return await sender.Send(query);
    }

    public async Task<int> CreateProduct(ISender sender, CreateProductCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<IResult> DeleteProduct(ISender sender, int id)
    {
        await sender.Send(new DeleteProductCommand(id));
        return Results.NoContent();
    }

    public async Task<IResult> UpdateProduct(ISender sender, int id, UpdateProductCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }
}
