using System.Net;
using FluentAssertions;
using WebApi;

namespace IntegrationTests;

public class SoapEndpointTests : IClassFixture<WebApiTestFixture>
{
    private readonly WebApiTestFixture _fixture;

    public SoapEndpointTests(WebApiTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Theory]
    [InlineData(Constants.SvcName)]
    [InlineData(Constants.AsmxName)]
    public async Task ShouldSoapServiceBeUp(string serviceName)
    {
        // arrange
        var client = _fixture.CreateClient();

        // act
        var response = await client.GetAsync($"/{serviceName}");

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}