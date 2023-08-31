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
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class IPFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IPFilterAttribute"/> class.
        /// </summary>
        /// <param name="policyName">Name of the policy.</param>
        public IPFilterAttribute(string policyName)
        {
            PolicyName = policyName;
        }

        /// <summary>
        /// Gets the name of the policy.
        /// </summary>
        /// <value>The name of the policy.</value>
        private string PolicyName { get; }

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