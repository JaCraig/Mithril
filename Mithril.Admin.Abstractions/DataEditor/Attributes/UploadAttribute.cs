namespace Mithril.Admin.Abstractions.DataEditor.Attributes
{
    /// <summary>
    /// Determines if the system should treat this as an upload
    /// </summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class UploadAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UploadAttribute" /> class.
        /// </summary>
        /// <param name="accept">The accept.</param>
        /// <param name="allowMultiple">if set to <c>true</c> [allow multiple].</param>
        public UploadAttribute(string accept, bool allowMultiple = false)
        {
            Accept = accept ?? string.Empty;
            AllowMultiple = allowMultiple;
        }

        /// <summary>
        /// Gets the file types accepted.
        /// </summary>
        /// <value>The acceptted file types.</value>
        public string Accept { get; }

        /// <summary>
        /// Gets a value indicating whether [allow multiple].
        /// </summary>
        /// <value><c>true</c> if [allow multiple]; otherwise, <c>false</c>.</value>
        public bool AllowMultiple { get; }
    }
}