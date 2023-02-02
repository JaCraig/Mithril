using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Mithril.API.Abstractions.Attributes;
using Mithril.API.Abstractions.Commands.Interfaces;
using Mithril.API.Abstractions.Configuration;
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
        /// <param name="apiOptions">The API options.</param>
        /// <returns></returns>
        public static void SetupEndPoint<TViewModel>(IEndpointRouteBuilder? endpoints, string commandEndPoint, ICommandHandler<TViewModel> commandHandler, MithrilConfig? config, APIOptions? apiOptions)
            where TViewModel : notnull
        {
            if (commandHandler is null || endpoints is null)
                return;
            commandEndPoint = CleanText(commandEndPoint);
            var CommandName = CleanText(commandHandler.CommandName, "/");
            var EndPointBuilder = endpoints.MapPost(commandEndPoint + CommandName, (
                                                        [FromServices] IDataService dataService,
                                                        [FromServices] ILogger<CommandModule> logger,
                                                        ClaimsPrincipal user,
                                                        TViewModel value) => CommandEndpoint.RequestDelegate(dataService, logger, user, commandHandler, value))
                                            .Produces<ReturnedResult>(StatusCodes.Status200OK, contentType: "application/json")
                                            .Produces<ReturnedResult>(StatusCodes.Status400BadRequest, contentType: "application/json")
                                            .WithName(CommandName)
                                            .WithTags(commandHandler.Tags);
            if (EndPointBuilder is null)
                return;
            EndPointBuilder = SetupContentTypesAccepted(commandHandler, EndPointBuilder);

            SetupAuthorization(apiOptions, EndPointBuilder, commandHandler.GetType());

            SetupSecurity(config, EndPointBuilder);
        }

        /// <summary>
        /// Determines if the endpoint should allow anonymous users access.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="HandlerType">Type of the handler.</param>
        /// <returns>True if it should, false otherwise.</returns>
        private static bool AllowAnonymous(APIOptions? config, Type HandlerType) => (config?.AllowAnonymous ?? false) || HandlerType.GetCustomAttribute<ApiAllowAnonymousAttribute>() is not null;

        /// <summary>
        /// Cleans the text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="extraCharactersToRemove">The extra characters to remove.</param>
        /// <returns></returns>
        private static string CleanText(string? text, params string[] extraCharactersToRemove)
        {
            extraCharactersToRemove ??= Array.Empty<string>();
            text = text?.Replace("{", "").Replace("}", "").Replace("?", "") ?? "";
            for (var x = 0; x < extraCharactersToRemove.Length; ++x)
            {
                text.Replace(extraCharactersToRemove[x], "");
            }
            return text;
        }

        /// <summary>
        /// Sets up the authorization.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="EndPointBuilder">The end point builder.</param>
        /// <param name="HandlerType">Type of the handler.</param>
        /// <returns></returns>
        private static void SetupAuthorization(APIOptions? config, RouteHandlerBuilder? EndPointBuilder, Type HandlerType)
        {
            if (EndPointBuilder is null)
                return;
            var AuthorizationAttribute = HandlerType.GetCustomAttribute<ApiAuthorizeAttribute>();
            var DefaultAuthorizationPolicy = config?.AuthorizationPolicy ?? "";

            if (AllowAnonymous(config, HandlerType))
            {
                EndPointBuilder.AllowAnonymous();
                return;
            }
            EndPointBuilder = EndPointBuilder.Produces(StatusCodes.Status401Unauthorized);
            if (UsePolicyForAuth(DefaultAuthorizationPolicy, AuthorizationAttribute))
            {
                EndPointBuilder.RequireAuthorization(AuthorizationAttribute?.PolicyName ?? DefaultAuthorizationPolicy);
                return;
            }
            EndPointBuilder.RequireAuthorization();
        }

        /// <summary>
        /// Sets up the content types accepted for the endpoint.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the view model.</typeparam>
        /// <param name="commandHandler">The command handler.</param>
        /// <param name="EndPointBuilder">The end point builder.</param>
        /// <returns>The content types accepted.</returns>
        private static RouteHandlerBuilder? SetupContentTypesAccepted<TViewModel>(ICommandHandler<TViewModel> commandHandler, RouteHandlerBuilder? EndPointBuilder) where TViewModel : notnull
        {
            if (commandHandler.ContentTypeAccepts is null || EndPointBuilder is null)
                return EndPointBuilder;
            if (commandHandler.ContentTypeAccepts.Length == 0)
                return EndPointBuilder;
            var ExtraArgs = commandHandler.ContentTypeAccepts.Length > 1 ? commandHandler.ContentTypeAccepts[1..^1] : Array.Empty<string>();
            return EndPointBuilder.Accepts<TViewModel>(commandHandler.ContentTypeAccepts[0], ExtraArgs);
        }

        /// <summary>
        /// Setups the security.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="endPointBuilder">The end point builder.</param>
        private static void SetupSecurity(MithrilConfig? config, RouteHandlerBuilder? endPointBuilder)
        {
            if (string.IsNullOrEmpty(config?.Security?.DefaultCorsPolicy) || endPointBuilder is null)
                return;
            endPointBuilder.RequireCors(config.Security.DefaultCorsPolicy);
        }

        /// <summary>
        /// Determines if a policy should be used for authentication/authorization purposes on the endpoint.
        /// </summary>
        /// <param name="defaultAuthorizationPolicy">The default authorization policy.</param>
        /// <param name="AuthorizationAttribute">The authorization attribute.</param>
        /// <returns>True if it should, false otherwise.</returns>
        private static bool UsePolicyForAuth(string defaultAuthorizationPolicy, ApiAuthorizeAttribute? AuthorizationAttribute) => !string.IsNullOrEmpty(AuthorizationAttribute?.PolicyName) || !string.IsNullOrEmpty(defaultAuthorizationPolicy);
    }
}