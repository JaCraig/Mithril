using Inflatable.BaseClasses;
using Mithril.Data.Abstractions.Databases;

namespace Mithril.Navigation.Models.Mappings
{
    /// <summary>
    /// Menu item mapping
    /// </summary>
    /// <seealso cref="Inflatable.BaseClasses.MappingBaseClass{MenuItem, DefaultDatabase}"/>
    public class MenuItemMapping : MappingBaseClass<MenuItem, DefaultDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemMapping"/> class.
        /// </summary>
        public MenuItemMapping()
            : base(merge: true)
        {
            _ = Reference(x => x.Display).WithMaxLength(128);
            _ = Reference(x => x.Order);
            _ = ManyToOne(x => x.Parent);
            _ = Map(x => x.Permissions).CascadeChanges();
            _ = Reference(x => x.Url).WithMaxLength(1024);
            _ = Reference(x => x.Icon).WithMaxLength(64);
            _ = Reference(x => x.Description).WithMaxLength(1024);
        }
    }
}