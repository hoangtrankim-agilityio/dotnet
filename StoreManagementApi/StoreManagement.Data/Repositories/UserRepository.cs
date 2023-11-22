using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Core.Models;
using StoreManagement.Core.Repositories;
using StoreManagement.Core.Filters;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace StoreManagement.Data.Repositories
{
    public class UserRepository : Repository<IdentityUser>, IUserRepository
    {
        public UserRepository(IdentityDbContext context)
            : base(context)
        { }

        public async Task<List<IdentityUser>> GetUsersByFilterAsync(PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var users = await IdentityDbContext.Users
                .OrderBy(c => c.UserName)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            return users;
        }

        private IdentityDbContext IdentityDbContext
        {
            get { return Context as IdentityDbContext; }
        }
    }
}