using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mithril.Core.Abstractions.Services;
using System.Net;

namespace Mithril.Core.Abstractions.Mvc.Attributes
{
    /// <summary>
    /// IP Filter attribute
    /// </summary>
    /// <seealso cref="Attribute"/>
    /// <remarks>
    /// Initializes a new instance of the <see cref="IPFilterAttribute"/> class.
    /// </remarks>
    /// <param name="policyName">Name of the policy.</param>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class IPFilterAttribute(string policyName) : ActionFilterAttribute
    {
        /// <summary>
        /// Gets the name of the policy.
        /// </summary>
        /// <value>The name of the policy.</value>
        private string PolicyName { get; } = policyName;

        /// <inheritdoc/>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context is null)
                return;
            IIPFilterService? IPFilterService = context.HttpContext.RequestServices.GetService<IIPFilterService>();
            ILogger<IPFilterAttribute>? Logger = context.HttpContext.RequestServices.GetService<ILogger<IPFilterAttribute>>();
            if (IPFilterService is null || Logger is null)
                return;

            if (!IPFilterService.CheckIPAllowed(context.HttpContext, PolicyName))
            {
                Logger.LogWarning("Request from remote IP address blocked: {RemoteIP}", context.HttpContext.Connection.RemoteIpAddress);
                context.Result = new StatusCodeResult((int)HttpStatusCode.Forbidden);
                return;
            }
            base.OnActionExecuting(context);
        }
    }
}