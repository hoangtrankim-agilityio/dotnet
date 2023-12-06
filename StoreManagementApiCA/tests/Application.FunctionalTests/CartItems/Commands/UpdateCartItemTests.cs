using StoreManagementApiCA.Application.CartItems.Commands.CreateCartItem;
using StoreManagementApiCA.Application.CartItems.Commands.UpdateCartItem;
using StoreManagementApiCA.Application.Carts.Commands.CreateCart;
using StoreManagementApiCA.Application.Products.Commands.CreateProduct;
using StoreManagementApiCA.Domain.Entities;

namespace StoreManagementApiCA.Application.FunctionalTests.CartItems.Commands;

using static Testing;

public class UpdateCartItemTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidCartItemId()
    {
        await RunAsAdministratorAsync();
        var command = new UpdateCartItemCommand {
            Id = 99,
            Price = 100,
            Active = true,
            Quantity = 1
        };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldUpdateCartItem()
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

        var itemId = await SendAsync(new CreateCartItemCommand
        {
            CartId = cartId,
            Price = 100,
            Active = true,
            Quantity = 1,
            ProductId = productId
        });

        var command = new UpdateCartItemCommand
        {
            Id = itemId,
            Price = 200,
            Active = true,
            Quantity = 2
        };

        await SendAsync(command);

        var item = await FindAsync<CartItem>(itemId);

        item.Should().NotBeNull();
        item!.Price.Should().Be(command.Price);
        item.LastModifiedBy.Should().NotBeNull();
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
