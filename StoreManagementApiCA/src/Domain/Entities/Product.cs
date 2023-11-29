namespace StoreManagementApiCA.Domain.Entities;

public class Product : BaseAuditableEntity
{
    public string Name { get; set; } = "";
    public string Title { get; set; } = "";
    public float Price { get; set; }
    public string Type { get; set; } = "";
    public int Quantity { get; set; }
}
