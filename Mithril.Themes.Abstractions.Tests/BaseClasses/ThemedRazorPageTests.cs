using Mithril.Tests.Helpers;
using Mithril.Themes.Abstractions.BaseClasses;

namespace Mithril.Themes.Abstractions.Tests.BaseClasses
{
    /// <summary>
    /// Test model
    /// </summary>
    public class TestModel
    { }

    /// <summary>
    /// Test razor page
    /// </summary>
    /// <seealso cref="ThemedRazorPage&lt;TestModel&gt;"/>
    public class TestThemedRazorPage : ThemedRazorPage<TestModel>
    {
    }

    /// <summary>
    /// Themed razor page base class tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;TestThemedRazorPage&gt;"/>
    public class ThemedRazorPageTests : TestBaseClass<TestThemedRazorPage>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThemedRazorPageTests"/> class.
        /// </summary>
        public ThemedRazorPageTests()
        {
            TestObject = new TestThemedRazorPage();
        }
    }
}