using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Mithril.Core.Extensions;
using Mithril.Tests.Helpers;
using Xunit;

namespace Mithril.Core.Tests.Extensions
{
    /// <summary>
    /// StaticHttpContextExtensions tests
    /// </summary>
    /// <seealso cref="TestBaseClass"/>
    public class StaticHttpContextExtensionsTests : TestBaseClass
    {
        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        protected override Type? ObjectType { get; set; } = typeof(StaticHttpContextExtensions);

        /// <summary>
        /// When AddStaticHttpContextAccessor called, returns not null from services.
        /// </summary>
        [Fact]
        public void When_AddStaticHttpContextAccessor_ReturnsNotNullFromServices()
        {
            var Services = new ServiceCollection();
            _ = Services.AddStaticHttpContextAccessor();
            ServiceProvider ServiceProvider = Services.BuildServiceProvider();
            IHttpContextAccessor HttpContextAccessor = ServiceProvider.GetRequiredService<IHttpContextAccessor>();
            Assert.NotNull(HttpContextAccessor);
        }
    }
}