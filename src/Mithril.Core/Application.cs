using BigBook;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mithril.Data.Abstractions.Services;
using System.Reflection;

namespace Mithril.Core
{
    /// <summary>
    /// Application info holder.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="Application"/> class.
    /// </remarks>
    /// <param name="configuration">The configuration.</param>
    /// <param name="env">The host environment</param>
    public class Application(IConfiguration? configuration, IHostEnvironment? env)
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        public IConfiguration? Configuration { get; } = configuration;

        /// <summary>
        /// Gets the environment.
        /// </summary>
        /// <value>The environment.</value>
        public IHostEnvironment? Environment { get; } = env;

        /// <summary>
        /// Gets the modules.
        /// </summary>
        /// <value>The modules.</value>
        public Abstractions.Modules.Interfaces.IModule[] Modules { get; } = FindModules();

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; } = Assembly.GetEntryAssembly()?.FullName ?? "";

        /// <summary>
        /// Gets or sets the web root path.
        /// </summary>
        /// <value>The web root path.</value>
        private string WebRootPath { get; } = env?.ContentRootPath ?? ".";

        /// <summary>
        /// Allows configuration of MVC related items.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>The application object.</returns>
        public WebApplication? ConfigureApplication(WebApplication? app)
        {
            if (app is null || app.Lifetime is null)
                return app;
            IApplicationBuilder? ApplicationBuilder = app;

            // Turn on routing
            ApplicationBuilder = ApplicationBuilder?.UseRouting();

            //Configure modules
            for (int i = 0, ModulesLength = Modules.Length; i < ModulesLength; i++)
            {
                Abstractions.Modules.Interfaces.IModule Module = Modules[i];
                ApplicationBuilder = Module.ConfigureApplication(ApplicationBuilder, Configuration, Environment);
            }

            // Set up endpoints
            ApplicationBuilder = ApplicationBuilder?.UseEndpoints(endpoints =>
            {
                //Module specific routes added
                for (int i = 0, ModulesLength = Modules.Length; i < ModulesLength; i++)
                {
                    Abstractions.Modules.Interfaces.IModule Module = Modules[i];
                    endpoints = Module.ConfigureRoutes(endpoints, Configuration, Environment) ?? endpoints;
                }
            });

            // Set up application lifetime events
            _ = app.Lifetime.ApplicationStarted.Register(OnStarted);
            _ = app.Lifetime.ApplicationStopped.Register(OnStopped);
            _ = app.Lifetime.ApplicationStopping.Register(OnStopping);

            return app;
        }

        /// <summary>
        /// Configures the host settings.
        /// </summary>
        /// <param name="host">The host.</param>
        public void ConfigureHostSettings(IHostBuilder? host)
        {
            if (host is null)
                return;

            for (int i = 0, ModulesLength = Modules.Length; i < ModulesLength; i++)
            {
                Abstractions.Modules.Interfaces.IModule Module = Modules[i];
                host = Module.ConfigureHostSettings(host, Configuration, Environment);
            }
        }

        /// <summary>
        /// Configures the logging settings.
        /// </summary>
        /// <param name="logging">The logging.</param>
        public void ConfigureLoggingSettings(ILoggingBuilder? logging)
        {
            if (logging is null)
                return;
            for (int i = 0, ModulesLength = Modules.Length; i < ModulesLength; i++)
            {
                Abstractions.Modules.Interfaces.IModule Module = Modules[i];
                logging = Module.ConfigureLoggingSettings(logging, Configuration, Environment);
            }
        }

        /// <summary>
        /// Configures the MVC.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>The services</returns>
        public IServiceCollection? ConfigureMVC(IServiceCollection services)
        {
            if (Configuration is null || Environment is null || services is null)
                return services;

            // MVC Builder setup with debug runtime compilation if needed.
            IMvcBuilder? MVCBuilder = services.AddControllersWithViews(options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()))
                                    .When(Environment.IsDevelopment(), x => x.AddRazorRuntimeCompilation(options => SetupFileProviders(options.FileProviders)));

            //Add modules
            for (int i = 0, ModulesLength = Modules.Length; i < ModulesLength; i++)
            {
                Abstractions.Modules.Interfaces.IModule Module = Modules[i];
                MVCBuilder = Module.ConfigureMVC(MVCBuilder, Configuration, Environment);
                _ = (MVCBuilder?.AddApplicationPart(Module.GetType().Assembly));
            }
            return services;
        }

        /// <summary>
        /// Configures the services for MVC.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <returns>The service collection</returns>
        public IServiceCollection? ConfigureServices(IServiceCollection? services)
        {
            if (Configuration is null || Environment is null || services is null)
                return services;

            // Add options
            services = services.AddOptions();

            //Add modules
            for (int i = 0, ModulesLength = Modules.Length; i < ModulesLength; i++)
            {
                Abstractions.Modules.Interfaces.IModule Module = Modules[i];
                services = services?.AddSingleton(Module);
            }
            for (int i = 0, ModulesLength = Modules.Length; i < ModulesLength; i++)
            {
                Abstractions.Modules.Interfaces.IModule Module = Modules[i];
                services = Module.ConfigureServices(services, Configuration, Environment);
            }
            return services?.AddCanisterModules();
        }

        /// <summary>
        /// Configures the web host settings.
        /// </summary>
        /// <param name="webHost">The web host.</param>
        public void ConfigureWebHostSettings(IWebHostBuilder? webHost)
        {
            if (webHost is null)
                return;
            for (int i = 0, ModulesLength = Modules.Length; i < ModulesLength; i++)
            {
                Abstractions.Modules.Interfaces.IModule Module = Modules[i];
                webHost = Module.ConfigureWebHostSettings(webHost, Configuration, Environment);
            }
        }

        /// <summary>
        /// Initializes any data associated with the modules.
        /// </summary>
        /// <param name="services">The services.</param>
        public void InitializeData(IServiceProvider? services) => AsyncHelper.RunSync(() => InitializeDataAsync(services));

        /// <summary>
        /// Finds the modules.
        /// </summary>
        /// <returns>The modules</returns>
        private static Abstractions.Modules.Interfaces.IModule[] FindModules()
        {
            var AssembliesFound = new List<Assembly>
            {
                Assembly.GetEntryAssembly()!
            };
            var Temp = typeof(Application).Assembly.Location;
            foreach (FileInfo? TempAssembly in new FileInfo(Temp).Directory?.EnumerateFiles("*.dll", SearchOption.TopDirectoryOnly) ?? Array.Empty<FileInfo>())
            {
                try
                {
                    AssembliesFound.Add(Assembly.Load(AssemblyName.GetAssemblyName(TempAssembly.FullName)));
                }
                catch { }
            }
            Assembly[] Assemblies = AssembliesFound.ToArray();
            var TempModules = new List<Abstractions.Modules.Interfaces.IModule>();
            for (int i = 0, AssembliesLength = Assemblies.Length; i < AssembliesLength; i++)
            {
                Assembly? TempAssembly = Assemblies[i];
                try
                {
                    IEnumerable<TypeInfo> ModuleTypes = TempAssembly.DefinedTypes
                                                  .Where(x => x.Is<Abstractions.Modules.Interfaces.IModule>()
                                                           && x.HasDefaultConstructor());
                    _ = TempModules.Add(ModuleTypes.Create<Abstractions.Modules.Interfaces.IModule>());
                }
                catch { }
            }
            return TempModules.OrderBy(x => x.Order).ToArray();
        }

        /// <summary>
        /// Initializes the data.
        /// </summary>
        /// <param name="services">The services.</param>
        private async Task InitializeDataAsync(IServiceProvider? services)
        {
            IDataService? DataService = services?.GetService<IDataService>();
            if (DataService is null)
                return;
            for (int i = 0, ModulesLength = Modules.Length; i < ModulesLength; i++)
            {
                Abstractions.Modules.Interfaces.IModule Module = Modules[i];
                await Module.InitializeDataAsync(DataService, services).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Called when [started].
        /// </summary>
        private void OnStarted()
        {
            for (int i = 0, ModulesLength = Modules.Length; i < ModulesLength; i++)
            {
                Abstractions.Modules.Interfaces.IModule Module = Modules[i];
                Module.OnStarted();
            }
        }

        /// <summary>
        /// Called when [stopped].
        /// </summary>
        private void OnStopped()
        {
            for (int i = 0, ModulesLength = Modules.Length; i < ModulesLength; i++)
            {
                Abstractions.Modules.Interfaces.IModule Module = Modules[i];
                Module.OnStopped();
            }
        }

        /// <summary>
        /// Called when [shutdown].
        /// </summary>
        private void OnStopping()
        {
            for (int i = 0, ModulesLength = Modules.Length; i < ModulesLength; i++)
            {
                Abstractions.Modules.Interfaces.IModule Module = Modules[i];
                Module.OnStopping();
            }
        }

        /// <summary>
        /// Setups the file providers.
        /// </summary>
        /// <param name="fileProviders">The file providers.</param>
        private void SetupFileProviders(IList<IFileProvider> fileProviders)
        {
            if (fileProviders is null)
                return;

            for (var i = 0; i < Modules.Length; i++)
            {
                var libraryPath = Path.GetFullPath(Path.Combine(WebRootPath, "..", Modules[i].GetType().Assembly.GetName().Name ?? ""));
                if (new DirectoryInfo(libraryPath).Exists)
                    fileProviders.Add(new PhysicalFileProvider(libraryPath));
            }
        }
    }
}