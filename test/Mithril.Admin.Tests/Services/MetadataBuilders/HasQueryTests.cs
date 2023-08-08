using Mithril.Admin.Services.MetadataBuilders;
using Mithril.API.Abstractions.Query.Interfaces;
using Mithril.Tests.Helpers;

namespace Mithril.Admin.Tests.Services.MetadataBuilders
{
    /// <summary>
    /// Has query tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;HasQuery&gt;" />
    public class HasQueryTests : TestBaseClass<HasQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HasQueryTests"/> class.
        /// </summary>
        public HasQueryTests()
        {
            TestObject = new HasQuery(Array.Empty<IDropDownQuery>());
            ObjectType = typeof(HasQuery);
        }
    }
}