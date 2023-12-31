using StoreManagementApiCA.Application.Common.Models;

namespace StoreManagementApiCA.Application.Users.Queries.GetUsersWithPagination;

public class UserDto
{
    public string? Id { get; set; }
    public string? UserName { get; init; }

    public string? Email { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<IdentityAppUser, UserDto>();
        }
    }
}
