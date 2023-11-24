using System;
using System.Threading.Tasks;
using StoreManagement.Core.Repositories;

namespace StoreManagement.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IOrderRepository Orders { get; }
        ICartRepository Carts { get; }

        ICartItemRepository CartItems { get; }
        Task<int> CommitAsync();
    }
}