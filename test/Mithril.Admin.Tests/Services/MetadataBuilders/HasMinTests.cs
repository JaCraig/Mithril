using Mithril.Admin.Abstractions.DataEditor;
using Mithril.Admin.Abstractions.Interfaces;
using Mithril.Admin.Services;
using Mithril.Admin.Services.MetadataBuilders;
using Mithril.Tests.Helpers;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Mithril.Admin.Tests.Services.MetadataBuilders
{
    /// <summary>
    /// Has min tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;HasMin&gt;"/>
    public class HasMinTests : TestBaseClass<HasMin>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HasMinTests"/> class.
        /// </summary>
        public HasMinTests()
        {
            TestObject = new HasMin();
        }

        /// <summary>
        /// Determines whether [has minimum extract metadata false].
        /// </summary>
        [Fact]
        public void HasMin_ExtractMetadata_False()
        {
            var TestObject = new HasMin();
            var TestProperty = new PropertyMetadata(typeof(TestClass).GetProperty(nameof(TestClass.TestIgnoreProperty)));
            PropertyMetadata? Result = TestObject.ExtractMetadata(TestProperty, new EntityMetadataService(Array.Empty<IMetadataBuilder>()));

            Assert.False(Result?.Metadata.ContainsKey("min"));
        }

        /// <summary>
        /// Determines whether [has minimum extract metadata nullable false].
        /// </summary>
        [Fact]
        public void HasMin_ExtractMetadata_Nullable_False()
        {
            var TestObject = new HasMin();
            var TestProperty = new PropertyMetadata(typeof(TestClass).GetProperty(nameof(TestClass.TestNullableIgnoreProperty)));
            PropertyMetadata? Result = TestObject.ExtractMetadata(TestProperty, new EntityMetadataService(Array.Empty<IMetadataBuilder>()));

            Assert.False(Result?.Metadata.ContainsKey("min"));
        }

        /// <summary>
        /// Determines whether [has minimum extract metadata nullable true].
        /// </summary>
        [Fact]
        public void HasMin_ExtractMetadata_Nullable_True()
        {
            var TestObject = new HasMin();
            var TestProperty = new PropertyMetadata(typeof(TestClass).GetProperty(nameof(TestClass.TestNullableProperty)));
            PropertyMetadata? Result = TestObject.ExtractMetadata(TestProperty, new EntityMetadataService(Array.Empty<IMetadataBuilder>()));

            Assert.True(Result?.Metadata.ContainsKey("min"));
        }

        /// <summary>
        /// Determines whether [has minimum extract metadata true].
        /// </summary>
        [Fact]
        public void HasMin_ExtractMetadata_True()
        {
            var TestObject = new HasMin();
            var TestProperty = new PropertyMetadata(typeof(TestClass).GetProperty(nameof(TestClass.TestProperty)));
            PropertyMetadata? Result = TestObject.ExtractMetadata(TestProperty, new EntityMetadataService(Array.Empty<IMetadataBuilder>()));

            Assert.True(Result?.Metadata.ContainsKey("min"));
        }

        /// <summary>
        /// Determines whether [has minimum extract metadata value].
        /// </summary>
        [Fact]
        public void HasMin_ExtractMetadata_Value()
        {
            var TestObject = new HasMin();
            var TestProperty = new PropertyMetadata(typeof(TestClass).GetProperty(nameof(TestClass.TestProperty)));
            PropertyMetadata? Result = TestObject.ExtractMetadata(TestProperty, new EntityMetadataService(Array.Empty<IMetadataBuilder>()));

            Assert.Equal(10m, Result?.Metadata["min"]);
        }

        /// <summary>
        /// Test class
        /// </summary>
        private class TestClass
        {
            /// <summary>
            /// Gets or sets the test ignore property.
            /// </summary>
            /// <value>The test ignore property.</value>
            public int TestIgnoreProperty { get; set; }

            /// <summary>
            /// Gets or sets the test nullable ignore property.
            /// </summary>
            /// <value>The test nullable ignore property.</value>
            public int? TestNullableIgnoreProperty { get; set; }

            /// <summary>
            /// Gets or sets the test nullable property.
            /// </summary>
            /// <value>The test nullable property.</value>
            [Range(10, 100)]
            public int? TestNullableProperty { get; set; }

            /// <summary>
            /// Gets or sets the test property.
            /// </summary>
            /// <value>The test property.</value>
            [Range(10, 100)]
            public int TestProperty { get; set; }
        }
    }
}