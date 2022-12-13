﻿using BigBook;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Mithril.API.Abstractions.Commands.Interfaces;
using Mithril.Core.Abstractions.Modules.Interfaces;
using System.Security.Claims;

namespace Mithril.API.Abstractions.Commands.BaseClasses
{
    /// <summary>
    /// Command handler base class
    /// </summary>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    /// <typeparam name="TViewModel">The type of the view model.</typeparam>
    /// <seealso cref="ICommandHandler&lt;TViewModel&gt;"/>
    public abstract class CommandHandlerBaseClass<TCommand, TViewModel> : ICommandHandler<TViewModel>
        where TCommand : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandHandlerBaseClass{TCommand,
        /// TViewModel}"/> class.
        /// </summary>
        protected CommandHandlerBaseClass(ILogger? logger, IFeatureManager? featureManager)
        {
            Logger = logger;
            FeatureManager = featureManager;
        }

        /// <summary>
        /// Gets the command type accepted.
        /// </summary>
        /// <value>The command type accepted.</value>
        public virtual string CommandName { get; } = typeof(TCommand).Name;

        /// <summary>
        /// Gets the content type accepted by command handler.
        /// </summary>
        /// <value>The content type accepted by command handler.</value>
        public virtual string[] ContentTypeAccepts { get; } = new string[] { "application/json", "text/json", "application/*+json" };

        /// <summary>
        /// Gets the features associated with this command.
        /// </summary>
        /// <value>The features associated with this command.</value>
        public virtual IFeature[] Features { get; } = Array.Empty<IFeature>();

        /// <summary>
        /// Gets the tags (Used by OpenAPI, etc).
        /// </summary>
        /// <value>The tags (Used by OpenAPI, etc).</value>
        public virtual string[] Tags { get; } = Array.Empty<string>();

        /// <summary>
        /// Gets the type of the view model it accepts.
        /// </summary>
        /// <value>The type of the view model it accepts.</value>
        public Type ViewModelType { get; } = typeof(TViewModel);

        /// <summary>
        /// Gets the feature manager.
        /// </summary>
        /// <value>The feature manager.</value>
        protected IFeatureManager? FeatureManager { get; }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        protected ILogger? Logger { get; }

        /// <summary>
        /// Determines whether this instance can handle the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>
        /// <c>true</c> if this instance can handle the specified command; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool CanHandle(ICommand command)
        {
            return command is TCommand;
        }

        /// <summary>
        /// Creates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="user">The user.</param>
        /// <returns>A command value converted from the view model.</returns>
        public abstract CommandCreationResult? Create(TViewModel? value, ClaimsPrincipal user);

        /// <summary>
        /// Handles the Command.
        /// </summary>
        /// <param name="arg">The argument.</param>
        /// <returns>Any events that are spawned by the command.</returns>
        public IEvent[] HandleCommand(params ICommand[] arg)
        {
            arg ??= Array.Empty<ICommand>();
            var Items = arg.Where(x => CanHandle(x)).OfType<TCommand>().ToArray();
            if (Items.Length == 0)
                return Array.Empty<IEvent>();
            return HandleCommand(Items) ?? Array.Empty<IEvent>();
        }

        /// <summary>
        /// Handles the command.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>The events generated by the command.</returns>
        protected abstract IEvent[] HandleCommand(TCommand?[]? args);

        /// <summary>
        /// Determines whether the associated features are enabled.
        /// </summary>
        /// <returns><c>true</c> if all features are enabled; otherwise, <c>false</c>.</returns>
        protected bool IsFeatureEnabled()
        {
            return FeatureManager is null
                || Features is null
                || Features.Length == 0
                || Features.All(x => AsyncHelper.RunSync(() => FeatureManager.IsEnabledAsync(x.Name)));
        }
    }
}