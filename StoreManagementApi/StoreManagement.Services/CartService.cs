using StoreManagement.Core;
using StoreManagement.Core.Models;
using StoreManagement.Core.Services;

namespace StoreManagement.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<List<Cart>> GetCartsByUserId(string id)
        {
            return await _unitOfWork.Carts.GetCartsByUserIdAsync(id);
        }

        public async Task<Cart> GetCartById(Guid id)
        {
            return await _unitOfWork.Carts.GetCartByIdAsync(id);
        }

        public async Task<List<Cart>> GetCarts()
        {
            return await _unitOfWork.Carts.GetCartsAsync();
        }
    }
}