using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Mithril.FileSystem.HealthChecks
{
    /// <summary>
    /// Disk space health check
    /// </summary>
    /// <seealso cref="IHealthCheck"/>
    public class DiskSpaceHealthCheck : IHealthCheck
    {
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
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            foreach (var Drive in DriveInfo.GetDrives().Where(Drive => Drive.IsReady))
            {
                long FreeSpaceMegabytes = Drive.AvailableFreeSpace / 1024 / 1024;
                if (FreeSpaceMegabytes < 1024)
                {
                    return Task.FromResult(HealthCheckResult.Unhealthy(description: "Drive space is less than 1 GB"));
                }
                else if (FreeSpaceMegabytes < 5120)
                {
                    return Task.FromResult(HealthCheckResult.Degraded(description: "Drive space is less than 5 GB"));
                }
            }
            return Task.FromResult(HealthCheckResult.Healthy("No issues discovered"));
        }
    }
}