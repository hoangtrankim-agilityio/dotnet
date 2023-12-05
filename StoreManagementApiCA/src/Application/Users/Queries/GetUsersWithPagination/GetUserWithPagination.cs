using StoreManagementApiCA.Application.Common.Mappings;
using StoreManagementApiCA.Application.Common.Models;
using StoreManagementApiCA.Application.Common.Interfaces;
using StoreManagementApiCA.Application.Common.Security;
using StoreManagementApiCA.Domain.Constants;

namespace StoreManagementApiCA.Application.Users.Queries.GetUsersWithPagination;

[Authorize(Roles = Roles.Administrator)]
public record GetUsersWithPaginationQuery : IRequest<List<UserDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetUsersWithPaginationQueryHandler : IRequestHandler<GetUsersWithPaginationQuery, List<UserDto>>
{
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    public GetUsersWithPaginationQueryHandler(IMapper mapper, IIdentityService identityService)
    {
        _mapper = mapper;
        _identityService = identityService;
    }

    public async Task<List<UserDto>> Handle(GetUsersWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var users = await _identityService.GetApplicationUsersAsync(request);
        var userResult = _mapper.Map<List<IdentityAppUser>, List<UserDto>>(users);
        return userResult;
    }
}
