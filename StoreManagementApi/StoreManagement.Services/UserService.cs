using System.Collections.Generic;
using System.Threading.Tasks;
using StoreManagement.Core;
using StoreManagement.Core.Models;
using StoreManagement.Core.Services;
using StoreManagement.Core.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace StoreManagement.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }
        public async Task<List<User>> GetUsersByFilter(PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var users = await _userManager.Users
                .OrderBy(c => c.UserName)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            return users;
        }

        public async Task<User> GetUserByName(string name)
        {
            var user = await _userManager.Users.Include(e => e.Cart).Include(e => e.Orders).ThenInclude(i => i.OrderItems).FirstOrDefaultAsync(e => e.UserName == name);
            return user;
        }

        public async Task<IdentityResult> RegisterUser(RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return IdentityResult.Failed(new IdentityError[] {
                                                new IdentityError{
                                                    Code = "Error",
                                                    Description = "User already exists!"
                                                }
                                            });

            User user = new User()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                PhoneNumber = model.PhoneNumber,
                DoB = model.DoB
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            return result;
        }

        public async Task<IdentityResult> RegisterAdmin(RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return IdentityResult.Failed(new IdentityError[] {
                                                new IdentityError{
                                                    Code = "Error",
                                                    Description = "User already exists!"
                                                }
                                            });

            User user = new User()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                PhoneNumber = model.PhoneNumber,
                DoB = model.DoB
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            return result;
        }
    }
}