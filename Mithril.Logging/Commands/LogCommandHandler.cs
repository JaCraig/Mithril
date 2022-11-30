﻿using BigBook;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Mithril.API.Abstractions.Commands;
using Mithril.API.Abstractions.Commands.BaseClasses;
using Mithril.API.Abstractions.Commands.Interfaces;
using Mithril.Core.Abstractions.Modules.Interfaces;
using Mithril.Logging.Commands.ViewModels;
using Mithril.Logging.Exceptions;
using Mithril.Logging.Features;
using Mithril.Logging.Models;
using System.Security.Claims;

namespace Mithril.Logging.Commands
{
    /// <summary>
    /// Log command handler
    /// </summary>
    /// <seealso cref="CommandHandlerBaseClass&lt;LogCommand,LogCommandVM&gt;"/>
    public class LogCommandHandler : CommandHandlerBaseClass<LogCommand, LogCommandVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogCommandHandler"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="featureManager">The feature manager.</param>
        public LogCommandHandler(ILogger<LogCommandHandler>? logger, IFeatureManager? featureManager)
            : base(logger, featureManager)
        {
        }

        /// <summary>
        /// Gets the command type accepted.
        /// </summary>
        /// <value>The command type accepted.</value>
        public override string CommandName => "Log";

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
        /// Creates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="user">The user.</param>
        /// <returns>A command value converted from the ExpandoObject.</returns>
        public override CommandCreationResult? Create(LogCommandVM? value, ClaimsPrincipal user)
        {
            return IsFeatureEnabled()
                ? new CommandCreationResult(new LogCommand(value?.LogLevel ?? LogLevel.Information, value?.Message ?? ""))
                : null;
        }

        /// <summary>
        /// Handles the command.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>The events generated by the command.</returns>
        protected override IEvent[] HandleCommand(params LogCommand?[]? args)
        {
            if (args is null || Logger is null)
                return Array.Empty<IEvent>();
            for (var x = 0; x < args.Length; ++x)
            {
                var arg = args[x];
                if (arg is null)
                    continue;
                if (arg.LogLevel == LogLevel.Error)
                    Logger.LogError(new JavascriptException(arg.Message ?? ""), "An error has occurred and is being logged by the error service");
                else
                    Logger.Log(arg.LogLevel, arg.Message ?? "");
            }
            return Array.Empty<IEvent>();
        }
    }
}