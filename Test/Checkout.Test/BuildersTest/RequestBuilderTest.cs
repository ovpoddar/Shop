using Checkout.Builders;
using FluentAssertions;
using System;
using System.Net.Http;
using Xunit;

namespace Checkout.Test.BuildersTest
{
    public class RequestBuilderTest
    {
        private readonly RequestBuilder _requestBuilder;

        public RequestBuilderTest()
        {
            _requestBuilder = new RequestBuilder();
        }

        [Fact]
        public void BuildRequestWithToken()
        {
            var uri = new Uri("http://www.api/location/guid");
            var token = "this token should pass in every request";
            var builder = _requestBuilder.BuildRequest(HttpMethod.Get, uri, token);
            builder.Method.Should().Be(builder.Method);
            builder.RequestUri.Should().Be(uri);
            builder.Content.Should().BeNull();
            builder.Headers.Authorization.Should().NotBeNull();
            builder.Headers.Authorization.Parameter.Should().NotBeNullOrEmpty();
            builder.Headers.Authorization.Parameter.Should().Equals(token);
        }

        [Fact]
        public void BuildRequestWithoutToken()
        {
            var uri = "http://www.api/location/guid";
            var Content = "this the the onject as string which need to send in body";
            var builder = _requestBuilder.BuildRequest(HttpMethod.Get, uri, Content);

            builder.Method.Should().Be(builder.Method);
            builder.RequestUri.Should().Be(uri);
            builder.Content.ReadAsStringAsync().Result.Should().Be(Content);
        }

        [Fact]
        public void BuildRequestWithParameterTest()
        {
            var uri = "myServer/api/location/guid";
            var method = HttpMethod.Patch;
            const string content = "Content";
            var token = "this token should pass in every request";

            var output = _requestBuilder.BuildRequest(method, uri, token, content);

            output.Method.Should().Be(method);
            output.RequestUri.Should().Be(uri);
            output.Content.ReadAsStringAsync().Result.Should().Be(content);
            output.Headers.Authorization.Should().NotBeNull();
            output.Headers.Authorization.Parameter.Should().NotBeNullOrEmpty();
            output.Headers.Authorization.Parameter.Should().Equals(token);
        }
    }
}
