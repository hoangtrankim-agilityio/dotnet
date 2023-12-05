using StoreManagementApiCA.Application.Common.Security;
using StoreManagementApiCA.Application.Common.Exceptions;
using StoreManagementApiCA.Application.Carts.Commands.CreateCart;
using StoreManagementApiCA.Domain.Entities;

namespace StoreManagementApiCA.Application.FunctionalTests.Carts.Commands;

using static Testing;

public class CreateCartTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        await RunAsDefaultUserAsync();

        var command = new CreateCartCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateCart()
    {
        var userId = await RunAsDefaultUserAsync();

        var command = new CreateCartCommand
        {
            UserId = userId,
            Name = "Cart for test",
        };

        var itemId = await SendAsync(command);

        var item = await FindAsync<Cart>(itemId);
        item.Should().NotBeNull();
        item!.Id.Should().Be(itemId);
        item!.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
