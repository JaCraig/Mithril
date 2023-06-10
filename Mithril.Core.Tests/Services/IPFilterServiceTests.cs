using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mithril.Core.Abstractions.Services.Options;
using Mithril.Core.Services;
using Mithril.Tests.Helpers;
using NSubstitute;
using System.Net;

namespace Mithril.Core.Tests.Services
{
    /// <summary>
    /// IPFilterService tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;IPFilterService&gt;"/>
    public class IPFilterServiceTests : TestBaseClass<IPFilterService>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IPFilterServiceTests"/> class.
        /// </summary>
        public IPFilterServiceTests()
        {
            TestObject = new IPFilterService(null, null);
            ObjectType = typeof(IPFilterService);
        }

        /// <summary>
        /// When the ip address does not exist, ip check returns false.
        /// </summary>
        [Fact]
        public void When_IPAddressDoesNotExist_IPCheckReturnsFalse()
        {
            IOptions<IPFilterOptions> MockPolicy = Substitute.For<IOptions<IPFilterOptions>>();
            ILogger<IPFilterService> MockLogger = Substitute.For<ILogger<IPFilterService>>();
            HttpContext MockHttpContext = Substitute.For<HttpContext>();
            MockHttpContext.Connection.RemoteIpAddress.Returns((IPAddress)null);

            var FilterOptions = new IPFilterOptions();
            FilterOptions.AddDefaultPolicy();
            MockPolicy.Value.Returns(FilterOptions);

            var Service = new IPFilterService(MockPolicy, MockLogger);

            Assert.False(Service.CheckIPAllowed(MockHttpContext, "DefaultPolicy"));
        }

        /// <summary>
        /// When the policy does not exist ip check returns trye.
        /// </summary>
        [Fact]
        public void When_PolicyDoesNotExist_IPCheckReturnsTrue()
        {
            IOptions<IPFilterOptions> MockPolicy = Substitute.For<IOptions<IPFilterOptions>>();
            ILogger<IPFilterService> MockLogger = Substitute.For<ILogger<IPFilterService>>();
            HttpContext MockHttpContext = Substitute.For<HttpContext>();
            MockHttpContext.Connection.RemoteIpAddress.Returns(IPAddress.Parse("10.0.0.1"));

            var FilterOptions = new IPFilterOptions();
            MockPolicy.Value.Returns(FilterOptions);

            var Service = new IPFilterService(MockPolicy, MockLogger);

            Assert.True(Service.CheckIPAllowed(MockHttpContext, "DefaultPolicy"));
        }

        /// <summary>
        /// When the policy exists, the ip of the call is checked.
        /// </summary>
        [Fact]
        public void When_PolicyExists_IPChecked()
        {
            IOptions<IPFilterOptions> MockPolicy = Substitute.For<IOptions<IPFilterOptions>>();
            ILogger<IPFilterService> MockLogger = Substitute.For<ILogger<IPFilterService>>();
            HttpContext MockHttpContext = Substitute.For<HttpContext>();
            MockHttpContext.Connection.RemoteIpAddress.Returns(IPAddress.Parse("10.0.0.1"));

            var FilterOptions = new IPFilterOptions();
            FilterOptions.AddDefaultPolicy();
            MockPolicy.Value.Returns(FilterOptions);

            var Service = new IPFilterService(MockPolicy, MockLogger);

            Assert.True(Service.CheckIPAllowed(MockHttpContext, "DefaultPolicy"));
        }
    }
}