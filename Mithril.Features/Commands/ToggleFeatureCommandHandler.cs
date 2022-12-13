﻿using BigBook;
using Microsoft.Extensions.Logging;
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
    [ApiAuthorize("Admin Only")]
    public class ToggleFeatureCommandHandler : CommandHandlerBaseClass<ToggleFeatureCommand, ToggleFeatureCommandVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSPLogCommandHandler"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="featureManager">The feature manager.</param>
        /// <param name="sessionManager">The session manager.</param>
        public ToggleFeatureCommandHandler(ILogger<ToggleFeatureCommandHandler>? logger, IFeatureManager? featureManager, ISessionManager? sessionManager)
            : base(logger, featureManager)
        {
            SessionManager = sessionManager;
        }

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
        private ISessionManager? SessionManager { get; }

        /// <summary>
        /// Creates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="user">The user.</param>
        /// <returns>A command value converted from the ExpandoObject.</returns>
        public override CommandCreationResult? Create(ToggleFeatureCommandVM? value, ClaimsPrincipal user)
        {
            if (string.IsNullOrEmpty(value?.FeatureName))
                return null;
            return new CommandCreationResult(new ToggleFeatureCommand(value.FeatureName, value.Active), ResultText: "Feature toggle command successfully received");
        }

        /// <summary>
        /// Handles the command.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>The events generated by the command.</returns>
        protected override IEvent[] HandleCommand(ToggleFeatureCommand?[]? args)
        {
            if (args is null || Logger is null)
                return Array.Empty<IEvent>();
            List<IEvent> ReturnValues = new List<IEvent>();
            for (var x = 0; x < args.Length; ++x)
            {
                var arg = args[x];
                if (arg is null || string.IsNullOrEmpty(arg.FeatureName))
                    continue;
                AsyncHelper.RunSync(() => SessionManager?.SetAsync(arg.FeatureName, arg.FeatureStatus) ?? Task.CompletedTask);
                ReturnValues.Add(new FeatureToggledEvent(arg.FeatureName, arg.FeatureStatus));
            }
            return ReturnValues.ToArray();
        }
    }
}