using StoreManagementApiCA.Application.Common.Interfaces;
using StoreManagementApiCA.Application.Common.Mappings;
using StoreManagementApiCA.Application.Common.Models;

namespace StoreManagementApiCA.Application.Orders.Queries.GetOrderByUserId;

public record GetOrderByUserIdQuery : IRequest<OrderDto>
{
    public string? UserId { get; init; }
}

public class GetOrderByUserIdQueryHandler : IRequestHandler<GetOrderByUserIdQuery, OrderDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetOrderByUserIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OrderDto> Handle(GetOrderByUserIdQuery request, CancellationToken cancellationToken)
    {
        if ( request.UserId == null) {
            throw new ArgumentNullException(request.UserId);
        }

        var order = await _context.Orders
            .Where(x => x.UserId == request.UserId)
            .Include(x => x.OrderItems)
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
            .FirstAsync();

        Guard.Against.NotFound(request.UserId, order);
        return order;
    }
}
