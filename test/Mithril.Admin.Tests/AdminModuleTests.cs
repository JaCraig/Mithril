using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Tests
{
    /// <summary>
    /// Admin module tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;AdminModule&gt;" />
    public class AdminModuleTests : TestBaseClass<AdminModule>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdminModuleTests"/> class.
        /// </summary>
        public AdminModuleTests()
        {
            TestObject = new AdminModule();
            ObjectType = typeof(AdminModule);
        }

        /// <summary>
        /// Configures the services test.
        /// </summary>
        [Fact]
        public void ConfigureServicesTest()
        {
            var Services = new ServiceCollection();
            IConfigurationRoot Configuration = new ConfigurationBuilder().Build();
            IHostEnvironment Environment = NSubstitute.Substitute.For<IHostEnvironment>();
            _ = (TestObject?.ConfigureServices(Services, Configuration, Environment));
            Assert.NotNull(Services);
        }
    }
}