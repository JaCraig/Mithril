using GraphQL.Server.Transports.AspNetCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Mithril.API.GraphQL.Authorization
{
    /// <summary>
    /// User context builder
    /// </summary>
    /// <seealso cref="IUserContextBuilder"/>
    public class GraphQLUserContextBuilder : IUserContextBuilder
    {
        /// <summary>
        /// Builds the user context object.
        /// </summary>
        /// <param name="context">
        /// The <see cref="T:Microsoft.AspNetCore.Http.HttpContext"/> of the HTTP connection.
        /// </param>
        /// <param name="payload">
        /// The payload of the WebSocket connection request, if applicable. Typically this is either
        /// <see langword="null"/> or an object that has not fully been deserialized yet; when using
        /// the Newtonsoft.Json deserializer, this will be a JObject, and when using
        /// System.Text.Json this will be a JsonElement. You may call <see
        /// cref="M:GraphQL.IGraphQLSerializer.ReadNode``1(System.Object)"/> to deserialize the node
        /// into the expected type. To deserialize into a nested set of <see
        /// cref="T:System.Collections.Generic.IDictionary`2">IDictionary&lt;string,
        /// object?&gt;</see> maps, call <see
        /// cref="M:GraphQL.IGraphQLSerializer.ReadNode``1(System.Object)"/> with <see
        /// cref="T:GraphQL.Inputs"/> as the generic type. <br/><br/> To determine if this is a
        /// WebSocket connection request, check <paramref name="context"/>. <see
        /// cref="P:Microsoft.AspNetCore.Http.HttpContext.WebSockets">WebSockets</see>. <see cref="P:Microsoft.AspNetCore.Http.WebSocketManager.IsWebSocketRequest">IsWebSocketRequest</see>.
        /// </param>
        /// <returns>
        /// Dictionary object representing user context. Return <see langword="null"/> to use
        /// default user context.
        /// </returns>
        /// <inheritdoc cref="T:GraphQL.Server.Transports.AspNetCore.IUserContextBuilder"/>
        public ValueTask<IDictionary<string, object?>?> BuildUserContextAsync(HttpContext context, object? payload) => ValueTask.FromResult((IDictionary<string, object?>?)new GraphQLUserContextDictionary(context?.User));
    }

    /// <summary>
    /// GraphQL User Context Holder
    /// </summary>
    /// <seealso cref="Dictionary&lt;String, Object&gt;"/>
    public class GraphQLUserContextDictionary : Dictionary<string, object?>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphQLUserContextDictionary"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        public GraphQLUserContextDictionary(ClaimsPrincipal? user)
        {
            User = user;
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <value>The user.</value>
        public ClaimsPrincipal? User { get; }
    }
}