using StoreManagementApiCA.Application.Common.Interfaces;
using StoreManagementApiCA.Domain.Entities;

namespace StoreManagementApiCA.Application.Products.Commands.CreateProduct;

public record CreateProductCommand : IRequest<int>
{
    public string? Title { get; set; }
    public string? Name { get; set; }
    public float Price { get; set; }
    public string? Type { get; set; }
    public int Quantity { get; set; }
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = new Product();

        entity.Title = request.Title ?? "";
        entity.Name = request.Name ?? "";
        entity.Price = request.Price;
        entity.Type = request.Type ?? "";
        entity.Quantity = request.Quantity;

        _context.Products.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
