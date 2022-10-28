using BigBook.Patterns.BaseClasses;

namespace Mithril.Security.Abstractions.Enums
{
    /// <summary>
    /// Common system permissions.
    /// </summary>
    /// <seealso cref="StringEnumBaseClass{SystemPermissions}"/>
    public class SystemPermissions : StringEnumBaseClass<SystemPermissions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SystemPermissions"/> class.
        /// </summary>
        public SystemPermissions()
            : base("")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemPermissions"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public SystemPermissions(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Gets the admin only.
        /// </summary>
        /// <value>The admin only.</value>
        public static SystemPermissions AdminOnly { get; } = new SystemPermissions("Admin Only");

        /// <summary>
        /// Gets the content editor.
        /// </summary>
        /// <value>The content editor.</value>
        public static SystemPermissions ContentEditor { get; } = new SystemPermissions("Content Editor");

        /// <summary>
        /// Gets the data editor.
        /// </summary>
        /// <value>The data editor.</value>
        public static SystemPermissions DataEditor { get; } = new SystemPermissions("Data Editor");

        /// <summary>
        /// Gets the form editor.
        /// </summary>
        /// <value>The form editor.</value>
        public static SystemPermissions FormEditor { get; } = new SystemPermissions("Form Editor");

        /// <summary>
        /// Gets the report editor.
        /// </summary>
        /// <value>The report editor.</value>
        public static SystemPermissions ReportEditor { get; } = new SystemPermissions("Report Editor");

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <value>The user.</value>
        public static SystemPermissions User { get; } = new SystemPermissions("User");
    }
}