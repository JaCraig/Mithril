namespace Mithril.Admin.Abstractions.DataEditor.Attributes
{
    /// <summary>
    /// Determines if the system should treat this as an upload
    /// </summary>
    /// <seealso cref="Attribute" />
    /// <remarks>
    /// Initializes a new instance of the <see cref="UploadAttribute" /> class.
    /// </remarks>
    /// <param name="accept">The accept.</param>
    /// <param name="allowMultiple">if set to <c>true</c> [allow multiple].</param>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class UploadAttribute(string accept, bool allowMultiple = false) : Attribute
    {
        /// <summary>
        /// Gets the file types accepted.
        /// </summary>
        /// <value>The acceptted file types.</value>
        public string Accept { get; } = accept ?? string.Empty;

        /// <summary>
        /// Gets a value indicating whether [allow multiple].
        /// </summary>
        /// <value><c>true</c> if [allow multiple]; otherwise, <c>false</c>.</value>
        public bool AllowMultiple { get; } = allowMultiple;
    }
}