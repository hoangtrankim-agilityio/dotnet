using StoreManagement.Core.Models;
using StoreManagement.Data.Repositories;
using StoreManagement.Data;
using Moq;
using Moq.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace StoreManagement.Tests
{
    public class ProductRepositoryTest
    {
        // private ProductRepository _productRepository;
        // private IProductRepository productRepository;

        // public ProductRepositoryTest()
        // {

        // }

        // [Fact]
        public async Task GetProductById()
        {
            //Arrang
            List<Product> lstProducts = new();
            Random rand = new Random();
            for (int index = 1; index <= 10; index++)
            {
                lstProducts.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product-" + index,
                    Title = "Title-" + index,
                    Price = 20,
                    Quantity = 20 + index,
                    Type = "Toy"
                });
            }
            var myDbContextMock = new Mock<ApiDbContext>();
            myDbContextMock.Setup(x => x.Products).ReturnsDbSet(lstProducts);


            // Act
            var productRepository = new ProductRepository(myDbContextMock.Object);
            var product = await productRepository.GetProductByIdAsync(lstProducts[0].Id);

            // Assert
            Assert.Equal(product.Id, lstProducts[0].Id);
        }
    }
}