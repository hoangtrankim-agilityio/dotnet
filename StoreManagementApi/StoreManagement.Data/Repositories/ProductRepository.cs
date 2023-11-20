using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Core.Models;
using StoreManagement.Core.Repositories;
using StoreManagement.Core.Filters;

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

        public async Task<List<Product>> GetProductsByFilterAsync(PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await ApiDbContext.Products
                .OrderBy(c => c.Name)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            return pagedData;
        }

        private ApiDbContext ApiDbContext
        {
            get { return Context as ApiDbContext; }
        }
    }
}