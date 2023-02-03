using Microsoft.AspNetCore.Http;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mithril.Logging.Commands.ViewModels
{
    /// <summary>
    /// CSP Log Command, used for reporting CSP violations.
    /// </summary>
    public class CSPLogCommandVM
    {
        /// <summary>
        /// The CSP report.
        /// </summary>
        /// <value>The CSP report.</value>
        [JsonPropertyName("csp-report")]
        public CspReport? CspReport { get; set; }

        /// <summary>
        /// Binds the VM asynchronously.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns>The view model.</returns>
        /// <exception cref="BadHttpRequestException">Request content type was not 'application/csp-report'</exception>
        public static ValueTask<CSPLogCommandVM?> BindAsync(HttpContext context, ParameterInfo parameter)
        {
            if (context?.Request is null || parameter is null)
                return ValueTask.FromResult<CSPLogCommandVM?>(null);
            if (!string.Equals(context.Request.ContentType, "application/csp-report"))
            {
                throw new BadHttpRequestException("Request content type was not 'application/csp-report'", StatusCodes.Status415UnsupportedMediaType);
            }
            return JsonSerializer.DeserializeAsync<CSPLogCommandVM?>(context.Request.Body);
        }
    }

    /// <summary>
    /// CSP Report
    /// </summary>
    public class CspReport
    {
        /// <summary>
        /// The blocked URI.
        /// </summary>
        /// <value>The blocked URI.</value>
        [JsonPropertyName("blocked-uri")]
        public string? BlockedUri { get; set; }

        /// <summary>
        /// The document URI.
        /// </summary>
        /// <value>The document URI.</value>
        [JsonPropertyName("document-uri")]
        public string? DocumentUri { get; set; }

        /// <summary>
        /// The effective directive.
        /// </summary>
        /// <value>The effective directive.</value>
        [JsonPropertyName("effective-directive")]
        public string? EffectiveDirective { get; set; }

        /// <summary>
        /// The original policy.
        /// </summary>
        /// <value>The original policy.</value>
        [JsonPropertyName("original-policy")]
        public string? OriginalPolicy { get; set; }

        /// <summary>
        /// The referrer.
        /// </summary>
        /// <value>The referrer.</value>
        [JsonPropertyName("referrer")]
        public string? Referrer { get; set; }

        /// <summary>
        /// The status code.
        /// </summary>
        /// <value>The status code.</value>
        [JsonPropertyName("status-code")]
        public int StatusCode { get; set; }

        /// <summary>
        /// The violated directive.
        /// </summary>
        /// <value>The violated directive.</value>
        [JsonPropertyName("violated-directive")]
        public string? ViolatedDirective { get; set; }
    }
}