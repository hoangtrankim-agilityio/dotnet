using StoreManagementApiCA.Application.Carts.Queries.GetCartByUserId;
using StoreManagementApiCA.Application.Common.Interfaces;
using StoreManagementApiCA.Application.Common.Mappings;
using StoreManagementApiCA.Application.Common.Models;

namespace StoreManagementApiCA.Application.Carts.Queries.GetCartByUserId;

public record GetCartWithUserIdQuery : IRequest<CartDto>
{
    public string? UserId { get; init; }
}

public class GetCartWithUserIdQueryHandler : IRequestHandler<GetCartWithUserIdQuery, CartDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCartWithUserIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CartDto> Handle(GetCartWithUserIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Carts
            .Where(x => x.UserId == request.UserId)
            .Include(x => x.CartItems)
            .OrderBy(x => x.Name)
            .ProjectTo<CartDto>(_mapper.ConfigurationProvider)
            .FirstAsync();
    }
}
