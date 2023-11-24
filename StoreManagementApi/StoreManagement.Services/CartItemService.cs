using StoreManagement.Core;
using StoreManagement.Core.Models;
using StoreManagement.Core.Services;

namespace StoreManagement.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartItemService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<CartItem> GetCartById(Guid id)
        {
            return await _unitOfWork.CartItems.GetCartItemByIdAsync(id);
        }

        public async Task<CartItem> CreateCartItem(CartItem cartItem)
        {
            await _unitOfWork.CartItems.AddAsync(cartItem);
            await _unitOfWork.CommitAsync();
            return cartItem;
        }

        public async Task DeleteCartItem(CartItem cartItem)
        {
            _unitOfWork.CartItems.Remove(cartItem);
            await _unitOfWork.CommitAsync();
        }
    }
}