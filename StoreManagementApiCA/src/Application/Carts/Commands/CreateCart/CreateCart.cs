using StoreManagementApiCA.Application.Common.Interfaces;
using StoreManagementApiCA.Domain.Entities;
using StoreManagementApiCA.Domain.Enums;

namespace StoreManagementApiCA.Application.Carts.Commands.CreateCart;

public record CreateCartCommand : IRequest<int>
{
    public string? Name { get; init; }
    public string? UserId { get; set; }
}

public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateCartCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        var entity = new Cart();

        entity.Name = request.Name;
        entity.UserId = request.UserId;

        _context.Carts.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
