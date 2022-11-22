using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Mithril.API.Abstractions.Commands;
using Mithril.API.Abstractions.Commands.Interfaces;
using Mithril.Data.Abstractions.Services;
using System.Security.Claims;

namespace Mithril.API.Commands.Endpoint
{
    /// <summary>
    /// Command endpoint
    /// </summary>
    public static class CommandEndpoint
    {
        /// <summary>
        /// Requests the delegate.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the view model.</typeparam>
        /// <param name="dataService">The data service.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="user">The user.</param>
        /// <param name="commandHandler">The command handler.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static async Task<IResult> RequestDelegate<TViewModel>(IDataService dataService, ILogger logger, ClaimsPrincipal user, ICommandHandler<TViewModel> commandHandler, TViewModel value)
        {
            var Command = commandHandler?.Create(value, user);
            LogCommand(logger, Command);
            if (Command is null || Command.Command is null || Command.ReturnCode == StatusCodes.Status400BadRequest)
            {
                return Results.BadRequest(new ReturnedResult { Result = Command?.ResultText ?? $"Command {commandHandler?.CommandName} was not successful." });
            }
            if (Command.ReturnCode == StatusCodes.Status401Unauthorized)
            {
                return Results.Unauthorized();
            }
            await Command.Command.SaveAsync(dataService, user).ConfigureAwait(false);
            return Results.Json(new ReturnedResult { Result = Command?.ResultText ?? $"Command {commandHandler?.CommandName} was successfully created." });
        }

        /// <summary>
        /// Logs the command.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="command">The command.</param>
        private static void LogCommand(ILogger logger, CommandCreationResult? command)
        {
            if (command is null) return;

            if (command.Exception is not null)
            {
                logger.LogError(command.Exception, command.ResultText);
                return;
            }

            logger.LogDebug($"Command recieved: {command.Command}");
        }
    }
}