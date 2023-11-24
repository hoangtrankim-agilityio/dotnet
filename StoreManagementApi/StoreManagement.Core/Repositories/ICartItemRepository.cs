using System.Collections.Generic;
using System.Threading.Tasks;
using StoreManagement.Core.Models;
using StoreManagement.Core.Filters;

namespace StoreManagement.Core.Repositories
{
    public interface ICartItemRepository : IRepository<CartItem>
    {
        Task<CartItem> GetCartItemByIdAsync(Guid id);
    }
}