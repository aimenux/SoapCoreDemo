using FluentAssertions;
using WebApi.Models;
using WebApi.Services;

namespace UnitTests
{
    public class SoapServiceTests
    {
        [Theory]
        [InlineData("Foo")]
        [InlineData("Bar")]
        public void ShouldRunSoapModelSuccessfully(string name)
        {
            // arrange
            var model = new SoapModel
            {
                Name = name
            };
            var service = new SoapService();

            // act
            var result = service.Run(model);

            // assert
            result.Should().Be($"Hello {name}");
        }
    }
}