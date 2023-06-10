using Mithril.API.Abstractions.Query;
using Mithril.Tests.Helpers;

namespace Mithril.API.Abstractions.Tests.Query
{
    /// <summary>
    /// Arguments tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;Arguments&gt;"/>
    public class ArgumentsTests : TestBaseClass<Arguments>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentsTests"/> class.
        /// </summary>
        public ArgumentsTests()
        {
            TestObject = new Arguments();
        }

        /// <summary>
        /// When the value does not exist get value returns default value.
        /// </summary>
        [Fact]
        public void When_ValueDoesNotExist_GetValueReturnsDefaultValue()
        {
            var TestObject = new Arguments();
            const string TestKey = "TestKey";
            const string TestValue = "TestValue";
            TestObject.Add(TestKey, TestValue);
            var Result = TestObject.GetValue<string>("NonExistentKey");
            Assert.Equal(default, Result);
        }

        /// <summary>
        /// When the value does not exist try get value returns false and default value.
        /// </summary>
        [Fact]
        public void When_ValueDoesNotExist_TryGetValueReturnsFalseAndDefaultValue()
        {
            var TestObject = new Arguments();
            const string TestKey = "TestKey";
            const string TestValue = "TestValue";
            TestObject.Add(TestKey, TestValue);
            var Result = TestObject.TryGetValue<string>("NonExistentKey", out var Value);
            Assert.False(Result);
            Assert.Equal(default, Value);
        }

        /// <summary>
        /// When the value exists get value returns correct value.
        /// </summary>
        [Fact]
        public void When_ValueExists_GetValueReturnsCorrectValue()
        {
            var TestObject = new Arguments();
            const string TestKey = "TestKey";
            const string TestValue = "TestValue";
            TestObject.Add(TestKey, TestValue);
            var Result = TestObject.GetValue<string>(TestKey);
            Assert.Equal(TestValue, Result);
        }

        /// <summary>
        /// Whens the value exists try get value returns true and correct value.
        /// </summary>
        [Fact]
        public void When_ValueExists_TryGetValueReturnsTrueAndCorrectValue()
        {
            var TestObject = new Arguments();
            const string TestKey = "TestKey";
            const string TestValue = "TestValue";
            TestObject.Add(TestKey, TestValue);
            var Result = TestObject.TryGetValue<string>(TestKey, out var Value);
            Assert.True(Result);
            Assert.Equal(TestValue, Value);
        }
    }
}