using Mithril.Core.Abstractions.Extensions;
using Mithril.Tests.Helpers;
using System.Dynamic;
using Xunit;

namespace Mithril.Core.Abstractions.Tests.Extensions
{
    public class Example
    {
        public string? Test { get; set; } = "A";
    }

    public class Example2
    {
        public Example Example { get; set; } = new Example();
    }

    /// <summary>
    /// ExpandoObject extension methods
    /// </summary>
    /// <seealso cref="TestBaseClass"/>
    public class ExpandoObjectExtensionsTests : TestBaseClass
    {
        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        protected override Type? ObjectType { get; set; } = typeof(ExpandoObjectExtensions);

        /// <summary>
        /// Converts the expando to a dictionary test.
        /// </summary>
        [Fact]
        public void ConvertExpandoTest()
        {
            var Expando = new ExpandoObject();
            _ = Expando.TryAdd("test", "test");
            Dictionary<string, object?>? Result = Expando.ConvertExpando<Dictionary<string, object?>>();
            Assert.NotNull(Result);
            Assert.Equal("test", Result["Test"]);
        }

        /// <summary>
        /// Converts to expando test.
        /// </summary>
        [Fact]
        public void ConvertToExpandoTest()
        {
            var Value = new Example2();
            Value.Example.Test = "test";
            dynamic? Result = Value.ConvertToExpando();
            Assert.NotNull(Result);
            Assert.Equal("test", Result.example.test);
        }
    }
}