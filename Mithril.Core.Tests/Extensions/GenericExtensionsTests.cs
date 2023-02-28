using Mithril.Core.Extensions;
using Mithril.Tests.Helpers;

namespace Mithril.Core.Tests.Extensions
{
    /// <summary>
    /// Generic extension method tests
    /// </summary>
    /// <seealso cref="TestBaseClass"/>
    public class GenericExtensionsTests : TestBaseClass
    {
        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        protected override Type? ObjectType { get; set; } = typeof(GenericExtensions);

        /// <summary>
        /// Whens the predicate is false method is not executed.
        /// </summary>
        [Fact]
        public void When_PredicateIsFalse_MethodIsNotExecuted()
        {
            var Obj = new object();
            const bool Predicate = false;
            Func<object, object> Method = _ => new object();

            var result = Obj.When(Predicate, Method);

            Assert.Equal(Obj, result);
        }

        /// <summary>
        /// Whens the predicate is true method is executed.
        /// </summary>
        [Fact]
        public void When_PredicateIsTrue_MethodIsExecuted()
        {
            var Obj = new object();
            const bool Predicate = true;
            Func<object, object> Method = _ => new object();

            var result = Obj.When(Predicate, Method);

            Assert.NotEqual(Obj, result);
        }
    }
}