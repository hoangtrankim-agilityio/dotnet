using StoreManagementApiCA.Application.Common.Security;
using StoreManagementApiCA.Application.Common.Exceptions;
using StoreManagementApiCA.Application.CartItems.Commands.CreateCartItem;
using StoreManagementApiCA.Application.Carts.Commands.CreateCart;
using StoreManagementApiCA.Application.Products.Commands.CreateProduct;
using StoreManagementApiCA.Domain.Entities;

namespace StoreManagementApiCA.Application.FunctionalTests.CartItems.Commands;

using static Testing;

public class CreateCartItemTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        await RunAsDefaultUserAsync();

        var command = new CreateCartItemCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateCartItem()
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

        var cartId = await SendAsync(new CreateCartCommand
        {
            UserId = userId,
            Name = "Cart for test",
        });

        var command = new CreateCartItemCommand
        {
            CartId = cartId,
            Price = 100,
            Active = true,
            Quantity = 1,
            ProductId = productId
        };

        var itemId = await SendAsync(command);

        var item = await FindAsync<CartItem>(itemId);
        item.Should().NotBeNull();
        item!.Id.Should().Be(itemId);
        item!.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
