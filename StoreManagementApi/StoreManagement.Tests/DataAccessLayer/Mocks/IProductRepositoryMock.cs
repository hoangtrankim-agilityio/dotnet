using Microsoft.AspNetCore.Identity;
using StoreManagement.Data;
using StoreManagement.Core.Models;
using StoreManagement.Core.Repositories;
using StoreManagement.Tests.DataAccessLayer.ContextMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreManagement.Data.Repositories;

namespace StoreManagement.Tests.DataAccessLayer.Mocks
{
    public class IProductRepositoryMock
    {
        public static IProductRepository GetMock()
        {
            List<Product> lstProducts = GenerateTestData();
            ApiDbContext dbContextMock = DbContextMock.GetMock<Product, ApiDbContext>(lstProducts, x => x.Products);
            return new ProductRepository(dbContextMock);
        }

        private static List<Product> GenerateTestData()
        {
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
            return lstProducts;
        }
    }
}