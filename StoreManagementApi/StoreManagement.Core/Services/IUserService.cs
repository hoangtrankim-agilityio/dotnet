using System.Collections.Generic;
using System.Threading.Tasks;
using StoreManagement.Core.Models;
using StoreManagement.Core.Filters;
using Microsoft.AspNetCore.Identity;

namespace StoreManagement.Core.Services
{
    public interface IUserService
    {
        Task<List<User>> GetUsersByFilter(PaginationFilter filter);
        Task<User> GetUserByName(string name);

        Task<IdentityResult> RegisterUser(RegisterModel model);
        Task<IdentityResult> RegisterAdmin(RegisterModel model);
    }
}