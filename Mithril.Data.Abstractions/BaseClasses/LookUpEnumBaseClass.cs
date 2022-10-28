using BigBook.Patterns.BaseClasses;

namespace Mithril.Data.Abstractions.BaseClasses
{
    /// <summary>
    /// LookUp "enum" base class
    /// </summary>
    /// <seealso cref="StringEnumBaseClass&lt;LookUpTypeEnum&gt;"/>
    public abstract class LookUpEnumBaseClass<TClass> : StringEnumBaseClass<TClass>
        where TClass : LookUpEnumBaseClass<TClass>, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LookUpEnumBaseClass"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="icon">The icon.</param>
        protected LookUpEnumBaseClass(string name, string icon)
            : base(name)
        {
            Icon = icon;
        }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>The icon.</value>
        public string? Icon { get; }
    }
}