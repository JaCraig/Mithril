﻿using Inflatable.BaseClasses;
using Mithril.Data.Inflatable.Databases;

namespace Mithril.Communication.Models.Mappings
{
    /// <summary>
    /// Message template mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass&lt;MessageTemplate, DefaultDatabase&gt;"/>
    public class MessageTemplateMapping : MappingBaseClass<MessageTemplate, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageTemplateMapping"/> class.
        /// </summary>
        public MessageTemplateMapping()
        {
            Reference(x => x.DisplayName);
        }
    }
}