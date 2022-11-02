using BigBook;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mithril.API.Abstractions.Services;
using Mithril.Core.Abstractions.Extensions;

namespace Mithril.API.Commands.BackgroundTasks
{
    /// <summary>
    /// Command processor task
    /// </summary>
    /// <seealso cref="IHostedService"/>
    /// <seealso cref="IDisposable"/>
    public class CommandProcessorTask : IHostedService, IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandProcessorTask"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="commandService">The command service.</param>
        /// <param name="configuration">The configuration.</param>
        public CommandProcessorTask(ILogger<CommandProcessorTask> logger, ICommandService commandService, IConfiguration configuration)
        {
            Logger = logger;
            CommandService = commandService;
            CommandRunFrequency = configuration?.GetSystemConfig()?.API?.CommandRunFrequency ?? 60;
        }

        /// <summary>
        /// Gets the command run frequency.
        /// </summary>
        /// <value>The command run frequency.</value>
        private int CommandRunFrequency { get; }

        /// <summary>
        /// Gets the command service.
        /// </summary>
        /// <value>The command service.</value>
        private ICommandService CommandService { get; }

        /// <summary>
        /// Gets or sets the internal timer.
        /// </summary>
        /// <value>The internal timer.</value>
        private Timer? InternalTimer { get; set; }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        private ILogger<CommandProcessorTask> Logger { get; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Triggered when the application host is ready to start the service.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation("Starting command background service");
            InternalTimer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(CommandRunFrequency));
            return Task.CompletedTask;
        }

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        /// <param name="cancellationToken">
        /// Indicates that the shutdown process should no longer be graceful.
        /// </param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation("Stopping command background service");
            InternalTimer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="managed">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release
        /// only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool managed)
        {
            if (managed)
            {
                InternalTimer?.Dispose();
                InternalTimer = null;
            }
        }

        /// <summary>
        /// Does the work.
        /// </summary>
        /// <param name="state">The state.</param>
        private void DoWork(object? state)
        {
            AsyncHelper.RunSync(() => CommandService.RunAsync());
        }
    }
}