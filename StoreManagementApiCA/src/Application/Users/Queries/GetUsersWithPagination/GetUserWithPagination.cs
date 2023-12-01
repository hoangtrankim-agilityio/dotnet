using Microsoft.AspNetCore.Identity;
using StoreManagementApiCA.Application.Common.Mappings;
using StoreManagementApiCA.Application.Common.Models;
using StoreManagementApiCA.Domain.Identity;
using StoreManagementApiCA.Application.Common.Security;
using StoreManagementApiCA.Domain.Constants;

namespace StoreManagementApiCA.Application.Users.Queries.GetUsersWithPagination;

[Authorize(Roles = Roles.Administrator)]
public record GetUsersWithPaginationQuery : IRequest<PaginatedList<UserDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetUsersWithPaginationQueryHandler : IRequestHandler<GetUsersWithPaginationQuery, PaginatedList<UserDto>>
{
    // private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    // private readonly IIdentityService _identityService;

    public GetUsersWithPaginationQueryHandler(IMapper mapper, UserManager<ApplicationUser> userManager)
    {
        // _context = context;
        _mapper = mapper;
        _userManager = userManager;
        // _identityService = identityService;
    }

    public async Task<PaginatedList<UserDto>> Handle(GetUsersWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var users = await _userManager.Users
                .OrderBy(c => c.UserName)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);

        return users;
    }
}
