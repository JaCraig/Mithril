﻿using Microsoft.Extensions.Logging;
using Mithril.API.Abstractions.Commands.BaseClasses;
using Mithril.API.Abstractions.Commands.Interfaces;
using Mithril.Logging.Serilog.Exceptions;
using Mithril.Logging.Serilog.Models;
using System.Dynamic;

namespace Mithril.Logging.Serilog.Commands
{
    /// <summary>
    /// Log command handler
    /// </summary>
    /// <seealso cref="CommandHandlerBaseClass{LogCommand}"/>
    public class LogCommandHandler : CommandHandlerBaseClass<LogCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogCommandHandler"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="featureService">The feature service.</param>
        public LogCommandHandler(ILogger<LogCommandHandler> logger)
        {
            Logger = logger;
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public ILogger<LogCommandHandler> Logger { get; }

        /// <summary>
        /// Creates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A command value converted from the ExpandoObject.</returns>
        public override ICommand Create(ExpandoObject value)
        {
            dynamic Data = value;
            return new LogCommand(Data.loglevel, Data.message);
        }

        /// <summary>
        /// Handles the command.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>The events generated by the command.</returns>
        protected override IEvent[] HandleCommand(params LogCommand[] args)
        {
            if (args is null)
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