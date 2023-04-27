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
            Reference(x => x.Display).WithMaxLength(128);
            Reference(x => x.Order);
            ManyToOne(x => x.Parent);
            Map(x => x.Permissions).CascadeChanges();
            Reference(x => x.Url).WithMaxLength(1024);
            Reference(x => x.Icon).WithMaxLength(64);
            Reference(x => x.Description).WithMaxLength(1024);
        }
    }
}