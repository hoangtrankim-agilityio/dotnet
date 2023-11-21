using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Core.Models;
using StoreManagement.Core.Repositories;
using StoreManagement.Core.Filters;

namespace StoreManagement.Data.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ApiDbContext context)
            : base(context)
        { }

        public async Task<List<Order>> GetOrderByUserIdAsync(string UserId)
        {
            return await ApiDbContext.Orders.Include(o => o.OrderItems).Where(order => order.User.Id == UserId).ToListAsync();
        }
        public async Task<Order> GetOrderByIdAsync(Guid id)
        {
            return await ApiDbContext.Orders.Where(u => u.Id == id).Include(o => o.OrderItems).FirstOrDefaultAsync();
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            return await ApiDbContext.Orders.Include(o => o.OrderItems).ToListAsync();
        }

        private ApiDbContext ApiDbContext
        {
            get { return Context as ApiDbContext; }
        }
    }
}