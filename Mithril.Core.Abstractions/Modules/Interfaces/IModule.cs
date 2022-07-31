using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

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
        /// Allows configuration of MVC related items.
        /// </summary>
        /// <param name="builder">The application builder.</param>
        void Configure(IApplicationBuilder builder);

        /// <summary>
        /// Configures the services for MVC.
        /// </summary>
        /// <param name="services">The services collection.</param>
        void ConfigureServices(IServiceCollection services);

        /// <summary>
        /// Initializes the data.
        /// </summary>
        /// <returns>The async task.</returns>
        Task InitializeDataAsync();

        /// <summary>
        /// Shutdowns this instance.
        /// </summary>
        void Shutdown();
    }
}