using StoreManagementApiCA.Application.Common.Interfaces;
using StoreManagementApiCA.Application.Common.Security;
using StoreManagementApiCA.Domain.Constants;

namespace StoreManagementApiCA.Application.Products.Commands.DeleteProduct;

[Authorize(Roles = Roles.Administrator)]
public record DeleteProductCommand(int Id) : IRequest;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Products
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.Products.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

}
