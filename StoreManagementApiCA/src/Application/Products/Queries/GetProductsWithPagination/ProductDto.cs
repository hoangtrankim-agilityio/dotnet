using StoreManagementApiCA.Domain.Entities;

namespace StoreManagementApiCA.Application.Products.Queries.GetProductsWithPagination;

public class ProductDto
{
    public int Id { get; set; }

    public string? Title { get; set; }
    public string? Type { get; set; }
    public string? Name { get; set; }
    public float Price { get; set; }
    public int Quantity { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}
