using BigBook.Patterns.BaseClasses;
using System.Globalization;

namespace Mithril.Data.Abstractions.Enums
{
    /// <summary>
    /// Claim types that are available
    /// </summary>
    /// <seealso cref="StringEnumBaseClass{UserClaimTypes}"/>
    public class UserClaimTypes : StringEnumBaseClass<UserClaimTypes>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserClaimTypes"/> class.
        /// </summary>
        public UserClaimTypes()
            : base("")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserClaimTypes"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public UserClaimTypes(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Gets the ad group claim type.
        /// </summary>
        /// <value>The claim corresponding to the AD group.</value>
        public static UserClaimTypes ADGroup { get; } = new UserClaimTypes("ADGroup");

        /// <summary>
        /// Gets the attorney level.
        /// </summary>
        /// <value>The attorney level.</value>
        public static UserClaimTypes AttorneyLevel { get; } = new UserClaimTypes("AttorneyLevel");

        /// <summary>
        /// The committee claim type
        /// </summary>
        /// <value>The claim corresponding to the committee.</value>
        public static UserClaimTypes Committee { get; } = new UserClaimTypes("Committee");

        /// <summary>
        /// The group claim type
        /// </summary>
        /// <value>The claim corresponding to the group.</value>
        public static UserClaimTypes Group { get; } = new UserClaimTypes("Group");

        /// <summary>
        /// The manager claim type
        /// </summary>
        /// <value>The claim corresponding to who the person's manager is.</value>
        public static UserClaimTypes Manager { get; } = new UserClaimTypes("Manager");

        /// <summary>
        /// The manager for claim type
        /// </summary>
        /// <value>The claim corresponding to who the person is a manager for.</value>
        public static UserClaimTypes ManagerFor { get; } = new UserClaimTypes("Manager For");

        /// <summary>
        /// The office claim type
        /// </summary>
        /// <value>The claim corresponding to the person's office.</value>
        public static UserClaimTypes Office { get; } = new UserClaimTypes("Office");

        /// <summary>
        /// The role claim type
        /// </summary>
        /// <value>The claim corresponding to the role.</value>
        public static UserClaimTypes Role { get; } = new UserClaimTypes("Role");

        /// <summary>
        /// The secretary claim type
        /// </summary>
        /// <value>The claim corresponding to who the person's secretary is.</value>
        public static UserClaimTypes Secretary { get; } = new UserClaimTypes("Secretary");

        /// <summary>
        /// The secretary for claim type
        /// </summary>
        /// <value>The claim corresponding to who the person is a secretary for.</value>
        public static UserClaimTypes SecretaryFor { get; } = new UserClaimTypes("Secretary For");

        /// <summary>
        /// Gets the section.
        /// </summary>
        /// <value>The section.</value>
        public static UserClaimTypes Section { get; } = new UserClaimTypes("Section");

        /// <summary>
        /// The section head claim type
        /// </summary>
        /// <value>The claim corresponding to the person's section head.</value>
        public static UserClaimTypes SectionHead { get; } = new UserClaimTypes("SectionHead");

        /// <summary>
        /// The section head for claim type
        /// </summary>
        /// <value>The claim corresponding to who the person is section head for.</value>
        public static UserClaimTypes SectionHeadFor { get; } = new UserClaimTypes("SectionHead For");

        /// <summary>
        /// The staff type claim type
        /// </summary>
        /// <value>The claim corresponding to the person's staff type.</value>
        public static UserClaimTypes StaffType { get; } = new UserClaimTypes("StaffType");

        /// <summary>
        /// The user name claim type
        /// </summary>
        /// <value>The claim corresponding to the user name.</value>
        public static UserClaimTypes UserName { get; } = new UserClaimTypes("UserName");

        /// <summary>
        /// The name mapping
        /// </summary>
        private static Dictionary<string, UserClaimTypes> NameMapping { get; } = new Dictionary<string, UserClaimTypes>
        {
            [UserName.ToString().ToUpper(CultureInfo.InvariantCulture)] = UserName,
            [StaffType.ToString().ToUpper(CultureInfo.InvariantCulture)] = StaffType,
            [SectionHeadFor.ToString().ToUpper(CultureInfo.InvariantCulture)] = SectionHeadFor,
            [SectionHead.ToString().ToUpper(CultureInfo.InvariantCulture)] = SectionHead,
            [Section.ToString().ToUpper(CultureInfo.InvariantCulture)] = Section,
            [SecretaryFor.ToString().ToUpper(CultureInfo.InvariantCulture)] = SecretaryFor,
            [Secretary.ToString().ToUpper(CultureInfo.InvariantCulture)] = Secretary,
            [Role.ToString().ToUpper(CultureInfo.InvariantCulture)] = Role,
            [Office.ToString().ToUpper(CultureInfo.InvariantCulture)] = Office,
            [ManagerFor.ToString().ToUpper(CultureInfo.InvariantCulture)] = ManagerFor,
            [Manager.ToString().ToUpper(CultureInfo.InvariantCulture)] = Manager,
            [Group.ToString().ToUpper(CultureInfo.InvariantCulture)] = Group,
            [Committee.ToString().ToUpper(CultureInfo.InvariantCulture)] = Committee,
            [AttorneyLevel.ToString().ToUpper(CultureInfo.InvariantCulture)] = AttorneyLevel,
            [ADGroup.ToString().ToUpper(CultureInfo.InvariantCulture)] = ADGroup
        };

        /// <summary>
        /// Gets the type of the contact information.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The contact info type specified.</returns>
        public static UserClaimTypes? GetUserClaimType(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            var KeyName = name.ToUpper(CultureInfo.InvariantCulture).Replace("-", "", StringComparison.OrdinalIgnoreCase);
            return NameMapping.ContainsKey(KeyName) ? NameMapping[KeyName] : new UserClaimTypes(name);
        }

        /// <summary>
        /// Gets the enum types.
        /// </summary>
        /// <returns>The various enum types.</returns>
        public static IEnumerable<UserClaimTypes> GetUserClaimTypes() => NameMapping.Values;
    }
}