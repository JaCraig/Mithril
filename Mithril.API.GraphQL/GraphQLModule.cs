using Mithril.Core.Abstractions.Modules.BaseClasses;

namespace Mithril.API.GraphQL
{
    /// <summary>
    /// API Module
    /// </summary>
    /// <seealso cref="ModuleBaseClass&lt;APIModule&gt;"/>
    public class GraphQLModule : ModuleBaseClass<GraphQLModule>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphQLModule"/> class.
        /// </summary>
        public GraphQLModule()
            : base("GraphQL Module", "API", "API", "GraphQL")
        {
        }
    }
}