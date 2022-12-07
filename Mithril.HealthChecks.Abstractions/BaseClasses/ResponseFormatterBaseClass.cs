using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Mithril.HealthChecks.Abstractions.Interfaces;

namespace Mithril.HealthChecks.Abstractions.BaseClasses
{
    /// <summary>
    /// Response formatter base class
    /// </summary>
    /// <seealso cref="IResponseFormatter"/>
    public abstract class ResponseFormatterBaseClass : IResponseFormatter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseFormatterBaseClass"/> class.
        /// </summary>
        protected ResponseFormatterBaseClass()
        { }

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        public virtual int Order { get; }

        /// <summary>
        /// Acceptses the specified format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <returns>True if it accepts that format, false otherwise.</returns>
        public virtual bool Accepts(string format) => true;

        /// <summary>
        /// Sends the response asynchronously.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <param name="healthReport">The health report.</param>
        /// <returns>The async task.</returns>
        public abstract Task SendResponseAsync(HttpContext httpContext, HealthReport healthReport);
    }
}