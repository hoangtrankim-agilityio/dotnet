using StoreManagementApiCA.Application.Common.Interfaces;

namespace StoreManagementApiCA.Application.CartItems.Commands.UpdateCartItem;

public record UpdateCartItemCommand : IRequest
{
    public int Id { get; init; }

    public float Price { get; set; }
    public bool Active { get; init; }
    public int Quantity { get; init; }
}

public class UpdateCartItemCommandHandler : IRequestHandler<UpdateCartItemCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCartItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.CartItems
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Price = request.Price;
        entity.Active = request.Active;
        entity.Quantity = request.Quantity;

        await _context.SaveChangesAsync(cancellationToken);

    }
}
