using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Mithril.API.Abstractions.Configuration;
using Mithril.API.Abstractions.Modules;
using Mithril.Tests.Helpers;
using NSubstitute;

namespace Mithril.API.Abstractions.Tests.Modules
{
    /// <summary>
    /// APIModule tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;APIModule&gt;"/>
    public class APIModuleTests : TestBaseClass<APIModule>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="APIModuleTests"/> class.
        /// </summary>
        public APIModuleTests()
        {
            TestObject = new APIModule();
        }

        [Fact]
        public void When_ConfigureServicesIsCalled_APIOptionsIsNotNull()
        {
            IConfiguration MockConfiguration = Substitute.For<IConfiguration>();
            IHostEnvironment MockHostingEnvironment = Substitute.For<IHostEnvironment>();
            var TestObject = new APIModule();
            var Services = new ServiceCollection();
            TestObject.ConfigureServices(Services, MockConfiguration, MockHostingEnvironment);
            IOptions<APIOptions>? Result = Services.BuildServiceProvider().GetService<IOptions<APIOptions>>();
            Assert.NotNull(Result);
        }
    }
}