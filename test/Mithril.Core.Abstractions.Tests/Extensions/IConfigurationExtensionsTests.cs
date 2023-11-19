using Microsoft.Extensions.Configuration;
using Mithril.Core.Abstractions.Extensions;
using Mithril.Tests.Helpers;
using Xunit;

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
            IConfigurationRoot Config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    {"TestSection:TestKey", "TestValue"}
                })
                .Build();

            Dictionary<string, string>? Section = Config.GetConfig<Dictionary<string, string>>("TestSection2");

            Assert.Null(Section);
        }

        /// <summary>
        /// When the configuration section exists get configuration returns section.
        /// </summary>
        [Fact]
        public void When_ConfigSectionExists_GetConfigReturnsSection()
        {
            IConfigurationRoot Config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    {"TestSection:TestKey", "TestValue"}
                })
                .Build();

            Dictionary<string, string>? Section = Config.GetConfig<Dictionary<string, string>>("TestSection");

            Assert.NotNull(Section);
            Assert.Equal("TestValue", Section["TestKey"]);
        }

        /// <summary>
        /// When the system configuration does not exist get system configuration returns null.
        /// </summary>
        [Fact]
        public void When_SystemConfigDoesNotExist_GetSystemConfigReturnsNull()
        {
            IConfigurationRoot Config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    {"Mithril2:ApplicationName", "TestValue"}
                })
                .Build();

            Abstractions.Configuration.MithrilConfig? Section = Config.GetSystemConfig();

            Assert.Null(Section);
        }

        /// <summary>
        /// When the system configuration exists get system configuration returns section.
        /// </summary>
        [Fact]
        public void When_SystemConfigExists_GetSystemConfigReturnsSection()
        {
            IConfigurationRoot Config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    {"Mithril:ApplicationName", "TestValue"}
                })
                .Build();

            Abstractions.Configuration.MithrilConfig? Section = Config.GetSystemConfig();

            Assert.NotNull(Section);
            Assert.Equal("TestValue", Section.ApplicationName);
        }
    }
}