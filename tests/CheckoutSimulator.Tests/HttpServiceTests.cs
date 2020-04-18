using CheckoutSimulator.Services;
using FluentAssertions;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CheckoutSimulator.Tests
{
    public class HttpServiceTests
    {
        [Fact(DisplayName = "SendAsync returns HttpResponseMessage successfully")]
        public void Test1()
        {
            var content = "Hello World";
            var factory = new Mock<IHttpClientFactory>();
            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            handler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(content),
                })
                .Verifiable();

            var client = new HttpClient(handler.Object);

            factory.Setup(httpFactory => httpFactory.CreateClient(It.IsAny<string>())).Returns(client);

            var httpService = new HttpService(factory.Object);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://test.com/api/test/whatever");
            var result = httpService.SendAsync(httpRequestMessage);

            result.Should().NotBeNull();

            var actualResult = result.Result;

            actualResult.StatusCode.Should().Be(HttpStatusCode.OK);
            actualResult.Content.ReadAsStringAsync().Result.Should().Be(content);
        }
    }
}

