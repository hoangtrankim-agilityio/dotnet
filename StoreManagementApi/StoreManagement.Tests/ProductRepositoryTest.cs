using StoreManagement.Core.Models;
using StoreManagement.Data.Repositories;
using StoreManagement.Data;
using Moq;
using Moq.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace StoreManagement.Tests;

public class ProductRepositoryTest
{
    [Fact]
    public async Task GetProductById_WhenCalled_ReturnsProductAsync()
    {
        //Arrange
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
        var contextMock = new Mock<ApiDbContext>();
        contextMock.Setup<DbSet<Product>>(x => x.Products)
            .ReturnsDbSet(lstProducts);

        // Act
        var productRepository = new ProductRepository(contextMock.Object);
        var product = await productRepository.GetProductByIdAsync(lstProducts[0].Id);

        // Assert
        Assert.Equal(product.Id, lstProducts[0].Id);
    }
}