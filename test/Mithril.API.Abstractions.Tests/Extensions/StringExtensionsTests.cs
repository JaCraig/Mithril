using Mithril.API.Abstractions.ExtensionMethods;
using Mithril.Tests.Helpers;
using Xunit;

namespace Mithril.API.Abstractions.Tests.Extensions
{
    public class StringExtensionsTests : TestBaseClass
    {
        protected override Type? ObjectType { get; set; } = typeof(StringExtensions);

        /// <summary>
        /// Whens the split camel case returns correct string.
        /// </summary>
        [Fact]
        public void When_SplitCamelCase_ReturnsCorrectString()
        {
            const string TestString = "ThisIsATest";
            const string ExpectedResult = "This Is A Test";
            var Result = TestString.SplitCamelCase();
            Assert.Equal(ExpectedResult, Result);
        }
    }
}