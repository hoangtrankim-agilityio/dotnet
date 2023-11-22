using StoreManagement.Core;
using StoreManagement.Core.Models;
using StoreManagement.Core.Services;

namespace StoreManagement.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<List<Order>> GetOrdersByUserId(string id)
        {
            return await _unitOfWork.Orders.GetOrderByUserIdAsync(id);
        }

        public async Task<Order> GetOrderById(Guid id)
        {
            return await _unitOfWork.Orders.GetOrderByIdAsync(id);
        }

        public async Task<List<Order>> GetOrders()
        {
            return await _unitOfWork.Orders.GetOrdersAsync();
        }

        public async Task UpdateOrder(Guid id, OrderStatus status)
        {
            var updateOrder = await _unitOfWork.Orders.GetByIdAsync(id);
            updateOrder.Status = status;
            await _unitOfWork.CommitAsync();
        }
    }
}