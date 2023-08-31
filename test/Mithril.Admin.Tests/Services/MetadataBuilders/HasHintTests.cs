using Mithril.Admin.Abstractions.DataEditor;
using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Admin.Abstractions.Interfaces;
using Mithril.Admin.Services;
using Mithril.Admin.Services.MetadataBuilders;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Tests.Services.MetadataBuilders
{
    /// <summary>
    /// Has hint tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;HasHint&gt;" />
    public class HasHintTests : TestBaseClass<HasHint>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HasHintTests"/> class.
        /// </summary>
        public HasHintTests()
        {
            TestObject = new HasHint();
        }

        /// <summary>
        /// Determines whether [has hint extract metadata false].
        /// </summary>
        [Fact]
        public void HasHint_ExtractMetadata_False()
        {
            var TestObject = new HasHint();
            var TestProperty = new PropertyMetadata(typeof(TestClass).GetProperty(nameof(TestClass.TestIgnoreProperty)));
            var Result = TestObject.ExtractMetadata(TestProperty, new EntityMetadataService(Array.Empty<IMetadataBuilder>()));

            Assert.False(Result?.Metadata.ContainsKey("hint"));
        }

        /// <summary>
        /// Determines whether [has hint extract metadata false2].
        /// </summary>
        [Fact]
        public void HasHint_ExtractMetadata_True()
        {
            var TestObject = new HasHint();
            var TestProperty = new PropertyMetadata(typeof(TestClass).GetProperty(nameof(TestClass.TestProperty)));
            var Result = TestObject.ExtractMetadata(TestProperty, new EntityMetadataService(Array.Empty<IMetadataBuilder>()));

            Assert.True(Result?.Metadata.ContainsKey("hint"));
        }

        /// <summary>
        /// Determines whether [has hint extract metadata value].
        /// </summary>
        [Fact]
        public void HasHint_ExtractMetadata_Value()
        {
            var TestObject = new HasHint();
            var TestProperty = new PropertyMetadata(typeof(TestClass).GetProperty(nameof(TestClass.TestProperty)));
            var Result = TestObject.ExtractMetadata(TestProperty, new EntityMetadataService(Array.Empty<IMetadataBuilder>()));

            Assert.Equal("Example hint", Result?.Metadata["hint"]);
        }

        /// <summary>
        /// Test class
        /// </summary>
        private class TestClass
        {
            /// <summary>
            /// Gets or sets the test ignore property.
            /// </summary>
            /// <value>
            /// The test ignore property.
            /// </value>
            public string? TestIgnoreProperty { get; set; }

            /// <summary>
            /// Gets or sets the test property.
            /// </summary>
            /// <value>
            /// The test property.
            /// </value>
            [Hint("Example hint")]
            public string? TestProperty { get; set; }
        }
    }
}