using System.Collections.Generic;
using System.Threading.Tasks;
using StoreManagement.Core.Models;
using StoreManagement.Core.Filters;

namespace StoreManagement.Core.Services
{
    public interface ICartService
    {
        Task<List<Cart>> GetCartsByUserId(string id);
        Task<Cart> GetCartById(Guid id);
        Task<List<Cart>> GetCarts();
    }
}