using Mithril.Data.Abstractions.BaseClasses;

namespace Mithril.Data.Abstractions.Enums
{
    /// <summary>
    /// Claim types that are available
    /// </summary>
    /// <seealso cref="LookUpEnumBaseClass{UserClaimTypes}"/>
    public class UserClaimTypes : LookUpEnumBaseClass<UserClaimTypes>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserClaimTypes"/> class.
        /// </summary>
        public UserClaimTypes()
            : this("")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserClaimTypes"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public UserClaimTypes(string name)
            : base(name, "fa-lock")
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
    }
}