using System.Threading.Tasks;
using StoreManagement.Core;
using StoreManagement.Core.Repositories;
using StoreManagement.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using StoreManagement.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace StoreManagement.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApiDbContext _context;
        // private readonly IdentityDbContext _identityContext;
        private ProductRepository _productRepository;
        private OrderRepository _orderRepository;
        private CartRepository _cartRepository;
        private CartItemRepository _cartItemRepository;

        public UnitOfWork(ApiDbContext context)
        {
            this._context = context;
        }
        public IProductRepository Products => _productRepository = _productRepository ?? new ProductRepository(_context);
        public IOrderRepository Orders => _orderRepository = _orderRepository ?? new OrderRepository(_context);
        public ICartRepository Carts => _cartRepository = _cartRepository ?? new CartRepository(_context);

        public ICartItemRepository CartItems => _cartItemRepository = _cartItemRepository ?? new CartItemRepository(_context);
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