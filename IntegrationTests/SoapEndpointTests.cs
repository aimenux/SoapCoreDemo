using System.Net;
using System.Text;
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

    [Theory]
    [InlineData("Foo")]
    [InlineData("Bar")]
    public async Task ShouldGetValidSoapResponseWithSvcSoapService(string name)
    {
        // arrange
        const string mediaType = "text/xml";
        const string operationName = "Run";
        var soapEnvelope = @$"
          <Envelope xmlns=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"" xmlns:web=""http://schemas.datacontract.org/2004/07/WebApi.Models"">
            <Body>
                <tem:Run>
                    <tem:model>
                        <web:Name>{name}</web:Name>
                    </tem:model>
                </tem:Run>
            </Body>
         </Envelope>";

        var client = _fixture.CreateClient();
        var content = new StringContent(soapEnvelope, Encoding.UTF8, mediaType);

        // act
        var response = await client.PostAsync($"/{Constants.SvcName}?op={operationName}", content);
        var body = await response.Content.ReadAsStringAsync();

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        body.Should().Contain($"Hello {name}");
    }

    [Theory]
    [InlineData("Foo")]
    [InlineData("Bar")]
    public async Task ShouldGetValidSoapResponseWithAsmxSoapService(string name)
    {
        // arrange
        const string mediaType = "text/xml";
        const string operationName = "Run";
        var soapEnvelope = @$"
          <Envelope xmlns=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
            <Body>
                <tem:Run>
                    <tem:model>
                        <tem:Name>{name}</tem:Name>
                    </tem:model>
                </tem:Run>
            </Body>
         </Envelope>";

        var client = _fixture.CreateClient();
        var content = new StringContent(soapEnvelope, Encoding.UTF8, mediaType);

        // act
        var response = await client.PostAsync($"/{Constants.AsmxName}?op={operationName}", content);
        var body = await response.Content.ReadAsStringAsync();

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        body.Should().Contain($"Hello {name}");
    }
}