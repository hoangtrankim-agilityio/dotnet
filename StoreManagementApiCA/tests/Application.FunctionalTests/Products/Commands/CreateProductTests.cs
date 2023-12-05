using StoreManagementApiCA.Application.Common.Security;
using StoreManagementApiCA.Application.Common.Exceptions;
using StoreManagementApiCA.Application.Products.Commands.CreateProduct;
using StoreManagementApiCA.Domain.Entities;

namespace StoreManagementApiCA.Application.FunctionalTests.Products.Commands;

using static Testing;

public class CreateProductTests : BaseTestFixture
{
    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        var command = new CreateProductCommand();

        command.GetType().Should().BeDecoratedWith<AuthorizeAttribute>();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<UnauthorizedAccessException>();
    }


    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        await RunAsAdministratorAsync();

        var command = new CreateProductCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateProduct()
    {
        await RunAsAdministratorAsync();

        var command = new CreateProductCommand
        {
            Title = "New Toy Car",
            Name = "Toy Toyota Car",
            Price = 100,
            Type = "Toy",
            Quantity = 100
        };

        var itemId = await SendAsync(command);

        var item = await FindAsync<Product>(itemId);
        item.Should().NotBeNull();
        item!.Id.Should().Be(itemId);
        item!.Title.Should().Be(command.Title);
        item!.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
