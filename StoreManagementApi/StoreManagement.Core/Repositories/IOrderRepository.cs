using System.Collections.Generic;
using System.Threading.Tasks;
using StoreManagement.Core.Models;
using StoreManagement.Core.Filters;

namespace StoreManagement.Core.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<List<Order>> GetOrderByUserIdAsync(string id);
        Task<Order> GetOrderByIdAsync(Guid id);
        Task<List<Order>> GetOrdersAsync();
    }
}