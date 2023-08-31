using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Mithril.Core.Abstractions.Configuration;
using Mithril.Core.Middleware;
using Mithril.Tests.Helpers;
using NSubstitute;
using System.Net;

namespace Mithril.Core.Tests.Middleware
{
    /// <summary>
    /// XFrame options middleware tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;XFrameOptionsMiddleware&gt;"/>
    public class XFrameOptionsMiddlewareTests : TestBaseClass<XFrameOptionsMiddleware>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XFrameOptionsMiddlewareTests"/> class.
        /// </summary>
        public XFrameOptionsMiddlewareTests()
        {
            TestObject = new XFrameOptionsMiddleware(null, null);
            ObjectType = typeof(XFrameOptionsMiddleware);
        }

        /// <summary>
        /// InvokeAsync should call next middleware.
        /// </summary>
        [Fact]
        public void InvokeAsync_Should_Call_Next_Middleware()
        {
            IOptions<MithrilConfig> MockOptions = Substitute.For<IOptions<MithrilConfig>>();
            var Middleware = new XFrameOptionsMiddleware(
                               next: (_) => Task.FromResult(0),
                               configuration: MockOptions);
            var HttpContext = new DefaultHttpContext();

            _ = Middleware.InvokeAsync(HttpContext);

            Assert.Equal((int)HttpStatusCode.OK, HttpContext.Response.StatusCode);
        }

        /// <summary>
        /// InvokeAsync should set header.
        /// </summary>
        [Fact]
        public void InvokeAsync_Should_Set_Header()
        {
            IOptions<MithrilConfig> MockOptions = Substitute.For<IOptions<MithrilConfig>>();
            var Middleware = new XFrameOptionsMiddleware(
                                next: (_) => Task.FromResult(0),
                                configuration: MockOptions);
            var HttpContext = new DefaultHttpContext();

            _ = Middleware.InvokeAsync(HttpContext);

            Assert.Equal("deny", HttpContext.Response.Headers["X-Frame-Options"]);
        }

        /// <summary>
        /// InvokeAsync should set header with configured value.
        /// </summary>
        [Fact]
        public void InvokeAsync_Should_Set_Header_With_Configured_Value()
        {
            IOptions<MithrilConfig> MockOptions = Substitute.For<IOptions<MithrilConfig>>();
            _ = MockOptions.Value.Returns(new MithrilConfig { Security = new Security { XFrameOptions = "SAMEORIGIN" } });
            var Middleware = new XFrameOptionsMiddleware(
                                               next: (_) => Task.FromResult(0),
                                                                              configuration: MockOptions);
            var HttpContext = new DefaultHttpContext();

            _ = Middleware.InvokeAsync(HttpContext);

            Assert.Equal("SAMEORIGIN", HttpContext.Response.Headers["X-Frame-Options"]);
        }
    }
}