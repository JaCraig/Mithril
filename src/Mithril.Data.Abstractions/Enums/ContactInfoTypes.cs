using Mithril.Data.Abstractions.BaseClasses;

namespace Mithril.Data.Abstractions.Enums
{
    /// <summary>
    /// ContactInfo types
    /// </summary>
    /// <seealso cref="LookUpEnumBaseClass{ContactInfoTypes}"/>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ContactInfoType"/> class.
    /// </remarks>
    /// <param name="name">The name.</param>
    /// <param name="icon">The icon.</param>
    public class ContactInfoType(string name, string icon = "fa-phone") : LookUpEnumBaseClass<ContactInfoType>(name, icon)
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContactInfoType"/> class.
        /// </summary>
        public ContactInfoType()
            : this("")
        {
        }

        /// <summary>
        /// Gets the mobile.
        /// </summary>
        /// <value>The mobile.</value>
        public static ContactInfoType CellPhone { get; } = new ContactInfoType("Cell Phone", "fa-mobile");

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <value>The email.</value>
        public static ContactInfoType Email { get; } = new ContactInfoType("Email", "fa-envelope");

        /// <summary>
        /// Gets the extension.
        /// </summary>
        /// <value>The extension.</value>
        public static ContactInfoType Extension { get; } = new ContactInfoType("Extension", "fa-phone");

        /// <summary>
        /// Gets the facebook.
        /// </summary>
        /// <value>The facebook.</value>
        public static ContactInfoType Facebook { get; } = new ContactInfoType("Facebook", "fa-facebook");

        /// <summary>
        /// Gets the fax.
        /// </summary>
        /// <value>The fax.</value>
        public static ContactInfoType Fax { get; } = new ContactInfoType("Fax", "fa-fax");

        /// <summary>
        /// Gets the git hub.
        /// </summary>
        /// <value>The git hub.</value>
        public static ContactInfoType GitHub { get; } = new ContactInfoType("GitHub", "fa-github");

        /// <summary>
        /// Gets the linked in.
        /// </summary>
        /// <value>The linked in.</value>
        public static ContactInfoType LinkedIn { get; } = new ContactInfoType("LinkedIn", "fa-linkedin");

        /// <summary>
        /// Gets the phone.
        /// </summary>
        /// <value>The phone.</value>
        public static ContactInfoType Phone { get; } = new ContactInfoType("Phone", "fa-phone");

        /// <summary>
        /// Gets the twitter.
        /// </summary>
        /// <value>The twitter.</value>
        public static ContactInfoType Twitter { get; } = new ContactInfoType("Twitter", "fa-twitter");

        /// <summary>
        /// Gets the web site.
        /// </summary>
        /// <value>The web site.</value>
        public static ContactInfoType WebSite { get; } = new ContactInfoType("Web Site", "fa-globe");
    }
}