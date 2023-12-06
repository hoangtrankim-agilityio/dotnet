using StoreManagementApiCA.Application.Common.Interfaces;
using StoreManagementApiCA.Domain.Entities;
using StoreManagementApiCA.Domain.Enums;

namespace StoreManagementApiCA.Application.CartItems.Commands.CreateCartItem;

public record CreateCartItemCommand : IRequest<int>
{
    public float Price { get; set; }
    public bool Active { get; init; }
    public int Quantity { get; init; }
    public int CartId { get; set; }
    public int ProductId { get; set; }


}

public class CreateCartItemCommandHandler : IRequestHandler<CreateCartItemCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateCartItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateCartItemCommand request, CancellationToken cancellationToken)
    {
        var entity = new CartItem();

        entity.Quantity = request.Quantity;
        entity.Active = request.Active;
        entity.Price = request.Price;
        entity.CartId = request.CartId;
        entity.ProductId = request.ProductId;

        _context.CartItems.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
