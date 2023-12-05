using StoreManagementApiCA.Application.Common.Interfaces;
using StoreManagementApiCA.Domain.Entities;

namespace StoreManagementApiCA.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand : IRequest<int>
{
    public float TotalPrice { get; set; }
    public float Discount { get; set; }
    public float Tax { get; set; }

    public float Shipping { get; set; }

    public string? ShippingAddress { get; set; }

    public string? UserId { get; set; }

    public IList<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

}

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateOrderCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var entity = new Order();

        entity.TotalPrice = request.TotalPrice;
        entity.Shipping = request.Shipping;
        entity.Tax = request.Tax;
        entity.Discount = request.Discount;
        entity.ShippingAddress = request.ShippingAddress;
        entity.OrderItems = request.OrderItems;
        entity.UserId = request.UserId;

        _context.Orders.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
