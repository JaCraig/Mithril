namespace Mithril.API.Abstractions.Attributes
{
    /// <summary>
    /// API Deprication attribute.
    /// </summary>
    /// <seealso cref="System.Attribute"/>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Interface)]
    public class ApiDepricationReasonAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiDepricationReasonAttribute"/> class.
        /// </summary>
        public ApiDepricationReasonAttribute()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiDepricationReasonAttribute"/> class.
        /// </summary>
        /// <param name="depricationReason">The deprication reason.</param>
        public ApiDepricationReasonAttribute(string depricationReason)
        {
            DepricationReason = depricationReason;
        }

        /// <summary>
        /// Gets the Deprication.
        /// </summary>
        /// <value>The Deprication.</value>
        public string? DepricationReason { get; }
    }
}