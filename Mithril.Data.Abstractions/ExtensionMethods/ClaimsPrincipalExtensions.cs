using System.Security.Claims;

namespace Mithril.Data.Abstractions.ExtensionMethods
{
    /// <summary>
    /// ClaimsPrincipal extensions
    /// </summary>
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <param name="claimsPrincipal">The claims principal.</param>
        /// <param name="removeDomain">
        /// if set to <c>true</c> [remove the domain]. As an example, for windows auth, the bit
        /// before the '\'. If using email addresses for auth, remove the '@domain.com' bit).
        /// </param>
        /// <returns>The user's name</returns>
        public static string GetName(this ClaimsPrincipal? claimsPrincipal, bool removeDomain = false)
        {
            var UserName = claimsPrincipal?.Identity?.Name ?? "";
            if (string.IsNullOrEmpty(UserName))
                return "";
            if (!removeDomain)
                return UserName;
            var UserNameParts = UserName.Split('\\', StringSplitOptions.RemoveEmptyEntries)[^1];
            if (UserNameParts.Contains('@'))
                return UserName.Split('@', StringSplitOptions.RemoveEmptyEntries)[0];
            return UserNameParts;
        }

        /// <summary>
        /// Gets the tennant ID for the user if it exists.
        /// </summary>
        /// <param name="claimsPrincipal">The claims principal.</param>
        /// <param name="tennantID">The tennant identifier.</param>
        /// <returns>True if it exists, false otherwise.</returns>
        public static bool TryGetTennant(this ClaimsPrincipal? claimsPrincipal, out long tennantID)
        {
            return long.TryParse(claimsPrincipal?.Claims.FirstOrDefault(x => string.Equals(x.Type, "Tenant", StringComparison.OrdinalIgnoreCase))?.Value ?? "", out tennantID);
        }
    }
}