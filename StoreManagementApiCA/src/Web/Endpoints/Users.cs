using StoreManagementApiCA.Domain.Identity;
namespace StoreManagementApiCA.Web.Endpoints;
using StoreManagementApiCA.Application.Common.Models;
using StoreManagementApiCA.Application.Users.Queries.GetUsersWithPagination;

public class Users : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetUsersWithPagination, "GetUsers")
            .MapIdentityApi<ApplicationUser>();

    }

    public async Task<PaginatedList<UserDto>> GetUsersWithPagination(ISender sender, [AsParameters] GetUsersWithPaginationQuery query)
    {
        return await sender.Send(query);
    }
}
