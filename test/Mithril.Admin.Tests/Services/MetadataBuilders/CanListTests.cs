using Mithril.Admin.Abstractions.DataEditor;
using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Admin.Abstractions.Interfaces;
using Mithril.Admin.Services;
using Mithril.Admin.Services.MetadataBuilders;
using Mithril.Tests.Helpers;
using System.Text.Json.Serialization;
using Xunit;

namespace Mithril.Admin.Tests.Services.MetadataBuilders
{
    /// <summary>
    /// Can list tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;CanList&gt;"/>
    public class CanListTests : TestBaseClass<CanList>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CanListTests"/> class.
        /// </summary>
        public CanListTests()
        {
            TestObject = new CanList();
        }

        /// <summary>
        /// Determines whether this instance [can list extract metadata false].
        /// </summary>
        [Fact]
        public void CanList_ExtractMetadata_False()
        {
            var TestObject = new CanList();
            var TestProperty = new PropertyMetadata(typeof(TestClass).GetProperty(nameof(TestClass.TestIgnoreProperty)));
            PropertyMetadata? Result = TestObject.ExtractMetadata(TestProperty, new EntityMetadataService(Array.Empty<IMetadataBuilder>()));

            Assert.False((bool?)Result?.Metadata["canList"]);
        }

        /// <summary>
        /// Determines whether this instance [can list extract metadata false2].
        /// </summary>
        [Fact]
        public void CanList_ExtractMetadata_False2()
        {
            var TestObject = new CanList();
            var TestProperty = new PropertyMetadata(typeof(TestClass).GetProperty(nameof(TestClass.TestIgnoreProperty2)));
            PropertyMetadata? Result = TestObject.ExtractMetadata(TestProperty, new EntityMetadataService(Array.Empty<IMetadataBuilder>()));

            Assert.False((bool?)Result?.Metadata["canList"]);
        }

        /// <summary>
        /// Determines whether this instance [can list extract metadata true].
        /// </summary>
        [Fact]
        public void CanList_ExtractMetadata_True()
        {
            var TestObject = new CanList();
            var TestProperty = new PropertyMetadata(typeof(TestClass).GetProperty(nameof(TestClass.TestProperty)));
            PropertyMetadata? Result = TestObject.ExtractMetadata(TestProperty, new EntityMetadataService(Array.Empty<IMetadataBuilder>()));

            Assert.True((bool?)Result?.Metadata["canList"]);
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
            [JsonIgnore]
            public int TestIgnoreProperty { get; set; }

            /// <summary>
            /// Gets or sets the test ignore property2.
            /// </summary>
            /// <value>The test ignore property2.</value>
            [DoNotList]
            public int TestIgnoreProperty2 { get; set; }

            /// <summary>
            /// Gets or sets the test property.
            /// </summary>
            /// <value>The test property.</value>
            public int TestProperty { get; set; }
        }
    }
}