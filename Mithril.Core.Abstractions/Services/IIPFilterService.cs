using Microsoft.AspNetCore.Http;

namespace Mithril.Core.Abstractions.Services
{
    /// <summary>
    /// IP filter service
    /// </summary>
    public interface IIPFilterService
    {
        /// <summary>
        /// Checks if the ip associated with the request is allowed.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="policyName">Name of the policy.</param>
        /// <returns>True if it is, false otherwise.</returns>
        bool CheckIPAllowed(HttpContext context, string policyName);
    }
}