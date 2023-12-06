using StoreManagementApiCA.Application.Common.Security;
using StoreManagementApiCA.Application.Common.Exceptions;
using StoreManagementApiCA.Application.Orders.Commands.CreateOrder;
using StoreManagementApiCA.Application.Products.Commands.CreateProduct;
using StoreManagementApiCA.Domain.Entities;

namespace StoreManagementApiCA.Application.FunctionalTests.Orders.Commands;

using static Testing;

public class CreateOrderTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        await RunAsAdministratorAsync();

        var command = new CreateOrderCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateOrder()
    {
        await RunAsAdministratorAsync();

        var productId = await SendAsync(new CreateProductCommand
        {
            Title = "New Toy Car",
            Name = "Toy Toyota Car",
            Price = 100,
            Type = "Toy",
            Quantity = 100
        });

        var userId = await RunAsDefaultUserAsync();

        var command = new CreateOrderCommand
        {
            TotalPrice = 100,
            Discount = 0,
            Tax = 0,
            UserId = userId,
            ShippingAddress = "VN",
            OrderItems = new List<OrderItem>{
                new OrderItem {
                    ProductId = productId,
                    Price = 200,
                    Quantity = 2,
                    Discount = 0
                }
            }
        };

        var itemId = await SendAsync(command);

        var item = await FindAsync<Order>(itemId);
        item.Should().NotBeNull();
        item!.Id.Should().Be(itemId);
        item!.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
