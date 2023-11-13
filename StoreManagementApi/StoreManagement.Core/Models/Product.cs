namespace StoreManagement.Core.Models;
public class Product : BaseEntity
{
    public string Name { get; set; } = "";
    public string Title { get; set; } = "";
    public float Price { get; set; }
    public string Type { get; set; }
    public int Quantity { get; set; }
}