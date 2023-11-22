using System.Collections.Generic;
using System.Threading.Tasks;
using StoreManagement.Core.Models;
using StoreManagement.Core.Filters;

namespace StoreManagement.Core.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrdersByUserId(string id);
        Task<Order> GetOrderById(Guid id);
        Task<List<Order>> GetOrders();
        Task UpdateOrder(Guid id, OrderStatus status);
    }
}