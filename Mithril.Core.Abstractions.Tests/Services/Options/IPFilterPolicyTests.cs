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
            const string Ip = "10.0.0.1";
            var TestObject = new IPFilterPolicy("Default");
            TestObject.SetWhiteList(Ip);
            var Result = TestObject.IsAllowed(Ip);
            Assert.True(Result);
        }

        /// <summary>
        /// When the ip is not allowed then return false.
        /// </summary>
        [Fact]
        public void When_IPIsNotAllowed_Then_ReturnFalse()
        {
            const string Ip = "10.0.0.1";
            var TestObject = new IPFilterPolicy("Default");
            TestObject.SetBlackList(Ip);
            var Result = TestObject.IsAllowed(Ip);
            Assert.False(Result);
        }
    }
}