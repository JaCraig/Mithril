﻿using Mithril.Admin.Abstractions.BaseClasses;
using Mithril.Admin.Abstractions.DataEditor;
using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Admin.Abstractions.ExtensionMethods;
using Mithril.Admin.Abstractions.Services;
using Mithril.API.Abstractions.Query.Interfaces;
using System.Reflection;

namespace Mithril.Admin.Services.MetadataBuilders
{
    /// <summary>
    /// Determines if the property has a query value.
    /// </summary>
    /// <seealso cref="MetadataBuilderBaseClass" />
    /// <remarks>
    /// Initializes a new instance of the <see cref="HasQuery" /> class.
    /// </remarks>
    /// <param name="dropDownQueries">The drop down queries.</param>
    public class HasQuery(IEnumerable<IDropDownQuery> dropDownQueries) : MetadataBuilderBaseClass
    {
        /// <summary>
        /// Gets the drop down queries.
        /// </summary>
        /// <value>
        /// The drop down queries.
        /// </value>
        private Dictionary<Type, IDropDownQuery> DropDownQueries { get; } = dropDownQueries.ToDictionary(x => x.GetType());

        /// <summary>
        /// Extracts metadata and adds it to the PropertyMetadata object.
        /// </summary>
        /// <param name="propertyMetadata">The property metadata.</param>
        /// <param name="metadataService">The metadata service.</param>
        /// <returns>
        /// The resulting property metadata.
        /// </returns>
        public override PropertyMetadata? ExtractMetadata(PropertyMetadata? propertyMetadata, IEntityMetadataService metadataService)
        {
            if (propertyMetadata?.Property.HasAttribute<QueryAttribute>() != true)
                return propertyMetadata;
            propertyMetadata.Metadata["queryType"] = GenerateQueryName(propertyMetadata.Property, DropDownQueries);
            propertyMetadata.Metadata["queryFilter"] = GenerateQueryFilter(propertyMetadata.Property);
            return propertyMetadata;
        }

        /// <summary>
        /// Generates the query filter.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The query filter.</returns>
        private static string GenerateQueryFilter(PropertyInfo? property)
        {
            QueryAttribute? DropDownType = property?.GetCustomAttribute<QueryAttribute>();
            return DropDownType is null ? "" : DropDownType.Filter ?? "";
        }

        /// <summary>
        /// Generates the query.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="dropDownQueries">The drop down queries.</param>
        /// <returns>
        /// The query
        /// </returns>
        private static string GenerateQueryName(PropertyInfo? property, Dictionary<Type, IDropDownQuery> dropDownQueries)
        {
            QueryAttribute? DropDownType = property?.GetCustomAttribute<QueryAttribute>();
            return DropDownType is null ? "" : !dropDownQueries.TryGetValue(DropDownType.QueryType, out IDropDownQuery? Query) ? "" : Query.Name;
        }
    }
}