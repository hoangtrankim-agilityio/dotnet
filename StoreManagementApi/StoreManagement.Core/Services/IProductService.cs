using System.Collections.Generic;
using System.Threading.Tasks;
using StoreManagement.Core.Models;

namespace StoreManagement.Core.Services
{
    public interface IProductService
    {
        Task<Product> GetProductById(Guid id);
    }
}