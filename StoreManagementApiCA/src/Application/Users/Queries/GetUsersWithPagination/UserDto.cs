using StoreManagementApiCA.Domain.Constants;
using StoreManagementApiCA.Domain.Entities;
using StoreManagementApiCA.Infrastructure.Identity;

namespace StoreManagementApiCA.Application.Users.Queries.GetUsersWithPagination;

public class UserDto
{
    public int Id { get; set; }
    public string Email { get; set; } = "";

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ApplicationUser, UserDto>();
        }
    }
}
