﻿using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Mithril.Data.HealthCheck
{
    /// <summary>
    /// SQL Connection health check
    /// </summary>
    /// <seealso cref="IHealthCheck"/>
    /// <remarks>
    /// Initializes a new instance of the <see cref="SqlConnectionHealthCheck"/> class.
    /// </remarks>
    /// <param name="configuration">The configuration.</param>
    public class SqlConnectionHealthCheck(IConfiguration? configuration) : IHealthCheck
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        private IConfiguration? Configuration { get; } = configuration;

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
            IConfigurationSection? ConnectionStrings = Configuration?.GetSection("ConnectionStrings");
            if (ConnectionStrings is null)
                return HealthCheckResult.Unhealthy("No connections to database defined");
            foreach (IConfigurationSection? ConnectionString in ConnectionStrings.GetChildren().AsEnumerable())
            {
                CheckHealthResult Result = await CheckHealthAsync(ConnectionString.Value, cancellationToken).ConfigureAwait(false);
                if (Result.Exception is not null)
                    return HealthCheckResult.Unhealthy($"Issue connecting to {ConnectionString.Value}", Result.Exception);
            }

            return HealthCheckResult.Healthy("No issues discovered");
        }

        /// <summary>
        /// Checks the health asynchronously.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        private static async Task<CheckHealthResult> CheckHealthAsync(string? connectionString, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(connectionString))
                return new CheckHealthResult();
            using var connection = new SqlConnection(connectionString);
            try
            {
                await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT 1";

                _ = await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                return new CheckHealthResult { Exception = exception };
            }
            finally
            {
                await connection.CloseAsync().ConfigureAwait(false);
            }
            return new CheckHealthResult();
        }
    }

    /// <summary>
    /// Check health result
    /// </summary>
    internal class CheckHealthResult
    {
        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        /// <value>The exception.</value>
        public Exception? Exception { get; set; }
    }
}