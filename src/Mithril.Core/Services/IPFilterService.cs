﻿using Microsoft.AspNetCore.Http;
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
    /// <remarks>
    /// Initializes a new instance of the <see cref="IPFilterService"/> class.
    /// </remarks>
    /// <param name="options">The options.</param>
    /// <param name="logger">The logger.</param>
    public class IPFilterService(IOptions<IPFilterOptions>? options, ILogger<IPFilterService>? logger) : IIPFilterService
    {
        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        private ILogger<IPFilterService>? Logger { get; } = logger;

        /// <summary>
        /// Gets the options.
        /// </summary>
        /// <value>The options.</value>
        private IPFilterOptions Options { get; } = options?.Value ?? new IPFilterOptions();

        /// <summary>
        /// Checks if the ip associated with the request is allowed.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="policyName">Name of the policy.</param>
        /// <returns>True if it is, false otherwise.</returns>
        public bool CheckIPAllowed(HttpContext context, string policyName)
        {
            if (!Options.TryGetPolicy(policyName, out IPFilterPolicy? Policy) || Policy is null)
                return true;

            System.Net.IPAddress? RemoteIP = context.Connection.RemoteIpAddress;
            if (RemoteIP is null)
                return false;

            Logger?.LogDebug("Request from remote IP address: {RemoteIP}", RemoteIP);

            return Policy.IsAllowed(RemoteIP.ToString());
        }
    }
}