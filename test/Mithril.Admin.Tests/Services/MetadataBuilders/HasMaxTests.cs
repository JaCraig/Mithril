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
    /// Has max tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;HasMax&gt;"/>
    public class HasMaxTests : TestBaseClass<HasMax>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HasMaxTests"/> class.
        /// </summary>
        public HasMaxTests()
        {
            TestObject = new HasMax();
        }

        /// <summary>
        /// Determines whether [has maximum extract metadata false].
        /// </summary>
        [Fact]
        public void HasMax_ExtractMetadata_False()
        {
            var TestObject = new HasMax();
            var TestProperty = new PropertyMetadata(typeof(TestClass).GetProperty(nameof(TestClass.TestIgnoreProperty)));
            PropertyMetadata? Result = TestObject.ExtractMetadata(TestProperty, new EntityMetadataService(Array.Empty<IMetadataBuilder>()));

            Assert.False(Result?.Metadata.ContainsKey("max"));
        }

        /// <summary>
        /// Determines whether [has maximum extract metadata nullable false].
        /// </summary>
        [Fact]
        public void HasMax_ExtractMetadata_Nullable_False()
        {
            var TestObject = new HasMax();
            var TestProperty = new PropertyMetadata(typeof(TestClass).GetProperty(nameof(TestClass.TestNullableIgnoreProperty)));
            PropertyMetadata? Result = TestObject.ExtractMetadata(TestProperty, new EntityMetadataService(Array.Empty<IMetadataBuilder>()));

            Assert.False(Result?.Metadata.ContainsKey("max"));
        }

        /// <summary>
        /// Determines whether [has maximum extract metadata nullable true].
        /// </summary>
        [Fact]
        public void HasMax_ExtractMetadata_Nullable_True()
        {
            var TestObject = new HasMax();
            var TestProperty = new PropertyMetadata(typeof(TestClass).GetProperty(nameof(TestClass.TestNullableProperty)));
            PropertyMetadata? Result = TestObject.ExtractMetadata(TestProperty, new EntityMetadataService(Array.Empty<IMetadataBuilder>()));

            Assert.True(Result?.Metadata.ContainsKey("max"));
        }

        /// <summary>
        /// Determines whether [has maximum extract metadata true].
        /// </summary>
        [Fact]
        public void HasMax_ExtractMetadata_True()
        {
            var TestObject = new HasMax();
            var TestProperty = new PropertyMetadata(typeof(TestClass).GetProperty(nameof(TestClass.TestProperty)));
            PropertyMetadata? Result = TestObject.ExtractMetadata(TestProperty, new EntityMetadataService(Array.Empty<IMetadataBuilder>()));

            Assert.True(Result?.Metadata.ContainsKey("max"));
        }

        /// <summary>
        /// Determines whether [has maximum extract metadata value].
        /// </summary>
        [Fact]
        public void HasMax_ExtractMetadata_Value()
        {
            var TestObject = new HasMax();
            var TestProperty = new PropertyMetadata(typeof(TestClass).GetProperty(nameof(TestClass.TestProperty)));
            PropertyMetadata? Result = TestObject.ExtractMetadata(TestProperty, new EntityMetadataService(Array.Empty<IMetadataBuilder>()));

            Assert.Equal(100m, Result?.Metadata["max"]);
        }

        /// <summary>
        /// Test class
        /// </summary>
        public class TestClass
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