using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using StoreManagement.Data;
using StoreManagement.Models;
using StoreManagement.Filters;
using StoreManagement.Wrappers;
using StoreManagement.Helpers;
using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace StoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // private readonly ApiDbContext _context;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;

        public UsersController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<User>(status: "Error", message: "User already exists!"));

            User user = new User()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                Phone = model.Phone,
                DoB = model.DoB
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<User>(status: "Error", message: "User creation failed! Please check user details and try again."));

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await userManager.AddToRoleAsync(user, UserRoles.Admin);
            }

            return Ok(new Response<User>(status: "Success", message: "User created successfully!"));
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<User>( status: "Error", message: "User already exists!" ));

            User user = new User()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                Phone = model.Phone,
                DoB = model.DoB
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<User>(status: "Success", message: "User creation failed! Please check user details and try again." ));

            return Ok(new Response<User>(status: "Success", message: "User created successfully!" ));
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<User>> GetUser(string username)
        {
            // var user = await userManager.FindByNameAsync(username);
            var user = await userManager.Users.Include(e => e.Cart).FirstOrDefaultAsync(e => e.UserName == username);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

         // GET: api/Users
        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers([FromQuery] PaginationFilter filter)
        {
            if (userManager.Users == null)
            {
                return NotFound();
            }
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await userManager.Users
                .OrderBy(c => c.UserName)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            var totalRecords = await userManager.Users.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<User>(pagedData, validFilter, totalRecords);
            return Ok(pagedReponse);
        }
    }
}