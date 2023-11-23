using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Core.Models;
using StoreManagement.Core.Repositories;
using StoreManagement.Core.Filters;

namespace StoreManagement.Data.Repositories
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        public CartRepository(ApiDbContext context)
            : base(context)
        { }

        public async Task<List<Cart>> GetCartsByUserIdAsync(string UserId)
        {
            return await ApiDbContext.Carts.Include(o => o.CartItems).ThenInclude(c => c.Product).Where(order => order.User.Id == UserId).ToListAsync();
        }
        public async Task<Cart> GetCartByIdAsync(Guid id)
        {
            return await ApiDbContext.Carts.Where(u => u.Id == id).Include(o => o.CartItems).ThenInclude(c => c.Product).FirstOrDefaultAsync();
        }

        public async Task<List<Cart>> GetCartsAsync()
        {
            return await ApiDbContext.Carts.Include(o => o.CartItems).ToListAsync();
        }
        private ApiDbContext ApiDbContext
        {
            get { return Context as ApiDbContext; }
        }
    }
}