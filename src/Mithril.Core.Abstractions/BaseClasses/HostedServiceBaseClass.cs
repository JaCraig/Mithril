using BigBook;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Mithril.Core.Abstractions.BaseClasses
{
    /// <summary>
    /// Hosted service base class
    /// </summary>
    /// <typeparam name="THostService">Host service type</typeparam>
    public abstract class HostedServiceBaseClass<THostService> : IHostedService, IDisposable
        where THostService : HostedServiceBaseClass<THostService>
    {
        /// <summary>
        /// Initializes a new instance of HostedServiceBaseClass
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="frequency">Frequency that it runs at.</param>
        protected HostedServiceBaseClass(ILogger<THostService>? logger, double frequency)
        {
            Logger = logger;
            Frequency = frequency;
        }

        /// <summary>
        /// Gets the description for the hosted service. Used for logging.
        /// </summary>
        protected virtual string? Description { get; } = typeof(THostService).Name.AddSpaces().ToLowerInvariant();

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        protected ILogger<THostService>? Logger { get; }

        /// <summary>
        /// Get or sets the frequency that it runs at.
        /// </summary>
        /// <value>The frequency that it runs at.</value>
        private double Frequency { get; }

        /// <summary>
        /// Gets or sets the internal timer.
        /// </summary>
        /// <value>The internal timer.</value>
        private Timer? InternalTimer { get; set; }

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
        /// <returns>The async task.</returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Logger?.LogInformation("Starting {Description}", Description);
            InternalTimer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(Frequency));
            return Task.CompletedTask;
        }

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        /// <param name="cancellationToken">
        /// Indicates that the shutdown process should no longer be graceful.
        /// </param>
        /// <returns>The async task.</returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            Logger?.LogInformation("Stopping {Description}", Description);
            _ = (InternalTimer?.Change(Timeout.Infinite, 0));
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
        /// Called to run the service.
        /// </summary>
        /// <returns>The async task.</returns>
        protected abstract Task DoWorkAsync();

        /// <summary>
        /// Does the work.
        /// </summary>
        /// <param name="state">The state.</param>
        private void DoWork(object? state) => AsyncHelper.RunSync(DoWorkAsync);
    }
}