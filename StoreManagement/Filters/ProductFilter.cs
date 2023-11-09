namespace StoreManagement.Filters;
public class ProductFilter
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public ProductFilter()
    {
        this.PageNumber = 1;
        this.PageSize = 10;
    }
    public ProductFilter(int pageNumber, int pageSize)
    {
        this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
        this.PageSize = pageSize > 10 ? 10 : pageSize;
    }
}