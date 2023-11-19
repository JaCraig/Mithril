using BigBook;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mithril.Core.Abstractions.Modules.Interfaces;
using Mithril.Data.Abstractions.Services;

namespace Mithril.Core.Abstractions.Modules.BaseClasses
{
    /// <summary>
    /// Module base class
    /// </summary>
    /// <typeparam name="TModule">The type of the module.</typeparam>
    /// <seealso cref="IModule"/>
    /// <seealso cref="IEquatable{TModule}"/>
    /// <seealso cref="IEquatable{ModuleBaseClass}"/>
    /// <seealso cref="IModule"/>
    public abstract class ModuleBaseClass<TModule> : IEquatable<TModule>, IModule
        where TModule : ModuleBaseClass<TModule>, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleBaseClass{TModule}"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="category">The category.</param>
        /// <param name="tags">The tags.</param>
        protected ModuleBaseClass(string? name, string? category, params string[] tags)
        {
            Name = name ?? typeof(TModule).GetName().AddSpaces();
            Category = category ?? typeof(TModule).Namespace?.Split(".", StringSplitOptions.RemoveEmptyEntries).Skip(1).FirstOrDefault() ?? "";
            Tags = tags ?? [];
            ContentPath = $"wwwroot/Content/{ID}/";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleBaseClass{TModule}"/> class.
        /// </summary>
        protected ModuleBaseClass()
            : this(null, null)
        {
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        public string Category { get; protected set; }

        /// <summary>
        /// The content path
        /// </summary>
        /// <value>The content path.</value>
        public string ContentPath { get; protected set; }

        /// <summary>
        /// Gets the features.
        /// </summary>
        /// <value>The features.</value>
        public virtual IFeature[] Features { get; protected set; } = [];

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string ID { get; protected set; } = typeof(TModule)
            .GetName()
            .Trim()
            .Replace(" ", "-", StringComparison.OrdinalIgnoreCase)
            .Replace("--", "-", StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Gets the last modified.
        /// </summary>
        /// <value>The last modified.</value>
        public DateTime LastModified { get; } = new FileInfo(typeof(TModule).Assembly.Location).LastWriteTimeUtc;

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets the order that they are initialized in.
        /// </summary>
        /// <value>The order that they are initialized in.</value>
        public virtual int Order { get; protected set; }

        /// <summary>
        /// Gets the tags.
        /// </summary>
        /// <value>The tags.</value>
        public string[] Tags { get; protected set; } = [];

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>The version.</value>
        public string Version { get; } = typeof(TModule).Assembly.GetName().Version?.ToString() ?? "";

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="class1">The class1.</param>
        /// <param name="class2">The class2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(ModuleBaseClass<TModule>? class1, ModuleBaseClass<TModule>? class2) => !(class1 == class2);

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="class1">The class1.</param>
        /// <param name="class2">The class2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(ModuleBaseClass<TModule>? class1, ModuleBaseClass<TModule>? class2) => EqualityComparer<ModuleBaseClass<TModule>>.Default.Equals(class1, class2);

        /// <summary>
        /// Configures the application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>Application builder</returns>
        public virtual IApplicationBuilder? ConfigureApplication(IApplicationBuilder? app, IConfiguration? configuration, IHostEnvironment? environment) => app;

        /// <summary>
        /// Configures the host settings.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>Host builder</returns>
        public virtual IHostBuilder? ConfigureHostSettings(IHostBuilder? host, IConfiguration? configuration, IHostEnvironment? environment) => host;

        /// <summary>
        /// Configures the logging settings.
        /// </summary>
        /// <param name="logging">The logging.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>Logging builder</returns>
        public virtual ILoggingBuilder? ConfigureLoggingSettings(ILoggingBuilder? logging, IConfiguration? configuration, IHostEnvironment? environment) => logging;

        /// <summary>
        /// Configures the MVC.
        /// </summary>
        /// <param name="mvcBuilder">The MVC builder.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>MVC Builder</returns>
        public virtual IMvcBuilder? ConfigureMVC(IMvcBuilder? mvcBuilder, IConfiguration? configuration, IHostEnvironment? environment) => mvcBuilder;

        /// <summary>
        /// Configures the routes.
        /// </summary>
        /// <param name="endpoints">The endpoints.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>Endpoint route builder</returns>
        public virtual IEndpointRouteBuilder? ConfigureRoutes(IEndpointRouteBuilder? endpoints, IConfiguration? configuration, IHostEnvironment? environment) => endpoints;

        /// <summary>
        /// Configures the services for the module.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>Services</returns>
        public virtual IServiceCollection? ConfigureServices(IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? environment) => services;

        /// <summary>
        /// Configures the web host settings.
        /// </summary>
        /// <param name="webHost">The web host.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>Web host builder.</returns>
        public virtual IWebHostBuilder? ConfigureWebHostSettings(IWebHostBuilder? webHost, IConfiguration? configuration, IHostEnvironment? environment) => webHost;

        /// <summary>
        /// Determines whether the specified <see cref="object"/>, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj) => Equals(obj as TModule);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref>
        /// parameter; otherwise, false.
        /// </returns>
        public bool Equals(TModule? other)
        {
            return other is not null
                   && ID == other.ID;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data
        /// structures like a hash table.
        /// </returns>
        public override int GetHashCode() => HashCode.Combine(ID);

        /// <summary>
        /// Initializes the data.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        /// <param name="services">The services for the application.</param>
        /// <returns>The async task.</returns>
        public virtual Task InitializeDataAsync(IDataService? dataService, IServiceProvider? services) => Task.CompletedTask;

        /// <summary>
        /// Called when the application is [started].
        /// </summary>
        public virtual void OnStarted()
        { }

        /// <summary>
        /// Called when the application is [stopped].
        /// </summary>
        public virtual void OnStopped()
        { }

        /// <summary>
        /// Called when the application is [stopping].
        /// </summary>
        public virtual void OnStopping()
        { }
    }
}