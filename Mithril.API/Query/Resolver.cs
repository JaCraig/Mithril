using System.Security.Claims;

namespace Mithril.API.Query
{
    /// <summary>
    /// Resolver
    /// </summary>
    /// <typeparam name="TClass">The type of the class.</typeparam>
    public class Resolver<TClass>
        where TClass : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Resolver{TClass}"/> class.
        /// </summary>
        /// <param name="resolveMethod">The resolve method.</param>
        public Resolver(Func<ClaimsPrincipal?, Arguments, Task<TClass?>> resolveMethod)
        {
            ResolveMethod = resolveMethod;
        }

        /// <summary>
        /// Gets the resolve method.
        /// </summary>
        /// <value>The resolve method.</value>
        private Func<ClaimsPrincipal?, Arguments, Task<TClass?>> ResolveMethod { get; }

        /// <summary>
        /// Resolves the asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public Task<TClass?> ResolveAsync(ClaimsPrincipal? user, Arguments arguments)
        {
            return ResolveMethod(user, arguments);
        }
    }
}