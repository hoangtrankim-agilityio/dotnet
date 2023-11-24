using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Core.Models;
using StoreManagement.Core.Repositories;
using StoreManagement.Core.Filters;

namespace StoreManagement.Data.Repositories
{
    public class CartItemRepository : Repository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(ApiDbContext context)
            : base(context)
        { }

        public async Task<CartItem> GetCartItemByIdAsync(Guid id)
        {
            return await ApiDbContext.CartItems.Include(o => o.Product).SingleOrDefaultAsync(m => m.Id == id);
        }

        private ApiDbContext ApiDbContext
        {
            get { return Context as ApiDbContext; }
        }
    }
}