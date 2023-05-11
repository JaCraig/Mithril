using Microsoft.Extensions.Logging;
using Mithril.Background.Abstractions.Services;
using Mithril.Core.Abstractions.BaseClasses;

namespace Mithril.Background.Default.HostedServices
{
    /// <summary>
    /// Background task hosted service
    /// </summary>
    /// <seealso cref="HostedServiceBaseClass&lt;BackgroundTasksHostedService&gt;" />
    public class BackgroundTasksHostedService : HostedServiceBaseClass<BackgroundTasksHostedService>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BackgroundTasksHostedService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="backgroundTaskService">The background task service.</param>
        public BackgroundTasksHostedService(ILogger<BackgroundTasksHostedService>? logger, IBackgroundTaskService? backgroundTaskService)
            : base(logger, 1)
        {
            BackgroundTaskService = backgroundTaskService;
        }

        /// <summary>
        /// Gets the background task service.
        /// </summary>
        /// <value>
        /// The background task service.
        /// </value>
        private IBackgroundTaskService? BackgroundTaskService { get; }

        /// <summary>
        /// Called to run the service.
        /// </summary>
        /// <returns>
        /// The async task.
        /// </returns>
        protected override Task DoWorkAsync()
        {
            return BackgroundTaskService?.ExecuteAsync() ?? Task.CompletedTask;
        }
    }
}