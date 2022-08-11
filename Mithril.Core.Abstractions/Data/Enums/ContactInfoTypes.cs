using BigBook.Patterns.BaseClasses;
using Mithril.Core.Abstractions.Data.Models;
using Mithril.Core.Abstractions.Services;
using System.Globalization;

namespace Mithril.Core.Abstractions.Data.Enums
{
    /// <summary>
    /// ContactInfo types
    /// </summary>
    /// <seealso cref="BigBook.Patterns.BaseClasses.StringEnumBaseClass{ContactInfoTypes}"/>
    public class ContactInfoType : StringEnumBaseClass<ContactInfoType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContactInfoType"/> class.
        /// </summary>
        public ContactInfoType()
            : base("")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactInfoType"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="icon">The icon.</param>
        public ContactInfoType(string name, string icon = "fa-phone")
            : base(name)
        {
            Icon = icon;
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

        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <value>The icon.</value>
        public string? Icon { get; }

        /// <summary>
        /// The name mapping
        /// </summary>
        /// <value>The name mapping.</value>
        private static Dictionary<string, ContactInfoType> NameMapping { get; } = new Dictionary<string, ContactInfoType>
        {
            [Extension.ToString().ToUpper(CultureInfo.InvariantCulture)] = Extension,
            [Email.ToString().ToUpper(CultureInfo.InvariantCulture)] = Email,
            [Phone.ToString().ToUpper(CultureInfo.InvariantCulture)] = Phone,
            [WebSite.ToString().ToUpper(CultureInfo.InvariantCulture)] = WebSite,
            [GitHub.ToString().ToUpper(CultureInfo.InvariantCulture)] = GitHub,
            [Twitter.ToString().ToUpper(CultureInfo.InvariantCulture)] = Twitter,
            [LinkedIn.ToString().ToUpper(CultureInfo.InvariantCulture)] = LinkedIn,
            [Facebook.ToString().ToUpper(CultureInfo.InvariantCulture)] = Facebook,
            [CellPhone.ToString().ToUpper(CultureInfo.InvariantCulture)] = CellPhone,
            [Fax.ToString().ToUpper(CultureInfo.InvariantCulture)] = Fax,
        };

        /// <summary>
        /// Gets the type of the contact information.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The contact info type specified.</returns>
        public static ContactInfoType? GetContactInfoType(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            var KeyName = name.ToUpper(CultureInfo.InvariantCulture).Replace("-", "", StringComparison.OrdinalIgnoreCase);
            return NameMapping.ContainsKey(KeyName) ? NameMapping[KeyName] : new ContactInfoType(name);
        }

        /// <summary>
        /// Gets the enum types.
        /// </summary>
        /// <returns>The various enum types.</returns>
        public static IEnumerable<ContactInfoType> GetContactInfoTypes()
        {
            return NameMapping.Values;
        }

        /// <summary>
        /// Setups the look ups asynchronously.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        /// <returns>Async task.</returns>
        public static Task SetupLookUpsAsync(IDataService dataService)
        {
            if (dataService is null)
                return Task.CompletedTask;
            List<Task> Tasks = new List<Task>();
            foreach (var TempType in GetContactInfoTypes())
            {
                Tasks.Add(LookUp.LoadOrCreateAsync(TempType, LookUpTypeEnum.ContactInfoType, TempType?.Icon ?? "", dataService));
            }
            return Task.WhenAll(Tasks);
        }
    }
}