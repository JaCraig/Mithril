using DragonHoard.Core;
using Microsoft.Extensions.Logging;
using Mithril.API.Abstractions.Attributes;
using Mithril.API.Abstractions.Commands;
using Mithril.API.Abstractions.Commands.BaseClasses;
using Mithril.API.Abstractions.Commands.Interfaces;
using Mithril.Caching.InMemory.Commands.ViewModels;
using Mithril.Core.Abstractions.Modules.Interfaces;
using Mithril.Data.Abstractions.ExtensionMethods;
using System.Security.Claims;

namespace Mithril.Caching.InMemory.Commands
{
    /// <summary>
    /// Clear cache command handler
    /// </summary>
    /// <seealso cref="CommandHandlerBaseClass&lt;ClearCacheCommandHandler, ClearCacheCommandVM&gt;"/>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ClearCacheCommandHandler"/> class.
    /// </remarks>
    /// <param name="memoryCache">The memory cache.</param>
    /// <param name="logger">The logger.</param>
    [ApiAuthorize("Admin Only")]
    public class ClearCacheCommandHandler(Cache? memoryCache, ILogger<ClearCacheCommandHandler>? logger) : ICommandHandler<ClearCacheCommandVM>
    {
        /// <summary>
        /// Gets the command type accepted.
        /// </summary>
        /// <value>The command type accepted.</value>
        public string CommandName { get; } = "ClearCache";

        /// <summary>
        /// Gets the content type accepted by command handler.
        /// </summary>
        /// <value>The content type accepted by command handler.</value>
        public string[] ContentTypeAccepts { get; } = ["application/json", "text/json", "application/*+json"];

        /// <summary>
        /// Gets the features associated with this command.
        /// </summary>
        /// <value>The features associated with this command.</value>
        public IFeature[] Features { get; } = [];

        /// <summary>
        /// Gets the tags (Used by OpenAPI, etc).
        /// </summary>
        /// <value>The tags (Used by OpenAPI, etc).</value>
        public string[] Tags { get; } = ["Cache"];

        /// <summary>
        /// Gets the version (not guaranteed to be used in all query providers, but defaults to "v1").
        /// </summary>
        /// <value>The version.</value>
        public string? Version { get; } = "v1";

        /// <summary>
        /// Gets the type of the view model it accepts.
        /// </summary>
        /// <value>The type of the view model it accepts.</value>
        public Type ViewModelType { get; } = typeof(ClearCacheCommandVM);

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        private ILogger<ClearCacheCommandHandler>? Logger { get; } = logger;

        /// <summary>
        /// Gets the memory cache.
        /// </summary>
        /// <value>The memory cache.</value>
        private Cache? MemoryCache { get; } = memoryCache;

        /// <summary>
        /// Determines whether this instance can handle the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>
        /// <c>true</c> if this instance can handle the specified command; otherwise, <c>false</c>.
        /// </returns>
        public bool CanHandle(ICommand command) => false;

        /// <summary>
        /// Creates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="user">The user.</param>
        /// <returns>A command value converted from the view model.</returns>
        public ValueTask<CommandCreationResult?> CreateAsync(ClearCacheCommandVM? value, ClaimsPrincipal user)
        {
            if (MemoryCache is null)
                return ValueTask.FromResult<CommandCreationResult?>(new CommandCreationResult(null));
            Logger?.LogInformation("Clearing cache via command sent by {user}", user.GetName());
            MemoryCache.GetOrAddCache(value?.CacheName ?? "Default")?.Compact(1);
            return ValueTask.FromResult<CommandCreationResult?>(new CommandCreationResult(null));
        }

        /// <summary>
        /// Handles the Command.
        /// </summary>
        /// <param name="arg">The argument.</param>
        /// <returns>Any events that are spawned by the command.</returns>
        public Task<IEvent[]> HandleCommandAsync(params ICommand[] arg) => Task.FromResult(Array.Empty<IEvent>());
    }
}