using Microsoft.AspNetCore.Http;
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
        /// <param name="user">The user.</param>
        /// <param name="commandHandler">The command handler.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static async Task<IResult> RequestDelegate<TViewModel>(IDataService dataService, ClaimsPrincipal user, ICommandHandler<TViewModel> commandHandler, TViewModel value)
        {
            var Command = commandHandler?.Create(value);
            if (Command is null)
                return Results.BadRequest();
            await Command.SaveAsync(dataService, user).ConfigureAwait(false);
            return Results.Json(new { result = "OK" });
        }
    }
}