using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Mithril.Core.Abstractions.Modules.Interfaces
{
    /// <summary>
    /// Module interface. Defines a module and initializes it.
    /// </summary>
    public interface IModule : Canister.Interfaces.IModule
    {
        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        string Category { get; }

        /// <summary>
        /// The content path
        /// </summary>
        /// <value>The content path.</value>
        string ContentPath { get; }

        /// <summary>
        /// Gets the features.
        /// </summary>
        /// <value>The features.</value>
        IFeature[] Features { get; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        string ID { get; }

        /// <summary>
        /// Gets the last modified.
        /// </summary>
        /// <value>The last modified.</value>
        DateTime LastModified { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Gets the tags.
        /// </summary>
        /// <value>The tags.</value>
        string[] Tags { get; }

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>The version.</value>
        string Version { get; }

        /// <summary>
        /// Configures the application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        void ConfigureApplication(IApplicationBuilder app, IConfiguration configuration, IHostEnvironment environment);

        /// <summary>
        /// Configures the host settings.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        void ConfigureHostSettings(ConfigureHostBuilder host, IConfiguration configuration, IHostEnvironment environment);

        /// <summary>
        /// Configures the logging settings.
        /// </summary>
        /// <param name="logging">The logging.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        void ConfigureLoggingSettings(ILoggingBuilder logging, IConfiguration configuration, IHostEnvironment environment);

        /// <summary>
        /// Configures the MVC.
        /// </summary>
        /// <param name="mvcBuilder">The MVC builder.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        void ConfigureMVC(IMvcBuilder? mvcBuilder, IConfiguration configuration, IHostEnvironment environment);

        /// <summary>
        /// Configures the routes.
        /// </summary>
        /// <param name="endpoints">The endpoints.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        void ConfigureRoutes(IEndpointRouteBuilder endpoints, IConfiguration configuration, IHostEnvironment environment);

        /// <summary>
        /// Configures the services for the module.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        void ConfigureServices(IServiceCollection services, IConfiguration configuration, IHostEnvironment environment);

        /// <summary>
        /// Configures the web host settings.
        /// </summary>
        /// <param name="webHost">The web host.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        void ConfigureWebHostSettings(ConfigureWebHostBuilder webHost, IConfiguration configuration, IHostEnvironment environment);

        /// <summary>
        /// Initializes the data.
        /// </summary>
        /// <returns>The async task.</returns>
        Task InitializeDataAsync();

        /// <summary>
        /// Called when the application is [started].
        /// </summary>
        void OnStarted();

        /// <summary>
        /// Called when the application is [stopped].
        /// </summary>
        void OnStopped();

        /// <summary>
        /// Called when the application is [stopping].
        /// </summary>
        void OnStopping();
    }
}