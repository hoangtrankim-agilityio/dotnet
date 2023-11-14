using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Core.Models;
using StoreManagement.Core.Repositories;

namespace StoreManagement.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApiDbContext context)
            : base(context)
        { }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await ApiDbContext.Products.SingleOrDefaultAsync(p => p.Id == id);
        }

        private ApiDbContext ApiDbContext
        {
            get { return Context as ApiDbContext; }
        }
    }
}