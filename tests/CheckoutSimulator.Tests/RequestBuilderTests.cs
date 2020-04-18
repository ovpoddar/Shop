using System;
using System.Net.Http;
using System.Text;
using CheckoutSimulator.Builders;
using FluentAssertions;
using Moq;
using Xunit;

namespace CheckoutSimulator.Tests
{
    public class RequestBuilderTests
    {
        private readonly RequestBuilder _requestBuilder;

        public RequestBuilderTests()
        {
            _requestBuilder = new RequestBuilder();
        }

        [Fact]
        public void BuildGetRequestReturnsHttpRequestMessage()
        {
            const string uri = "myServer/api/location/guid";

            var output = _requestBuilder.BuildGetRequest(uri);

            output.RequestUri.Should().Be($"{uri}");
            output.Method.Should().Be(HttpMethod.Get);
        }

        [Fact]
        public void BuildPutRequestReturnsHttpRequestMessage()
        {
            const string uri = "myServer/api/location/guid";
            const string content = "Content";

            var output = _requestBuilder.BuildInsertRequest(uri, content, HttpMethod.Put);

            output.RequestUri.Should().Be($"{uri}");
            output.Method.Should().Be(HttpMethod.Put);

            var outputContent = output.Content;
            var outputContentResult = outputContent.ReadAsStringAsync().Result;
            outputContentResult.Should().Be(content);
        }

        [Fact(DisplayName = "BuildDeleteRequest with 2 inputs returns correct equestMessage")]
        public void BuildDeleteRequestWithTwoInputs()
        {
            const string uri = "myServer/api/location/guid";

            var output = _requestBuilder.BuildDeleteRequest(uri);
            output.Method.Should().Be(HttpMethod.Delete);
            output.RequestUri.Should().Be(uri);
        }

        [Fact(DisplayName = "BuildDeleteRequest with 3 inputs returns correct equestMessage")]
        public void BuildDeleteRequestWithThreeInputs()
        {
            const string uri = "myServer/api/location/guid";
            const string content = "Content";

            var output = _requestBuilder.BuildDeleteRequest(uri, content);

            output.Method.Should().Be(HttpMethod.Delete);
            output.RequestUri.Should().Be(uri);
            output.Content.ReadAsStringAsync().Result.Should().Be(content);
        }

        [Fact]
        public void BuildPatchRequestReturnsHttpRequestMessage()
        {
            const string uri = "myServer/api/location/guid";

            var output = _requestBuilder.BuildPatchRequest(uri);

            output.RequestUri.Should().Be($"{uri}");
            output.Method.Should().Be(HttpMethod.Patch);
        }
    }
}