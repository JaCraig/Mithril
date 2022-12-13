﻿using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Mithril.API.Abstractions.Commands;
using Mithril.API.Abstractions.Commands.BaseClasses;
using Mithril.API.Abstractions.Commands.Interfaces;
using Mithril.Core.Abstractions.Modules.Interfaces;
using Mithril.Logging.Commands.ViewModels;
using Mithril.Logging.Features;
using Mithril.Logging.Models;
using System.Security.Claims;

namespace Mithril.Logging.Commands
{
    /// <summary>
    /// CSP log command handler
    /// </summary>
    /// <seealso cref="CommandHandlerBaseClass&lt;LogCommand, CSPLogCommandVM&gt;"/>
    public class CSPLogCommandHandler : CommandHandlerBaseClass<LogCommand, CSPLogCommandVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSPLogCommandHandler"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="featureManager">The feature manager.</param>
        public CSPLogCommandHandler(ILogger<CSPLogCommandHandler>? logger, IFeatureManager? featureManager)
            : base(logger, featureManager)
        {
        }

        /// <summary>
        /// Gets the command type accepted.
        /// </summary>
        /// <value>The command type accepted.</value>
        public override string CommandName => "CSPLog";

        /// <summary>
        /// Gets the content type accepted by command handler.
        /// </summary>
        /// <value>The content type accepted by command handler.</value>
        public override string[] ContentTypeAccepts => new string[] { "application/csp-report" };

        /// <summary>
        /// Gets the features associated with this command.
        /// </summary>
        /// <value>The features associated with this command.</value>
        public override IFeature[] Features => new IFeature[] { new LoggingFeature() };

        /// <summary>
        /// Gets the tags (Used by OpenAPI, etc).
        /// </summary>
        /// <value>The tags (Used by OpenAPI, etc).</value>
        public override string[] Tags => new[] { "Logging" };

        /// <summary>
        /// Determines whether this instance can handle the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>
        /// <c>true</c> if this instance can handle the specified command; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanHandle(ICommand command) => false;

        /// <summary>
        /// Creates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="user">The user.</param>
        /// <returns>A command value converted from the ExpandoObject.</returns>
        public override CommandCreationResult? Create(CSPLogCommandVM? value, ClaimsPrincipal user)
        {
            return IsFeatureEnabled()
                ? new CommandCreationResult(new LogCommand(LogLevel.Error, $"CSP Violation: {value?.CspReport?.DocumentUri}, {value?.CspReport?.BlockedUri}"), ResultText: "CSP violoation logged successfully")
                : null;
        }

        /// <summary>
        /// Handles the command.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>The events generated by the command.</returns>
        protected override IEvent[] HandleCommand(LogCommand?[]? args)
        {
            return Array.Empty<IEvent>();
        }
    }
}