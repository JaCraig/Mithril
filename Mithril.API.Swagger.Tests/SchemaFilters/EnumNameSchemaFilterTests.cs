using Mithril.API.Swagger.SchemaFilters;
using Mithril.Tests.Helpers;

namespace Mithril.API.Swagger.Tests.SchemaFilters
{
    /// <summary>
    /// EnumNameSchemaFilter tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;EnumNameSchemaFilter&gt;"/>
    public class EnumNameSchemaFilterTests : TestBaseClass<EnumNameSchemaFilter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumNameSchemaFilterTests"/> class.
        /// </summary>
        public EnumNameSchemaFilterTests()
        {
            TestObject = new EnumNameSchemaFilter();
        }
    }
}