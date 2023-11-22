using System.Collections.Generic;
using System.Threading.Tasks;
using StoreManagement.Core.Models;
using StoreManagement.Core.Filters;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace StoreManagement.Core.Repositories
{
    public interface IUserRepository : IRepository<IdentityUser>
    {
        Task<List<IdentityUser>> GetUsersByFilterAsync(PaginationFilter filter);
    }
}