using System.Threading.Tasks;
using StoreManagement.Core;
using StoreManagement.Core.Repositories;
using StoreManagement.Data.Repositories;

namespace StoreManagement.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApiDbContext _context;
        private ProductRepository _productRepository;
        private OrderRepository _orderRepository;

        public UnitOfWork(ApiDbContext context)
        {
            this._context = context;
        }
        public IProductRepository Products => _productRepository = _productRepository ?? new ProductRepository(_context);
        public IOrderRepository Orders => _orderRepository = _orderRepository ?? new OrderRepository(_context);
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}