using Microsoft.AspNetCore.Http;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mithril.Logging.Commands.ViewModels
{
    /// <summary>
    /// CSP Log Command VM
    /// </summary>
    public class CSPLogCommandVM
    {
        /// <summary>
        /// Gets or sets the CSP report.
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
        /// Gets or sets the blocked URI.
        /// </summary>
        /// <value>The blocked URI.</value>
        [JsonPropertyName("blocked-uri")]
        public string? BlockedUri { get; set; }

        /// <summary>
        /// Gets or sets the document URI.
        /// </summary>
        /// <value>The document URI.</value>
        [JsonPropertyName("document-uri")]
        public string? DocumentUri { get; set; }

        /// <summary>
        /// Gets or sets the effective directive.
        /// </summary>
        /// <value>The effective directive.</value>
        [JsonPropertyName("effective-directive")]
        public string? EffectiveDirective { get; set; }

        /// <summary>
        /// Gets or sets the original policy.
        /// </summary>
        /// <value>The original policy.</value>
        [JsonPropertyName("original-policy")]
        public string? OriginalPolicy { get; set; }

        /// <summary>
        /// Gets or sets the referrer.
        /// </summary>
        /// <value>The referrer.</value>
        [JsonPropertyName("referrer")]
        public string? Referrer { get; set; }

        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>The status code.</value>
        [JsonPropertyName("status-code")]
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the violated directive.
        /// </summary>
        /// <value>The violated directive.</value>
        [JsonPropertyName("violated-directive")]
        public string? ViolatedDirective { get; set; }
    }
}