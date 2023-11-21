namespace StoreManagement.Api.Resources;

public class ProductResource
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public float Price { get; set; }
    public int Quantity { get; set; }

}
