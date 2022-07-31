namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Web application extensions
    /// </summary>
    public static class IApplicationBuilderExtensions
    {
        /// <summary>
        /// Configures the mithril.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <returns>The web app.</returns>
        public static IApplicationBuilder ConfigureMithril(this IApplicationBuilder application) => application;

        /// <summary>
        /// Allows configuration of MVC related items.
        /// </summary>
        /// <param name="builder">The application builder.</param>
        /// <param name="applicationLifetime">The application lifetime.</param>
        public void Configure(IApplicationBuilder builder, IHostApplicationLifetime applicationLifetime)
        {
            if (builder is null || applicationLifetime is null)
                return;
            Log.Information("Mithril: Initializing data.");
            Canister.Builder.Bootstrapper?.Resolve<Session>();
            InitializeDataAsync().ConfigureAwait(false).GetAwaiter().GetResult();

            Log.Information("Mithril: Running module configuration on the app/environment.");

            _ = builder.UseRouting();

            //Configure modules
            for (int i = 0, ModulesLength = Modules.Length; i < ModulesLength; i++)
            {
                var Module = Modules[i];
                Module.Configure(builder, Environment);
            }

            Log.Information("Mithril: Setting up routes");

            _ = builder.UseAuthorization();
            builder.UseEndpoints(endpoints =>
            {
                //Module specific routes added
                for (int i = 0, ModulesLength = Modules.Length; i < ModulesLength; i++)
                {
                    var Module = Modules[i];
                    Module.ConfigureRoutes(endpoints);
                }
            });

            applicationLifetime.ApplicationStopping.Register(OnShutdown);
        }
    }
}