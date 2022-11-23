using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Mithril.API.Abstractions.Attributes;
using Mithril.API.Abstractions.Commands.Interfaces;
using Mithril.API.Commands.Endpoint;
using Mithril.Core.Abstractions.Configuration;
using Mithril.Data.Abstractions.Services;
using System.Reflection;
using System.Security.Claims;

namespace Mithril.API.Commands.Utils
{
    /// <summary>
    /// Command endpoint builder
    /// </summary>
    public static class CommandEndpointBuilder
    {
        /// <summary>
        /// Setups the end point.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the view model.</typeparam>
        /// <param name="endpoints">The endpoints.</param>
        /// <param name="commandEndPoint">The command end point.</param>
        /// <param name="commandHandler">The command handler.</param>
        /// <param name="config">The configuration.</param>
        public static void SetupEndPoint<TViewModel>(IEndpointRouteBuilder? endpoints, string commandEndPoint, ICommandHandler<TViewModel> commandHandler, MithrilConfig? config)
            where TViewModel : notnull
        {
            if (commandHandler is null || endpoints is null)
                return;
            commandEndPoint = commandEndPoint?.Replace("{", "").Replace("}", "") ?? "";
            var CommandName = (commandHandler?.CommandName ?? "").Replace("{", "").Replace("}", "");
            var EndPointBuilder = endpoints.MapPost(commandEndPoint + CommandName, (
                                                        [FromServices] IDataService dataService,
                                                        [FromServices] ILogger<CommandModule> logger,
                                                        ClaimsPrincipal user,
                                                        TViewModel value) => CommandEndpoint.RequestDelegate(dataService, logger, user, commandHandler, value))
                                            .Produces<ReturnedResult>(StatusCodes.Status200OK, contentType: "application/json")
                                            .Produces<ReturnedResult>(StatusCodes.Status400BadRequest, contentType: "application/json")
                                            .WithName(commandHandler.CommandName)
                                            .WithTags(commandHandler.Tags);
            if (EndPointBuilder is null)
                return;
            EndPointBuilder = SetupContentTypesAccepted(commandHandler, EndPointBuilder);

            var HandlerType = commandHandler.GetType();

            var AuthorizationAttribute = HandlerType.GetCustomAttribute<ApiAuthorizeAttribute>();

            if (AllowAnonymous(config, HandlerType))
            {
                EndPointBuilder = EndPointBuilder?.AllowAnonymous();
                return;
            }
            if (UsePolicyForAuth(config, AuthorizationAttribute))
            {
                EndPointBuilder = EndPointBuilder?.Produces(StatusCodes.Status401Unauthorized);
                EndPointBuilder = EndPointBuilder?.RequireAuthorization(AuthorizationAttribute?.PolicyName ?? config?.API?.AuthorizationPolicy ?? "");
                return;
            }
            EndPointBuilder = EndPointBuilder?.Produces(StatusCodes.Status401Unauthorized);
            EndPointBuilder = EndPointBuilder?.RequireAuthorization();
        }

        /// <summary>
        /// Determines if the endpoint should allow anonymous users access.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="HandlerType">Type of the handler.</param>
        /// <returns>True if it should, false otherwise.</returns>
        private static bool AllowAnonymous(MithrilConfig? config, Type HandlerType) => (config?.API?.AllowAnonymous ?? false) || HandlerType.GetCustomAttribute<ApiAllowAnonymousAttribute>() is not null;

        /// <summary>
        /// Sets up the content types accepted for the endpoint.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the view model.</typeparam>
        /// <param name="commandHandler">The command handler.</param>
        /// <param name="EndPointBuilder">The end point builder.</param>
        /// <returns>The content types accepted.</returns>
        private static RouteHandlerBuilder? SetupContentTypesAccepted<TViewModel>(ICommandHandler<TViewModel> commandHandler, RouteHandlerBuilder? EndPointBuilder) where TViewModel : notnull
        {
            if ((commandHandler.ContentTypeAccepts?.Length ?? 0) == 0)
                return EndPointBuilder;
            if (commandHandler.ContentTypeAccepts.Length > 1)
            {
                EndPointBuilder = EndPointBuilder.Accepts<TViewModel>(commandHandler.ContentTypeAccepts[0], commandHandler.ContentTypeAccepts[1..^1]);
                return EndPointBuilder;
            }
            EndPointBuilder = EndPointBuilder.Accepts<TViewModel>(commandHandler.ContentTypeAccepts[0]);
            return EndPointBuilder;
        }

        /// <summary>
        /// Determines if a policy should be used for authentication/authorization purposes on the endpoint.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="AuthorizationAttribute">The authorization attribute.</param>
        /// <returns>True if it should, false otherwise.</returns>
        private static bool UsePolicyForAuth(MithrilConfig? config, ApiAuthorizeAttribute? AuthorizationAttribute) => !string.IsNullOrEmpty(AuthorizationAttribute?.PolicyName) || !string.IsNullOrEmpty(config?.API?.AuthorizationPolicy);
    }
}