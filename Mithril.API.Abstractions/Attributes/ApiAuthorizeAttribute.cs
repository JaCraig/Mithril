namespace Mithril.API.Abstractions.Attributes
{
    /// <summary>
    /// API authorize
    /// </summary>
    /// <seealso cref="Attribute"/>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Interface)]
    public class ApiAuthorizeAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiAuthorizeAttribute"/> class.
        /// </summary>
        public ApiAuthorizeAttribute()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiAuthorizeAttribute"/> class.
        /// </summary>
        /// <param name="policyName">Name of the policy.</param>
        public ApiAuthorizeAttribute(string policyName)
        {
            PolicyName = policyName;
        }

        /// <summary>
        /// Gets or sets the name of the policy.
        /// </summary>
        /// <value>The name of the policy.</value>
        public string? PolicyName { get; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>The roles.</value>
        public string? Roles { get; }
    }
}