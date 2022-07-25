namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Service collection extensions.
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Setups Mithril services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>The services object.</returns>
        public static IServiceCollection SetupMithril(this IServiceCollection services)
        {
            return services;
        }
    }
}