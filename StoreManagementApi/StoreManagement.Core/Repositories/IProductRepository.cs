using System.Collections.Generic;
using System.Threading.Tasks;
using StoreManagement.Core.Models;

namespace StoreManagement.Core.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetProductByIdAsync(Guid id);
    }
}