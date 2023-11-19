using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Mithril.Core.Abstractions.Configuration;
using Mithril.Core.Middleware;
using Mithril.Tests.Helpers;
using Xunit;

namespace Mithril.Core.Tests.Middleware
{
    /// <summary>
    /// CSP Middleware tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;CSPMiddleware&gt;"/>
    public class CSPMiddlewareTests : TestBaseClass<CSPMiddleware>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSPMiddlewareTests"/> class.
        /// </summary>
        public CSPMiddlewareTests()
        {
            TestObject = new CSPMiddleware(null, null);
            ObjectType = typeof(CSPMiddleware);
        }

        /// <summary>
        /// Invokes the asynchronous should add content security policy header.
        /// </summary>
        [Fact]
        public async Task InvokeAsync_Should_Add_Content_Security_Policy_Header()
        {
            var Middleware = new CSPMiddleware(
                next: (_) => Task.CompletedTask,
                configuration: Options.Create(new MithrilConfig
                {
                    Security = new Security
                    {
                        ContentSecurityPolicy = "script-src 'self'; object-src 'none'; base-uri 'self'"
                    }
                }));
            var HttpContext = new DefaultHttpContext();

            await Middleware.InvokeAsync(HttpContext);

            Assert.False(string.IsNullOrEmpty(HttpContext.Response.Headers.ContentSecurityPolicy));
            Assert.Equal("script-src 'self'; object-src 'none'; base-uri 'self'; report-uri /api/Command/CSPLog", HttpContext.Response.Headers.ContentSecurityPolicy);
        }
    }
}