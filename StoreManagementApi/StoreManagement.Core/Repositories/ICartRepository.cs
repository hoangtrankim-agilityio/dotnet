using System.Collections.Generic;
using System.Threading.Tasks;
using StoreManagement.Core.Models;
using StoreManagement.Core.Filters;

namespace StoreManagement.Core.Repositories
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<List<Cart>> GetCartsByUserIdAsync(string id);
        Task<Cart> GetCartByIdAsync(Guid id);
        Task<List<Cart>> GetCartsAsync();
    }
}