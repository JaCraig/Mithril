using Microsoft.Extensions.Logging;
using Mithril.Background.Abstractions.Services;
using Mithril.Core.Abstractions.BaseClasses;

namespace Mithril.Background.Default.HostedServices
{
    /// <summary>
    /// Background task hosted service
    /// </summary>
    /// <seealso cref="HostedServiceBaseClass&lt;BackgroundTasksHostedService&gt;" />
    /// <remarks>
    /// Initializes a new instance of the <see cref="BackgroundTasksHostedService"/> class.
    /// </remarks>
    /// <param name="logger">The logger.</param>
    /// <param name="backgroundTaskService">The background task service.</param>
    public class BackgroundTasksHostedService(ILogger<BackgroundTasksHostedService>? logger, IBackgroundTaskService? backgroundTaskService) : HostedServiceBaseClass<BackgroundTasksHostedService>(logger, 1)
    {
        /// <summary>
        /// Gets the background task service.
        /// </summary>
        /// <value>
        /// The background task service.
        /// </value>
        private IBackgroundTaskService? BackgroundTaskService { get; } = backgroundTaskService;

        /// <summary>
        /// Called to run the service.
        /// </summary>
        /// <returns>
        /// The async task.
        /// </returns>
        protected override Task DoWorkAsync() => BackgroundTaskService?.ExecuteAsync() ?? Task.CompletedTask;
    }
}