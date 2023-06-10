using Mithril.Core.Abstractions.Services.Options;
using Mithril.Tests.Helpers;

namespace Mithril.Core.Abstractions.Tests.Services.Options
{
    /// <summary>
    /// IP Filter Policy Tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;IPFilterPolicy&gt;"/>
    public class IPFilterPolicyTests : TestBaseClass<IPFilterPolicy>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IPFilterPolicyTests"/> class.
        /// </summary>
        public IPFilterPolicyTests()
        {
            TestObject = new IPFilterPolicy("Default");
            ObjectType = typeof(IPFilterPolicy);
        }

        /// <summary>
        /// When the ip is allowed then return true.
        /// </summary>
        [Fact]
        public void When_IPIsAllowed_Then_ReturnTrue()
        {
            var ip = "10.0.0.1";
            var TestObject = new IPFilterPolicy("Default");
            TestObject.SetWhiteList(ip);
            var result = TestObject.IsAllowed(ip);
            Assert.True(result);
        }

        /// <summary>
        /// When the ip is not allowed then return false.
        /// </summary>
        [Fact]
        public void When_IPIsNotAllowed_Then_ReturnFalse()
        {
            var ip = "10.0.0.1";
            var TestObject = new IPFilterPolicy("Default");
            TestObject.SetBlackList(ip);
            var result = TestObject.IsAllowed(ip);
            Assert.False(result);
        }
    }
}