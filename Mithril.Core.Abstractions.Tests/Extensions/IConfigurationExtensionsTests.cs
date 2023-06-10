using Microsoft.Extensions.Configuration;
using Mithril.Core.Abstractions.Extensions;
using Mithril.Tests.Helpers;

namespace Mithril.Core.Abstractions.Tests.Extensions
{
    /// <summary>
    /// IConfiguration extensions tests
    /// </summary>
    /// <seealso cref="TestBaseClass"/>
    public class IConfigurationExtensionsTests : TestBaseClass
    {
        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        protected override Type? ObjectType { get; set; } = typeof(IConfigurationExtensions);

        /// <summary>
        /// When the configuration section does not exist get configuration returns null.
        /// </summary>
        [Fact]
        public void When_ConfigSectionDoesNotExist_GetConfigReturnsNull()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    {"TestSection:TestKey", "TestValue"}
                })
                .Build();

            Dictionary<string, string>? section = config.GetConfig<Dictionary<string, string>>("TestSection2");

            Assert.Null(section);
        }

        /// <summary>
        /// When the configuration section exists get configuration returns section.
        /// </summary>
        [Fact]
        public void When_ConfigSectionExists_GetConfigReturnsSection()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    {"TestSection:TestKey", "TestValue"}
                })
                .Build();

            Dictionary<string, string>? section = config.GetConfig<Dictionary<string, string>>("TestSection");

            Assert.NotNull(section);
            Assert.Equal("TestValue", section["TestKey"]);
        }

        /// <summary>
        /// When the system configuration does not exist get system configuration returns null.
        /// </summary>
        [Fact]
        public void When_SystemConfigDoesNotExist_GetSystemConfigReturnsNull()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    {"Mithril2:ApplicationName", "TestValue"}
                })
                .Build();

            Abstractions.Configuration.MithrilConfig? section = config.GetSystemConfig();

            Assert.Null(section);
        }

        /// <summary>
        /// When the system configuration exists get system configuration returns section.
        /// </summary>
        [Fact]
        public void When_SystemConfigExists_GetSystemConfigReturnsSection()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    {"Mithril:ApplicationName", "TestValue"}
                })
                .Build();

            Abstractions.Configuration.MithrilConfig? section = config.GetSystemConfig();

            Assert.NotNull(section);
            Assert.Equal("TestValue", section.ApplicationName);
        }
    }
}