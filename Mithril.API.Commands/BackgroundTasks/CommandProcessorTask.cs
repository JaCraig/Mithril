using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mithril.API.Abstractions.Configuration;
using Mithril.API.Abstractions.Services;
using Mithril.Core.Abstractions.BaseClasses;

namespace Mithril.API.Commands.BackgroundTasks
{
    /// <summary>
    /// Command processor task
    /// </summary>
    /// <seealso cref="IHostedService"/>
    /// <seealso cref="IDisposable"/>
    public class CommandProcessorTask : HostedServiceBaseClass<CommandProcessorTask>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandProcessorTask"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="commandService">The command service.</param>
        /// <param name="configuration">The configuration.</param>
        public CommandProcessorTask(ILogger<CommandProcessorTask>? logger, ICommandService? commandService, IOptions<APIOptions>? configuration)
            : base(logger, configuration?.Value?.CommandRunFrequency ?? 60)
        {
            CommandService = commandService;
        }

        /// <summary>
        /// Gets the command service.
        /// </summary>
        /// <value>The command service.</value>
        private ICommandService? CommandService { get; }

        /// <summary>
        /// Does the work.
        /// </summary>
        /// <returns>Async task.</returns>
        protected override Task DoWorkAsync()
        {
            return CommandService?.ProcessAsync() ?? Task.CompletedTask;
        }
    }
}