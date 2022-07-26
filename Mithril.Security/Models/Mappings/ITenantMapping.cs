﻿using Inflatable.BaseClasses;
using Mithril.Data.Inflatable.Databases;
using Mithril.Security.Abstractions.Interfaces;

namespace Mithril.Security.Models.Mappings
{
    /// <summary>
    /// ITenant mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass{ITenant, DefaultDatabase}"/>
    public class ITenantMapping : MappingBaseClass<ITenant, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ITenantMapping"/> class.
        /// </summary>
        public ITenantMapping()
            : base(merge: true)
        {
            Reference(x => x.DisplayName).WithMaxLength(100);
            ManyToOne(x => x.Users).CascadeChanges();
        }
    }
}