using Mithril.Core;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Web application builder extensions
    /// </summary>
    public static class WebApplicationBuilderExtensions
    {
        /// <summary>
        /// Gets the mithril application.
        /// </summary>
        /// <value>The mithril application.</value>
        private static Application? MithrilApp { get; set; }

        /// <summary>
        /// Sets up mithril framework and gets the system ready for use.
        /// </summary>
        /// <param name="webApplicationBuilder">The web application builder.</param>
        /// <returns>The web application.</returns>
        public static WebApplication? SetupMithril(this WebApplicationBuilder? webApplicationBuilder)
        {
            if (webApplicationBuilder is null)
                return null;

            // Create mithril app framework.
            MithrilApp ??= new Application(webApplicationBuilder.Configuration, webApplicationBuilder.Environment);

            // Configures the host settings
            MithrilApp.ConfigureHostSettings(webApplicationBuilder.Host);

            // Configures the WebHost settings
            MithrilApp.ConfigureWebHostSettings(webApplicationBuilder.WebHost);

            // Configures logging settings
            MithrilApp.ConfigureLoggingSettings(webApplicationBuilder.Logging);

            // Configure MVC
            MithrilApp.ConfigureMVC(webApplicationBuilder.Services);

            // Configure services for mithril.
            MithrilApp.ConfigureServices(webApplicationBuilder.Services);

            // Build the application object.
            var Application = webApplicationBuilder.Build();

            // Initialize any module specific data.
            MithrilApp.InitializeData(Application.Services);

            // Configure the application.
            return MithrilApp.ConfigureApplication(Application);
        }
    }
}