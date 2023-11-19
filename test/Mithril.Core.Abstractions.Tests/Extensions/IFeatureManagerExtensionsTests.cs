using Microsoft.FeatureManagement;
using Mithril.Core.Abstractions.Extensions;
using Mithril.Core.Abstractions.Modules.Interfaces;
using Mithril.Tests.Helpers;
using NSubstitute;
using Xunit;

namespace Mithril.Core.Abstractions.Tests.Extensions
{
    /// <summary>
    /// Feature manager extension tests
    /// </summary>
    /// <seealso cref="TestBaseClass"/>
    public class IFeatureManagerExtensionsTests : TestBaseClass
    {
        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        protected override Type? ObjectType { get; set; } = typeof(IFeatureManagerExtensions);

        /// <summary>
        /// When the features are enabled are features enabled returns true.
        /// </summary>
        [Fact]
        public void When_FeaturesAreEnabled_AreFeaturesEnabledReturnsTrue()
        {
            IFeature MockFeature = Substitute.For<IFeature>();
            IFeatureManager MockFeatureManager = Substitute.For<IFeatureManager>();
            _ = MockFeatureManager.IsEnabledAsync(MockFeature.Name).Returns(true);

            var Result = MockFeatureManager.AreFeaturesEnabled(MockFeature);

            Assert.True(Result);
        }

        /// <summary>
        /// Whens the features are not enabled are features enabled returns false.
        /// </summary>
        [Fact]
        public void When_FeaturesAreNotEnabled_AreFeaturesEnabledReturnsFalse()
        {
            IFeature MockFeature = Substitute.For<IFeature>();
            IFeatureManager MockFeatureManager = Substitute.For<IFeatureManager>();
            _ = MockFeatureManager.IsEnabledAsync(MockFeature.Name).Returns(false);

            var Result = MockFeatureManager.AreFeaturesEnabled(MockFeature);

            Assert.False(Result);
        }
    }
}