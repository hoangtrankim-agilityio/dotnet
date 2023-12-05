using StoreManagementApiCA.Application.Common.Interfaces;
using StoreManagementApiCA.Application.Common.Mappings;
using StoreManagementApiCA.Application.Common.Models;
using StoreManagementApiCA.Application.Orders.Queries.GetOrderByUserId;

namespace StoreManagementApiCA.Application.Orders.Queries.GetOrder;

public record GetOrderQuery : IRequest<OrderDto>
{
    public int Id { get; init; }
}

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetOrderQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OrderDto> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders
            .Where(x => x.Id == request.Id)
            .Include(x => x.OrderItems)
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
            .FirstAsync();

        Guard.Against.NotFound(request.Id, order);
        return order;
    }
}
