using StoreManagementApiCA.Application.Products.Commands.CreateProduct;
using StoreManagementApiCA.Application.Products.Commands.UpdateProduct;

using StoreManagementApiCA.Domain.Entities;

namespace StoreManagementApiCA.Application.FunctionalTests.Products.Commands;

using static Testing;

public class UpdateProductTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidProductId()
    {
        await RunAsAdministratorAsync();
        var command = new UpdateProductCommand {
            Id = 99,
            Title = "New Toy Car",
            Name = "Toy Toyota Car",
            Price = 100,
            Type = "Toy",
            Quantity = 100
        };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldUpdateTodoItem()
    {
        await RunAsAdministratorAsync();


        var itemId = await SendAsync(new CreateProductCommand
        {
            Title = "New Toy Car",
            Name = "Toy Toyota Car",
            Price = 100,
            Type = "Toy",
            Quantity = 100
        });

        var command = new UpdateProductCommand
        {
            Id = itemId,
            Title = "Updated Item Title",
            Name = "Toy Toyota Car",
            Price = 100,
            Type = "Toy",
            Quantity = 100
        };

        await SendAsync(command);

        var item = await FindAsync<Product>(itemId);

        item.Should().NotBeNull();
        item!.Title.Should().Be(command.Title);
        item.LastModifiedBy.Should().NotBeNull();
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
