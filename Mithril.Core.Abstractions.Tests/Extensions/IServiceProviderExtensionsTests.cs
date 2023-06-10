using Microsoft.Extensions.DependencyInjection;
using Mithril.Core.Abstractions.Extensions;
using Mithril.Tests.Helpers;

namespace Mithril.Core.Abstractions.Tests.Extensions
{
    /// <summary>
    /// IServiceProviderExtensions tests
    /// </summary>
    /// <seealso cref="TestBaseClass"/>
    public class IServiceProviderExtensionsTests : TestBaseClass
    {
        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        protected override Type? ObjectType { get; set; } = typeof(IServiceProviderExtensions);

        /// <summary>
        /// Whens the service does not exist, exists returns false.
        /// </summary>
        [Fact]
        public void When_ServiceDoesNotExist_ExistsReturnsFalse()
        {
            var services = new ServiceCollection();

            ServiceProvider provider = services.BuildServiceProvider();

            Assert.False(provider.Exists<ITestService>());
        }

        /// <summary>
        /// When the service exists, exists returns true.
        /// </summary>
        [Fact]
        public void When_ServiceExists_ExistsReturnsTrue()
        {
            var services = new ServiceCollection();
            services.AddSingleton<ITestService, TestService>();

            ServiceProvider provider = services.BuildServiceProvider();

            Assert.True(provider.Exists<ITestService>());
        }

        /// <summary>
        /// Test service interface
        /// </summary>
        private interface ITestService
        {
        }

        /// <summary>
        /// Test service
        /// </summary>
        /// <seealso cref="ITestService"/>
        private class TestService : ITestService
        {
        }
    }
}