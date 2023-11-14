using System.Collections.Generic;
using System.Threading.Tasks;
using StoreManagement.Core;
using StoreManagement.Core.Models;
using StoreManagement.Core.Services;

namespace StoreManagement.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Product> GetProductById(Guid id)
        {
            return await _unitOfWork.Products.GetProductByIdAsync(id);
        }
    }
}