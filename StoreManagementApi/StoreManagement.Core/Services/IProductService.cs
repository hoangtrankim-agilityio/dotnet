using System.Collections.Generic;
using System.Threading.Tasks;
using StoreManagement.Core.Models;
using StoreManagement.Core.Filters;

namespace StoreManagement.Core.Services
{
    public interface IProductService
    {
        Task<Product> GetProductById(Guid id);
        Task<List<Product>> GetProductsByFilter(PaginationFilter filter);
    }
}