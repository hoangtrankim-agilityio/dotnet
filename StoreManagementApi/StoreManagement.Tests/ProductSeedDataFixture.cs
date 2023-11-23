using StoreManagement.Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using StoreManagement.Data;

namespace StoreManagement.Tests;
public class ProductSeedDataFixture : IDisposable
{
    public ApiDbContext ApiDbContext { get; private set; } = new ApiDbContext();

    public ProductSeedDataFixture()
    {
        var options = new DbContextOptionsBuilder<ApiDbContext>()
            .UseInMemoryDatabase("InMemoryStoreManagementTest")
            .Options;

        ApiDbContext = new ApiDbContext(options);
        ApiDbContext.Products.Add(new Product { Id = Guid.NewGuid(), Title = "Subaru Toy Car", Name = "Toy Car", Price = 40, Type = "Toy", Quantity = 100  });

        ApiDbContext.SaveChanges();
    }

    public void Dispose()
    {
        ApiDbContext.Dispose();
    }
}