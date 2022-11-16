using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mithril.API.Abstractions.Services;
using Mithril.Data.Abstractions.Services;
using System.Dynamic;
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
        /// <param name="commandService">The command service.</param>
        /// <param name="dataService">The data service.</param>
        /// <param name="user">The user.</param>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static async Task<IResult> RequestDelegate(ICommandService commandService, IDataService dataService, ClaimsPrincipal user, [FromRoute] string type, [FromBody] ExpandoObject value)
        {
            if (commandService is null)
                return Results.BadRequest();
            var Command = commandService.Convert(type, value);
            if (Command is null)
                return Results.BadRequest();
            await Command.SaveAsync(dataService, user).ConfigureAwait(false);
            return Results.Json(new { result = "OK" });
        }
    }
}