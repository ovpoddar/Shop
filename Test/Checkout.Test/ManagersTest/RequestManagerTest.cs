using Checkout.Builders;
using Checkout.Managers;
using Checkout.Services;
using DataAccess.Entities;
using FluentAssertions;
using Moq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Test.ManagersTest
{
    public class RequestManagerTest
    {
        private readonly Mock<IRequestBuilder> _builder;
        private readonly Mock<IHttpServices> _sent;
        private readonly RequestManger _requestManger;
        public RequestManagerTest()
        {
            _builder = new Mock<IRequestBuilder>();
            _sent = new Mock<IHttpServices>();
            _requestManger = new RequestManger(_builder.Object, _sent.Object);
        }

        [Fact]
        public async Task GetRequestTestAsync()
        {
            var content = "Hello World";
            var factory = new Mock<IHttpClientFactory>();
            var url = "http://www.login.us";

            _sent
                .Setup(e => e.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(content),
                })
                .Verifiable();
            _builder
                .Setup(e => e.BuildRequest(HttpMethod.Get, It.IsAny<Uri>(), It.IsAny<string>()))
                .Returns(new HttpRequestMessage()
                {
                    Content = new StringContent(content)
                })
                .Verifiable();

            var result = await _requestManger.GetRequest(url, It.IsAny<string>());

            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task PatchRequestTestAsync()
        {
            var content = "Hello World";
            var factory = new Mock<IHttpClientFactory>();

            _sent
                .Setup(e => e.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(content),
                })
                .Verifiable();
            _builder
                .Setup(e => e.BuildRequest(HttpMethod.Patch, It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new HttpRequestMessage()
                {
                    Content = new StringContent(content)
                })
                .Verifiable();

            var result = await _requestManger.PatchRequest("localhost/1020://index.html", new Product() { ProductName = "xoxo" }, It.IsAny<string>());

            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task PostRequestTestAsync()
        {
            var content = "Hello World";
            var factory = new Mock<IHttpClientFactory>();

            _sent
                .Setup(e => e.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(content),
                })
                .Verifiable();
            _builder
                .Setup(e => e.BuildRequest(HttpMethod.Post, It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new HttpRequestMessage()
                {
                    Content = new StringContent(content)
                })
                .Verifiable();

            var result = await _requestManger.PostRequest("localhost/1020://index.html", new Product() { ProductName = "xoxo" });

            result.Should().NotBeNullOrEmpty();
        }

    }
}
