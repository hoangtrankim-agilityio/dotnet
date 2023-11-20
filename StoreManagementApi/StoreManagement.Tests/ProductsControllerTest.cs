using StoreManagement.Controllers;
using StoreManagement.Services;
using StoreManagement.Core.Models;
using StoreManagement.Core;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Data;
using StoreManagement.Core.Services;
using StoreManagement.Api.Filters;
using Moq;
using Moq.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoreManagement.Api.Wrappers;

namespace StoreManagement.Tests;

public class ProductsControllerTest
{

    [Fact]
    public async Task GetProducts_WhenCalled_ReturnsProductsListAsync()
    {
        // Arrange
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
        var unitOfWork = new UnitOfWork(contextMock.Object);
        var productService = new ProductService(unitOfWork);
        var mockLogger = new Mock<ILogger<ProductsController>>();
        ILogger<ProductsController> logger = mockLogger.Object;

        //Act
        ProductsController productController = new ProductsController(contextMock.Object, productService, logger);
        PaginationFilter filter = new PaginationFilter(1,2);
        var result = await productController.GetProduct(filter);

        //Assert
        var products = (result.Result as OkObjectResult).Value as PagedResponse<List<Product>>;
        Assert.NotNull(products);
        Assert.Equal(2, products.Data.Count());
    }

    [Fact]
    public async Task GetProductByIdService_WhenCalled_ReturnsProductAsync()
    {
        // Arrange
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
        var unitOfWork = new UnitOfWork(contextMock.Object);
        var productService = new ProductService(unitOfWork);
        var mockLogger = new Mock<ILogger<ProductsController>>();
        ILogger<ProductsController> logger = mockLogger.Object;

        //Act
        ProductsController productController = new ProductsController(contextMock.Object, productService, logger);
        var result = await productController.GetProduct(lstProducts[0].Id);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(result.Value.Id, lstProducts[0].Id);
    }

    [Fact]
    public async Task GetProductById_UnknownGuidPassed_ReturnsNotFoundResult()
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
        var unitOfWork = new UnitOfWork(contextMock.Object);
        var productService = new ProductService(unitOfWork);
        var mockLogger = new Mock<ILogger<ProductsController>>();
        ILogger<ProductsController> logger = mockLogger.Object;

        // Act
        ProductsController productController = new ProductsController(contextMock.Object, productService, logger);
        var result = await productController.GetProduct(Guid.NewGuid());

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task Add_ValidProductPassed_ReturnedProductHasCreatedItem()
    {
        //Arrange
        var testProduct = new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Test Name",
            Title = "test",
            Price = 20,
            Type = "test",
            Quantity = 100
        };
        var contextMock = new Mock<ApiDbContext>();
        var mockProduct = new Mock<DbSet<Product>>();
        contextMock.Setup(x=>x.Products).Returns(mockProduct.Object);
        var unitOfWork = new UnitOfWork(contextMock.Object);
        var productService = new ProductService(unitOfWork);
        var mockLogger = new Mock<ILogger<ProductsController>>();
        ILogger<ProductsController> logger = mockLogger.Object;

        // Act
        ProductsController productController = new ProductsController(contextMock.Object, productService, logger);
        var result = await productController.PostProduct(testProduct);
        var response = result.Result as CreatedAtActionResult;
        var item = response.Value as Product;

        // Assert
        Assert.Equal(testProduct.Name, item.Name);
    }
}