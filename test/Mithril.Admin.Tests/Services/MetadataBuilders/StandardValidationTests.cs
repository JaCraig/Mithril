using Mithril.Admin.Services.MetadataBuilders;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Tests.Services.MetadataBuilders
{
    /// <summary>
    /// Standard validation tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;StandardValidation&gt;" />
    public class StandardValidationTests : TestBaseClass<StandardValidation>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StandardValidationTests"/> class.
        /// </summary>
        public StandardValidationTests()
        {
            TestObject = new StandardValidation();
        }
    }
}