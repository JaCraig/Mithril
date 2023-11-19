using Microsoft.AspNetCore.Authentication;
using Mithril.Data.Abstractions.Enums;
using Mithril.Data.Abstractions.ExtensionMethods;
using Mithril.Data.Abstractions.Interfaces;
using Mithril.Data.Abstractions.Services;
using Mithril.Security.Models;
using System.Security.Claims;

namespace Mithril.Security.Services
{
    /// <summary>
    /// User claims transformer (adds claims to the ClaimsPrincipal object so we can use the system
    /// built into ASP.Net Core instead of our own)
    /// </summary>
    /// <seealso cref="IClaimsTransformation"/>
    /// <remarks>
    /// Initializes a new instance of the <see cref="UserClaimsTransformer"/> class.
    /// </remarks>
    /// <param name="dataService">The data service.</param>
    public class UserClaimsTransformer(IDataService? dataService) : IClaimsTransformation
    {
        /// <summary>
        /// Gets the data service.
        /// </summary>
        /// <value>The data service.</value>
        private IDataService? DataService { get; } = dataService;

        /// <summary>
        /// Provides a central transformation point to change the specified principal.
        /// Note: this will be run on each AuthenticateAsync call, so its safer to return a new
        /// ClaimsPrincipal if your transformation is not idempotent.
        /// </summary>
        /// <param name="principal">
        /// The <see cref="System.Security.Claims.ClaimsPrincipal"/> to transform.
        /// </param>
        /// <returns>The transformed principal.</returns>
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (principal?.HasClaim(x => string.Equals(x.Type, "role", StringComparison.OrdinalIgnoreCase)) != false)
                return Task.FromResult(principal)!;
            var CurrentUser = User.Load(principal.GetName(), DataService);
            if (CurrentUser is null)
                return Task.FromResult(principal);
            var NewIdentity = new ClaimsIdentity(principal.Identity, principal.Claims);
            foreach (IUserClaim? Claim in CurrentUser.Claims)
            {
                if (Claim.Type == UserClaimTypes.Role)
                {
                    NewIdentity.AddClaim(new Claim(ClaimTypes.GroupSid, Claim.Value ?? ""));
                }
                NewIdentity.AddClaim(new Claim(Claim.Type ?? "", Claim.Value ?? ""));
            }
            NewIdentity.AddClaim(new Claim("Tenant", CurrentUser.TenantID.ToString()));
            principal.AddIdentity(NewIdentity);
            return Task.FromResult(principal);
        }
    }
}