using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using StoreManagement.Core.Models;
using StoreManagement.Core.Filters;
using StoreManagement.Api.Wrappers;
using StoreManagement.Api.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using StoreManagement.Api.Resources;
using StoreManagement.Core.Services;

namespace StoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // private readonly ApiDbContext _context;
        private readonly IUserService _userService;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UsersController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IMapper mapper, IUserService userService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            this._mapper = mapper;
            this._userService = userService;
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var result = await _userService.RegisterAdmin(model);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<User>(status: "Error", message: "User creation failed! Please check user details and try again."));

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
            var result = await _userService.RegisterUser(model);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<User>(status: "Success", message: "User creation failed! Please check user details and try again." ));

            return Ok(new Response<User>(status: "Success", message: "User created successfully!" ));
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<User>> GetUser(string username)
        {
            var user = await _userService.GetUserByName(username);
            if (user == null)
            {
                return NotFound();
            }
            var userResources = _mapper.Map<User, UserResource>(user);
            return Ok(userResources);
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
            var users = await _userService.GetUsersByFilter(filter);

            var pagedData = _mapper.Map<List<User>, List<UserResource>>(users);
            var totalRecords = await userManager.Users.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<UserResource>(pagedData, validFilter, totalRecords);
            return Ok(pagedReponse);
        }
    }
}