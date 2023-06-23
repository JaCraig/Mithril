using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Mithril.Admin.Abstractions.Interfaces;
using Mithril.Admin.Abstractions.Services;
using Mithril.Admin.Commands.ViewModels;
using Mithril.API.Abstractions.Commands;
using Mithril.API.Abstractions.Commands.Interfaces;
using Mithril.Core.Abstractions.Modules.Interfaces;
using Mithril.Data.Abstractions.ExtensionMethods;
using System.Security.Claims;

namespace Mithril.Admin.Commands
{
    /// <summary>
    /// Save model command handler
    /// TODO: Add tests
    /// </summary>
    /// <seealso cref="ICommandHandler&lt;SaveModelCommandVM&gt;"/>
    public class SaveModelCommandHandler : ICommandHandler<SaveModelCommandVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SaveModelCommandHandler"/> class.
        /// </summary>
        /// <param name="editorService">The editor service.</param>
        /// <param name="logger">The logger.</param>
        public SaveModelCommandHandler(IEditorService editorService, ILogger<SaveModelCommandHandler>? logger)
        {
            EditorService = editorService;
            Logger = logger;
        }

        /// <summary>
        /// Gets the command type accepted.
        /// </summary>
        /// <value>The command type accepted.</value>
        public string CommandName { get; } = "SaveModelCommand";

        /// <summary>
        /// Gets the content type accepted by command handler.
        /// </summary>
        /// <value>The content type accepted by command handler.</value>
        public string[] ContentTypeAccepts { get; } = new string[] { "application/json", "text/json", "application/*+json" };

        /// <summary>
        /// Gets the features associated with this command.
        /// </summary>
        /// <value>The features associated with this command.</value>
        public IFeature[] Features { get; } = Array.Empty<IFeature>();

        /// <summary>
        /// Gets the tags (Used by OpenAPI, etc).
        /// </summary>
        /// <value>The tags (Used by OpenAPI, etc).</value>
        public string[] Tags { get; } = new string[] { "Data" };

        /// <summary>
        /// Gets the version (not guaranteed to be used in all query providers, but defaults to "v1").
        /// </summary>
        /// <value>The version.</value>
        public string? Version { get; } = "v1";

        /// <summary>
        /// Gets the type of the view model it accepts.
        /// </summary>
        /// <value>The type of the view model it accepts.</value>
        public Type ViewModelType { get; } = typeof(SaveModelCommandVM);

        /// <summary>
        /// Gets the editor service.
        /// </summary>
        /// <value>The editor service.</value>
        private IEditorService EditorService { get; }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        private ILogger<SaveModelCommandHandler>? Logger { get; }

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
        public async ValueTask<CommandCreationResult?> CreateAsync(SaveModelCommandVM? value, ClaimsPrincipal user)
        {
            if (EditorService is null || value?.Data is null)
            {
                Logger?.LogError("Saving model data failed.");
                return new CommandCreationResult(null, ResultText: "Error bad request.", ReturnCode: StatusCodes.Status400BadRequest);
            }

            IEntityEditor? EntityEditor = EditorService.Editors.OfType<IEntityEditor>().FirstOrDefault(x => x.EntityType == value.EntityType);
            if (EntityEditor?.CanView(user) != true)
            {
                Logger?.LogWarning("Saving model data of type {entityType} failed because user {user} does not have access.", value.EntityType, user.GetName());
                return new CommandCreationResult(null, ResultText: "Unauthorized.", ReturnCode: StatusCodes.Status401Unauthorized);
            }

            Logger?.LogInformation("Saving model data of type {entityType} and ID={id}, sent by {user}", value.EntityType, value.ID, user.GetName());
            if (!await EntityEditor.SaveEntityAsync(value.ID, value.Data, user).ConfigureAwait(false))
            {
                Logger?.LogError("Saving model data of type {entityType} and ID={id} sent by {user} failed.", value.EntityType, value.ID, user.GetName());
                return new CommandCreationResult(null, ResultText: "Error, unable to save.", ReturnCode: StatusCodes.Status500InternalServerError);
            }
            Logger?.LogInformation("Saving model data of type {entityType} and ID={id} sent by {user} succeeded.", value.EntityType, value.ID, user.GetName());
            return new CommandCreationResult(null);
        }

        /// <summary>
        /// Handles the Command.
        /// </summary>
        /// <param name="arg">The argument.</param>
        /// <returns>Any events that are spawned by the command.</returns>
        public Task<IEvent[]> HandleCommandAsync(params ICommand[] arg) => Task.FromResult(Array.Empty<IEvent>());
    }
}