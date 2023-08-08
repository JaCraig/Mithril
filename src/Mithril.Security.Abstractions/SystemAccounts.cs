using System.Security.Claims;

namespace Mithril.Security.Abstractions
{
    /// <summary>
    /// System accounts
    /// </summary>
    public class SystemAccounts
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SystemAccounts"/> class.
        /// </summary>
        public SystemAccounts()
        {
            var TempIdentity = new ClaimsIdentity();
            TempIdentity.AddClaim(new Claim(ClaimTypes.Name, "system_account"));
            TempIdentity.AddClaim(new Claim("Tenant", "1"));
            SystemClaimsPrincipal = new ClaimsPrincipal();
            SystemClaimsPrincipal.AddIdentity(TempIdentity);

            TempIdentity = new ClaimsIdentity();
            TempIdentity.AddClaim(new Claim(ClaimTypes.Name, "anonymous_account"));
            TempIdentity.AddClaim(new Claim("Tenant", "1"));
            AnonymousClaimsPrincipal = new ClaimsPrincipal();
            AnonymousClaimsPrincipal.AddIdentity(TempIdentity);
        }

        /// <summary>
        /// Gets the anonymous claims principal.
        /// </summary>
        /// <value>The anonymous claims principal.</value>
        public ClaimsPrincipal AnonymousClaimsPrincipal { get; }

        /// <summary>
        /// Gets the system claims principal.
        /// </summary>
        /// <value>The system claims principal.</value>
        public ClaimsPrincipal SystemClaimsPrincipal { get; }
    }
}