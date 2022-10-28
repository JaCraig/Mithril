﻿using GraphQL.Types;
using Mithril.API.Abstractions.Query.Interfaces;

namespace Mithril.API.GraphQL.ObjectGraphs
{
    /// <summary>
    /// Composite schema
    /// </summary>
    /// <seealso cref="Schema"/>
    public class CompositeSchema : Schema
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeSchema"/> class.
        /// </summary>
        /// <param name="graphQueries">The graph queries.</param>
        public CompositeSchema(IEnumerable<IQuery> graphQueries)
        {
            Query = new CompositeQuery(graphQueries);
            this.Description = "API schema";
        }
    }
}