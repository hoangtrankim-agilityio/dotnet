using StoreManagementApiCA.Application.Common.Interfaces;
using StoreManagementApiCA.Application.Common.Security;
using StoreManagementApiCA.Domain.Entities;
using StoreManagementApiCA.Domain.Constants;

namespace StoreManagementApiCA.Application.Products.Commands.UpdateTodoItem;

[Authorize(Roles = Roles.Administrator)]
public record UpdateProductCommand : IRequest
{
    public int Id { get; set; }
    public string? Title { get; init; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public float Price { get; set; }
    public int Quantity { get; set; }

}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Products
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Title = request.Title ?? "";
        entity.Type = request.Type ?? "";
        entity.Name = request.Name ?? "";
        entity.Price = request.Price;
        entity.Quantity = request.Quantity;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
