﻿using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Mithril.API.Abstractions.Attributes;
using Mithril.API.Abstractions.Commands;
using Mithril.API.Abstractions.Commands.BaseClasses;
using Mithril.API.Abstractions.Commands.Interfaces;
using Mithril.Features.Commands.ViewModels;
using Mithril.Features.Models;
using System.Security.Claims;

namespace Mithril.Features.Commands
{
    /// <summary>
    /// Toggle feature command handler
    /// </summary>
    /// <seealso cref="CommandHandlerBaseClass&lt;ToggleFeatureCommand, ToggleFeatureCommandVM&gt;"/>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ToggleFeatureCommandHandler"/> class.
    /// </remarks>
    /// <param name="logger">The logger.</param>
    /// <param name="featureManager">The feature manager.</param>
    /// <param name="sessionManager">The session manager.</param>
    [ApiAuthorize("Admin Only")]
    public class ToggleFeatureCommandHandler(ILogger<ToggleFeatureCommandHandler>? logger, IFeatureManager? featureManager, ISessionManager? sessionManager) : CommandHandlerBaseClass<ToggleFeatureCommand, ToggleFeatureCommandVM>(logger, featureManager)
    {
        /// <summary>
        /// Gets the command type accepted.
        /// </summary>
        /// <value>The command type accepted.</value>
        public override string CommandName => "ToggleFeature";

        /// <summary>
        /// Gets the tags (Used by OpenAPI, etc).
        /// </summary>
        /// <value>The tags (Used by OpenAPI, etc).</value>
        public override string[] Tags => new[] { "Feature Flags" };

        /// <summary>
        /// Gets the session manager.
        /// </summary>
        /// <value>The session manager.</value>
        private ISessionManager? SessionManager { get; } = sessionManager;

        /// <summary>
        /// Creates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="user">The user.</param>
        /// <returns>A command value converted from the ExpandoObject.</returns>
        public override ValueTask<CommandCreationResult?> CreateAsync(ToggleFeatureCommandVM? value, ClaimsPrincipal user)
        {
            return string.IsNullOrEmpty(value?.FeatureName)
                ? ValueTask.FromResult<CommandCreationResult?>(null)
                : ValueTask.FromResult<CommandCreationResult?>(new CommandCreationResult(new ToggleFeatureCommand(value.FeatureName, value.Active), ResultText: "Feature toggle command successfully received"));
        }

        /// <summary>
        /// Handles the command.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>The events generated by the command.</returns>
        protected override async Task<IEvent[]> HandleCommandAsync(ToggleFeatureCommand?[]? args)
        {
            if (args is null || Logger is null || SessionManager is null)
                return [];
            var ReturnValues = new List<IEvent>();
            for (var x = 0; x < args.Length; ++x)
            {
                ToggleFeatureCommand? arg = args[x];
                if (arg is null || string.IsNullOrEmpty(arg.FeatureName))
                    continue;
                await SessionManager.SetAsync(arg.FeatureName, arg.FeatureStatus).ConfigureAwait(false);
                ReturnValues.Add(new FeatureToggledEvent(arg.FeatureName, arg.FeatureStatus));
            }
            return ReturnValues.ToArray();
        }
    }
}