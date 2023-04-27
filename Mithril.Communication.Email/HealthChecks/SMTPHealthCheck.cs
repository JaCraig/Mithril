using MailKit.Net.Smtp;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.FeatureManagement;
using Mithril.Communication.Email.Features;
using Mithril.Communication.Email.Models;
using Mithril.Core.Abstractions.Extensions;
using Mithril.Data.Abstractions.Services;

namespace Mithril.Communication.Email.HealthChecks
{
    /// <summary>
    /// SMTP health check
    /// </summary>
    /// <seealso cref="IHealthCheck"/>
    public class SMTPHealthCheck : IHealthCheck
    {
        /// <summary>
        /// Gets the data service.
        /// </summary>
        /// <value>The data service.</value>
        public static IDataService? DataService { get; set; }

        /// <summary>
        /// Gets the feature manager.
        /// </summary>
        /// <value>The feature manager.</value>
        public static IFeatureManager? FeatureManager { get; set; }

        /// <summary>
        /// Runs the health check, returning the status of the component being checked.
        /// </summary>
        /// <param name="context">A context object associated with the current execution.</param>
        /// <param name="cancellationToken">
        /// A <see cref="T:System.Threading.CancellationToken"/> that can be used to cancel the
        /// health check.
        /// </param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1"/> that completes when the health check has
        /// finished, yielding the status of the component being checked.
        /// </returns>
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            if (!FeatureManager.AreFeaturesEnabled(EmailFeature.Instance))
                return HealthCheckResult.Healthy();
            var Settings = await EmailSettings.LoadOrCreateAsync(DataService, null).ConfigureAwait(false);
            try
            {
                using var Client = new SmtpClient();
                await Client.ConnectAsync(Settings.Server, Settings.Port, Settings.UseSSL, cancellationToken).ConfigureAwait(false);
                if (!Client.IsConnected)
                    return HealthCheckResult.Unhealthy($"Unable to connect to SMTP server: {Settings.Server}:{Settings.Port}");
                if (!string.IsNullOrEmpty(Settings.UserName) && !string.IsNullOrEmpty(Settings.Password))
                {
                    await Client.AuthenticateAsync(Settings.UserName, Settings.Password, cancellationToken).ConfigureAwait(false);
                    if (!Client.IsAuthenticated)
                        return HealthCheckResult.Unhealthy($"Unable to authenticate against SMTP server: {Settings.Server}:{Settings.Port}");
                }
                return HealthCheckResult.Healthy("No issues discovered");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Error occurred", exception: new Exception(ex?.Message ?? "An error occurred"));
            }
        }
    }
}