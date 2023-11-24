using System.Collections.Generic;
using System.Threading.Tasks;
using StoreManagement.Core.Models;
using StoreManagement.Core.Filters;

namespace StoreManagement.Core.Services
{
    public interface ICartItemService
    {
        Task<CartItem> CreateCartItem(CartItem cartItem);
        Task<CartItem> GetCartById(Guid id);

        Task DeleteCartItem(CartItem cartItem);
    }
}