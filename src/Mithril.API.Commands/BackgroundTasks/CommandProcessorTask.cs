using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Mithril.API.Abstractions.Configuration;
using Mithril.API.Abstractions.Services;
using Mithril.Background.Abstractions.Frequencies;
using Mithril.Background.Abstractions.Interfaces;

namespace Mithril.API.Commands.BackgroundTasks
{
    /// <summary>
    /// Command processor task
    /// </summary>
    /// <seealso cref="IHostedService"/>
    /// <seealso cref="IDisposable"/>
    /// <remarks>
    /// Initializes a new instance of the <see cref="CommandProcessorTask" /> class.
    /// </remarks>
    /// <param name="commandService">The command service.</param>
    /// <param name="configuration">The configuration.</param>
    public class CommandProcessorTask(ICommandService? commandService, IOptions<APIOptions>? configuration) : IScheduledTask
    {
        /// <summary>
        /// Gets the frequencies.
        /// </summary>
        /// <value>
        /// The frequencies.
        /// </value>
        public IFrequency[] Frequencies { get; } = [new RunEvery(TimeSpan.FromSeconds(configuration?.Value?.CommandRunFrequency ?? 60))];

        /// <summary>
        /// Gets the last run time.
        /// </summary>
        /// <value>
        /// The last run time.
        /// </value>
        public DateTime LastRunTime { get; set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; } = "Command Processor";

        /// <summary>
        /// Gets the command service.
        /// </summary>
        /// <value>The command service.</value>
        private ICommandService? CommandService { get; } = commandService;

        /// <summary>
        /// Executes the asynchronous.
        /// </summary>
        /// <returns></returns>
        public Task ExecuteAsync() => CommandService?.ProcessAsync() ?? Task.CompletedTask;
    }
}