namespace Mithril.API.Abstractions.Attributes
{
    /// <summary>
    /// API Description attribute.
    /// </summary>
    /// <seealso cref="System.Attribute"/>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Interface)]
    public class ApiDescriptionAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiDescriptionAttribute"/> class.
        /// </summary>
        public ApiDescriptionAttribute()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiDescriptionAttribute"/> class.
        /// </summary>
        /// <param name="description">The description.</param>
        public ApiDescriptionAttribute(string description)
        {
            Description = description;
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        public string? Description { get; }
    }
}