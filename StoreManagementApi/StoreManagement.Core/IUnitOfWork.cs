using System;
using System.Threading.Tasks;
using StoreManagement.Core.Repositories;

namespace StoreManagement.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        Task<int> CommitAsync();
    }
}