using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Moq.Protected;
using Shop.Builders;
using Shop.Entities;
using Shop.Managers;
using Shop.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Shop.Tests.ManagersTests
{
    public class RequestMangerTest
    {
        private readonly Mock<IRequestBuilder> _builder;
        private readonly Mock<ISentRequestService> _sent;
        private readonly RequestManger _requestManger;
        public RequestMangerTest()
        {
            _builder = new Mock<IRequestBuilder>();
            _sent = new Mock<ISentRequestService>();
            _requestManger = new RequestManger(_builder.Object, _sent.Object);
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

            var result = await _requestManger.PatchRequest("localhost/1020://index.html", new Product() { ProductName = "xoxo" });

            result.Should().NotBeNullOrEmpty();
        }
    }
}
