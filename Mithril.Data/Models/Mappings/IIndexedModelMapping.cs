﻿using Inflatable.BaseClasses;
using Mithril.Data.Abstractions.Interfaces;
using Mithril.Data.Inflatable.Databases;

namespace Mithril.Data.Inflatable.Models.Mappings
{
    /// <summary>
    /// IIndexedModel mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass{IIndexedModel, DefaultDatabase}"/>
    public class IIndexedModelMapping : MappingBaseClass<IIndexedModel, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IIndexedModelMapping"/> class.
        /// </summary>
        public IIndexedModelMapping()
            : base(merge: true)
        {
        }
    }
}