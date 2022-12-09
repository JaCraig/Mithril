using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mithril.Core.Abstractions.Services;
using Mithril.Core.Abstractions.Services.Options;

namespace Mithril.Core.Services
{
    /// <summary>
    /// IP Filter service
    /// </summary>
    /// <seealso cref="IIPFilterService"/>
    public class IPFilterService : IIPFilterService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IPFilterService"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="logger">The logger.</param>
        public IPFilterService(IOptions<IPFilterOptions>? options, ILogger<IPFilterService>? logger)
        {
            Options = options?.Value ?? new IPFilterOptions();
            Logger = logger;
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public ILogger<IPFilterService>? Logger { get; }

        /// <summary>
        /// Gets the options.
        /// </summary>
        /// <value>The options.</value>
        private IPFilterOptions Options { get; }

        /// <summary>
        /// Checks if the ip associated with the request is allowed.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="policyName">Name of the policy.</param>
        /// <returns>True if it is, false otherwise.</returns>
        public bool CheckIPAllowed(HttpContext context, string policyName)
        {
            if (!Options.TryGetPolicy(policyName, out var Policy) || Policy is null)
                return true;

            var RemoteIP = context.Connection.RemoteIpAddress;
            if (RemoteIP is null)
                return false;

            Logger?.LogDebug("Request from remote IP address: {RemoteIP}", RemoteIP);

            return Policy.IsAllowed(RemoteIP.ToString());
        }
    }
}