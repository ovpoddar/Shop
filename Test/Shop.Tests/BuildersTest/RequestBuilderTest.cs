using Checkout.Builders;
using FluentAssertions;
using System.Net.Http;
using Xunit;

namespace Shop.Tests.BuildersTest
{
    public class RequestBuilderTest
    {
        private readonly RequestBuilder _requestBuilder;
        public RequestBuilderTest()
        {
            _requestBuilder = new RequestBuilder();
        }

        [Fact]
        public void BuildRequestWithoutParameterTest()
        {
            var uri = "myServer/api/location/guid";
            var method = HttpMethod.Patch;

            var output = _requestBuilder.BuildRequest(method, uri);

            output.RequestUri.Should().Be($"{uri}");
            output.Method.Should().Be(method);
        }

        [Fact]
        public void BuildRequestWithParameterTest()
        {
            var uri = "myServer/api/location/guid";
            var method = HttpMethod.Patch;
            const string content = "Content";

            var output = _requestBuilder.BuildRequest(method, uri, content);

            output.Method.Should().Be(method);
            output.RequestUri.Should().Be(uri);
            output.Content.ReadAsStringAsync().Result.Should().Be(content);
        }

    }
}
