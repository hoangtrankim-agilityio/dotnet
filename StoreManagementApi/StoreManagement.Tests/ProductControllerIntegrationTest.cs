using System.Net;
using Newtonsoft.Json;
using StoreManagement.Core.Models;
using StoreManagement.Api.Wrappers;

namespace StoreManagement.Tests;
public class ProductsControllerIntegrationTests: IClassFixture<TestingWebAppFactory<Program>>, IClassFixture<ProductSeedDataFixture>
{
    private readonly HttpClient _client;
    private readonly ProductSeedDataFixture _fixture;

    public ProductsControllerIntegrationTests(TestingWebAppFactory<Program> factory, ProductSeedDataFixture fixture)
    {
        _client = factory.CreateClient();
        _fixture = fixture;
    }

    [Fact]
    public async Task GET_retrieves_product()
    {
        // _fixture.ApiDbContext;
        var response = await _client.GetAsync("/api/products");
        var responseString = await response.Content.ReadAsStringAsync();
        var pagedResponse = JsonConvert.DeserializeObject<PagedResponse<List<Product>>>(responseString);

        Assert.Equal(response.StatusCode, HttpStatusCode.OK);
        Assert.Equal(pagedResponse.Succeeded, true);
    }
}