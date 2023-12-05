using StoreManagementApiCA.Application.Products.Commands.CreateProduct;
using StoreManagementApiCA.Application.Products.Commands.DeleteProduct;
using StoreManagementApiCA.Domain.Entities;

namespace StoreManagementApiCA.Application.FunctionalTests.Products.Commands;

using static Testing;

public class DeleteProductTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidProductId()
    {
        await RunAsAdministratorAsync();
        var command = new DeleteProductCommand(99);

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteProduct()
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

        await SendAsync(new DeleteProductCommand(itemId));

        var item = await FindAsync<Product>(itemId);

        item.Should().BeNull();
    }
}
