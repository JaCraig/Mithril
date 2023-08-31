using Inflatable.BaseClasses;
using Mithril.Data.Abstractions.Databases;

namespace Mithril.Navigation.Models.Mappings
{
    /// <summary>
    /// Menu mapping
    /// </summary>
    /// <seealso cref="MappingBaseClass{Menu, DefaultDatabase}"/>
    public class MenuMapping : MappingBaseClass<Menu, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuMapping"/> class.
        /// </summary>
        public MenuMapping()
        {
            _ = Reference(x => x.Display).WithMaxLength(64);
            _ = ManyToOne(x => x.Items).CascadeChanges();
            _ = Map(x => x.Permissions).CascadeChanges();
        }
    }
}