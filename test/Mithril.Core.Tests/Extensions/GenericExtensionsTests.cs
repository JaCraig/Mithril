﻿using Mithril.Core.Extensions;
using Mithril.Tests.Helpers;
using Xunit;

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
            static object Method(object _)
            {
                return new();
            }

            var Result = Obj.When(Predicate, Method);

            Assert.Equal(Obj, Result);
        }

        /// <summary>
        /// Whens the predicate is true method is executed.
        /// </summary>
        [Fact]
        public void When_PredicateIsTrue_MethodIsExecuted()
        {
            var Obj = new object();
            const bool Predicate = true;
            static object Method(object _)
            {
                return new();
            }

            var Result = Obj.When(Predicate, Method);

            Assert.NotEqual(Obj, Result);
        }
    }
}