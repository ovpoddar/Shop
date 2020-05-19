using CheckoutSimulator.Builders;
using CheckoutSimulator.Handlers;
using CheckoutSimulator.Models;
using FluentAssertions;
using Mi4com.UserManagement.Api.Services;
using Moq;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CheckoutSimulator.Tests
{
    public class RequestHandlerTests
    {
        private readonly Mock<IRequestBuilder> _requestBuilder;
        private readonly Mock<IHttpService> _httpService;
        private readonly RequestHandler _requestHandler;

        public RequestHandlerTests()
        {
            _requestBuilder = new Mock<IRequestBuilder>();
            _httpService = new Mock<IHttpService>();
            _requestHandler = new RequestHandler(_requestBuilder.Object, _httpService.Object);
        }

        [Fact]
        public async Task GetRequestReturnsString()
        {
            const string content = "string Content";
            const string suffix = "api/location/value";

            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(content)
            };

            _requestBuilder.Setup(method => method.BuildGetRequest(It.IsAny<string>()))
                .Returns(new HttpRequestMessage());
            _httpService.Setup(method => method.SendAsync(It.IsAny<HttpRequestMessage>()))
                .Returns(Task.FromResult(httpResponseMessage));

            var output = await _requestHandler.GetRequest(suffix);

            _requestBuilder.Verify(method => method.BuildGetRequest(It.IsAny<string>()), Times.Once());
            _requestBuilder.Verify(method => method.BuildInsertRequest(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<HttpMethod>()), Times.Never);
            _httpService.Verify(method => method.SendAsync(It.IsAny<HttpRequestMessage>()), Times.Once);

            output.Should().Be(content);
        }

        [Fact]
        public async Task PutRequestReturnsString()
        {
            const string suffix = "api/location/value";
            var saleProduct = new SaleProduct
            {
               ProductId = 1
            };

            var content = JsonConvert.SerializeObject(saleProduct);

            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(content)
            };

            _requestBuilder.Setup(method => method.BuildInsertRequest(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<HttpMethod>()))
                .Returns(new HttpRequestMessage());
            _httpService.Setup(method => method.SendAsync(It.IsAny<HttpRequestMessage>()))
                .Returns(Task.FromResult(httpResponseMessage));

            var output = await _requestHandler.InsertRequest(suffix,  saleProduct, HttpMethod.Put);

            _requestBuilder.Verify(method => method.BuildInsertRequest(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<HttpMethod>()), Times.Once);
            _requestBuilder.Verify(method => method.BuildGetRequest(It.IsAny<string>()), Times.Never);
            _httpService.Verify(method => method.SendAsync(It.IsAny<HttpRequestMessage>()), Times.Once);

            output.Should().Be(content);
        }

        [Fact]
        public async Task DeleteRequestWithTwoParamsReturnsString()
        {
            const string content = "string Content";
            const string suffix = "api/location/value";

            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(content)
            };

            _requestBuilder.Setup(method => method.BuildDeleteRequest(It.IsAny<string>())).Returns(new HttpRequestMessage());
            _httpService.Setup(method => method.SendAsync(It.IsAny<HttpRequestMessage>())).Returns(Task.FromResult(httpResponseMessage));

            var output = await _requestHandler.DeleteRequest(suffix);

            _requestBuilder.Verify(method => method.BuildDeleteRequest(It.IsAny<string>()), Times.Once());
            _httpService.Verify(method => method.SendAsync(It.IsAny<HttpRequestMessage>()), Times.Once);

            output.Should().Be(content);
        }

        [Fact]
        public async Task DeleteRequestWithThreeParamsReturnsString()
        {
            const string content = "string Content";
            const string suffix = "api/location/value";
            const string entity = "Thing";

            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(content)
            };

            _requestBuilder.Setup(method => method.BuildDeleteRequest(It.IsAny<string>(), It.IsAny<string>())).Returns(new HttpRequestMessage());
            _httpService.Setup(method => method.SendAsync(It.IsAny<HttpRequestMessage>())).Returns(Task.FromResult(httpResponseMessage));

            var output = await _requestHandler.DeleteRequest(suffix, entity);

            _requestBuilder.Verify(method => method.BuildDeleteRequest(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
            _httpService.Verify(method => method.SendAsync(It.IsAny<HttpRequestMessage>()), Times.Once);

            output.Should().Be(content);
        }

        [Fact]
        public async Task PatchRequestReturnsString()
        {
            const string content = "string Content";
            const string suffix = "api/location/value";

            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(content)
            };

            _requestBuilder.Setup(method => method.BuildPatchRequest(It.IsAny<string>()))
                .Returns(new HttpRequestMessage());
            _httpService.Setup(method => method.SendAsync(It.IsAny<HttpRequestMessage>()))
                .Returns(Task.FromResult(httpResponseMessage));

            var output = await _requestHandler.PatchRequest(suffix);

            _requestBuilder.Verify(method => method.BuildPatchRequest(It.IsAny<string>()), Times.Once());
            _requestBuilder.Verify(method => method.BuildInsertRequest(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<HttpMethod>()), Times.Never);
            _httpService.Verify(method => method.SendAsync(It.IsAny<HttpRequestMessage>()), Times.Once);

            output.Should().Be(content);
        }

        [Fact]
        public async Task PostRequestReturnsString()
        {
            const string suffix = "api/location/value";

            var user = new Brand
            {
                BrandName = "Name"
            };

            var content = JsonConvert.SerializeObject(user);

            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(content)
            };

            _requestBuilder.Setup(method => method.BuildInsertRequest(It.IsAny<string>(),  It.IsAny<string>(), It.IsAny<HttpMethod>()))
                .Returns(new HttpRequestMessage());
            _httpService.Setup(method => method.SendAsync(It.IsAny<HttpRequestMessage>()))
                .Returns(Task.FromResult(httpResponseMessage));

            var output = await _requestHandler.PostRequest(suffix, user);

            _requestBuilder.Verify(method => method.BuildInsertRequest(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<HttpMethod>()), Times.Once);
            _requestBuilder.Verify(method => method.BuildGetRequest(It.IsAny<string>()), Times.Never);
            _httpService.Verify(method => method.SendAsync(It.IsAny<HttpRequestMessage>()), Times.Once);

            output.Should().Be(content);
        }

        [Fact]
        public async Task PatchRequestWithEntityReturnsString()
        {
            const string suffix = "api/location/value";

            var user = new SaleProduct
            {
                ProductId = 1
            };

            var content = JsonConvert.SerializeObject(user);

            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(content)
            };

            _requestBuilder.Setup(method => method.BuildInsertRequest(It.IsAny<string>(),  It.IsAny<string>(), It.IsAny<HttpMethod>()))
                .Returns(new HttpRequestMessage());
            _httpService.Setup(method => method.SendAsync(It.IsAny<HttpRequestMessage>()))
                .Returns(Task.FromResult(httpResponseMessage));

            var output = await _requestHandler.PatchRequest(suffix, user);

            _requestBuilder.Verify(method => method.BuildInsertRequest(It.IsAny<string>(),  It.IsAny<string>(), It.IsAny<HttpMethod>()), Times.Once);
            _requestBuilder.Verify(method => method.BuildGetRequest(It.IsAny<string>()), Times.Never);
            _httpService.Verify(method => method.SendAsync(It.IsAny<HttpRequestMessage>()), Times.Once);

            output.Should().Be(httpResponseMessage);
        }
    }
}