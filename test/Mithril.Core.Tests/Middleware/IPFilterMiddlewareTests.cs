using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Mithril.Core.Abstractions.Services;
using Mithril.Core.Middleware;
using Mithril.Tests.Helpers;
using NSubstitute;
using System.Net;
using Xunit;

namespace Mithril.Core.Tests.Middleware
{
    /// <summary>
    /// IP Filter middleware tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;IPFilterMiddleware&gt;"/>
    public class IPFilterMiddlewareTests : TestBaseClass<IPFilterMiddleware>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IPFilterMiddlewareTests"/> class.
        /// </summary>
        public IPFilterMiddlewareTests()
        {
            TestObject = new IPFilterMiddleware(null, null, null);
            ObjectType = typeof(IPFilterMiddleware);
        }

        /// <summary>
        /// Invokes the asynchronous should call next middleware when ip is allowed.
        /// </summary>
        [Fact]
        public async Task InvokeAsync_Should_Call_Next_Middleware_When_IP_Is_Allowed()
        {
            IIPFilterService MockIpFilterService = GetMockIPFilterService(true);
            ILogger<IPFilterMiddleware> MockLogger = Substitute.For<ILogger<IPFilterMiddleware>>();
            var Middleware = new IPFilterMiddleware(
                next: (_) => Task.CompletedTask,
                iPFilterService: MockIpFilterService,
                logger: MockLogger);
            var HttpContext = new DefaultHttpContext();

            await Middleware.InvokeAsync(HttpContext);

            Assert.Equal((int)HttpStatusCode.OK, HttpContext.Response.StatusCode);
        }

        /// <summary>
        /// Invokes the asynchronous should return forbidden response when ip is blocked.
        /// </summary>
        [Fact]
        public async Task InvokeAsync_Should_Return_Forbidden_Response_When_IP_Is_Blocked()
        {
            IIPFilterService MockIpFilterService = GetMockIPFilterService(false);
            ILogger<IPFilterMiddleware> MockLogger = Substitute.For<ILogger<IPFilterMiddleware>>();
            var Middleware = new IPFilterMiddleware(
                next: (_) => Task.CompletedTask,
                iPFilterService: MockIpFilterService,
                logger: MockLogger);
            var HttpContext = new DefaultHttpContext();

            await Middleware.InvokeAsync(HttpContext);

            Assert.Equal((int)HttpStatusCode.Forbidden, HttpContext.Response.StatusCode);
        }

        /// <summary>
        /// Gets the mock ip filter service.
        /// </summary>
        /// <param name="returnValue">if set to <c>true</c> [return value].</param>
        private static IIPFilterService GetMockIPFilterService(bool returnValue)
        {
            IIPFilterService MockIpFilterService = Substitute.For<IIPFilterService>();
            _ = MockIpFilterService.CheckIPAllowed(Arg.Any<HttpContext>(), Arg.Any<string>()).Returns(returnValue);
            return MockIpFilterService;
        }
    }
}