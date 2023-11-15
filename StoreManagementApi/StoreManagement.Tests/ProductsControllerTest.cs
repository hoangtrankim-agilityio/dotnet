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
namespace StoreManagement.Tests;

public class ProductsControllerTest
{

    [Fact]
    public async Task GetProducts_WhenCalled_ReturnsProductsListAsync()
    {
        // Arrange
        // private readonly IProductService _productService;
        // private readonly ILogger<ProductsController> _logger;
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
        var products = (result.Result as OkObjectResult).Value as IEnumerable<Product>;
        Assert.NotNull(products);
        Assert.Equal(2, products.Count());
    }
}