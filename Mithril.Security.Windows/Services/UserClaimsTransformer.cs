using Microsoft.AspNetCore.Authentication;
using Mithril.Core.Abstractions.ExtensionMethods;
using Mithril.Core.Abstractions.Security.Enums;
using Mithril.Core.Abstractions.Services;
using Mithril.Data.Models.Security;
using System.Security.Claims;

namespace Mithril.Security.Windows.Services
{
    /// <summary>
    /// User claims transformer (adds claims to the ClaimsPrincipal object so we can use the system
    /// built into ASP.Net Core instead of our own)
    /// </summary>
    /// <seealso cref="IClaimsTransformation"/>
    public class UserClaimsTransformer : IClaimsTransformation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserClaimsTransformer"/> class.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        public UserClaimsTransformer(IDataService dataService)
        {
            DataService = dataService;
        }

        /// <summary>
        /// Gets the data service.
        /// </summary>
        /// <value>The data service.</value>
        private IDataService DataService { get; }

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
            if (principal?.HasClaim(x => string.Equals(x.Type, "role", System.StringComparison.OrdinalIgnoreCase)) != false)
                return Task.FromResult(principal)!;
            var CurrentUser = User.Load(principal.GetName(), DataService);
            if (CurrentUser is null)
                return Task.FromResult(principal);
            var NewIdentity = new ClaimsIdentity(principal.Identity, principal.Claims);
            foreach (var Claim in CurrentUser.Claims)
            {
                if (Claim.Type == UserClaimTypes.Role)
                {
                    NewIdentity.AddClaim(new Claim(ClaimTypes.GroupSid, Claim.Value ?? ""));
                }
                NewIdentity.AddClaim(new Claim(Claim.Type ?? "", Claim.Value ?? ""));
            }
            principal.AddIdentity(NewIdentity);
            return Task.FromResult(principal);
        }
    }
}