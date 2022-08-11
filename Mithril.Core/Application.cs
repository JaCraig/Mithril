﻿using BigBook;
using Canister.IoC.Default;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mithril.Core.Abstractions.Services;
using Mithril.Core.Extensions;
using System.Reflection;

namespace Mithril.Core
{
    /// <summary>
    /// Application info holder.
    /// </summary>
    public class Application
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Application"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="env">The host environment</param>
        public Application(IConfiguration configuration, IHostEnvironment env)
        {
            Name = Assembly.GetEntryAssembly()?.FullName ?? "";
            Modules = FindModules();
            Configuration = configuration;
            Environment = env;
            WebRootPath = env?.ContentRootPath ?? ".";
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Gets the environment.
        /// </summary>
        /// <value>The environment.</value>
        public IHostEnvironment Environment { get; }

        /// <summary>
        /// Gets the modules.
        /// </summary>
        /// <value>The modules.</value>
        public Abstractions.Modules.Interfaces.IModule[] Modules { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; }

        /// <summary>
        /// Gets or sets the web root path.
        /// </summary>
        /// <value>The web root path.</value>
        private string WebRootPath { get; }

        /// <summary>
        /// Allows configuration of MVC related items.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>The application object.</returns>
        public WebApplication? ConfigureApplication(WebApplication? app)
        {
            if (app is null || app.Lifetime is null)
                return app;

            // Turn on routing
            _ = app.UseRouting();

            //Configure modules
            for (int i = 0, ModulesLength = Modules.Length; i < ModulesLength; i++)
            {
                var Module = Modules[i];
                Module.ConfigureApplication(app, Configuration, Environment);
            }

            // Add authorization
            _ = app.UseAuthorization();

            // Set up endpoints
            _ = app.UseEndpoints(endpoints =>
            {
                //Module specific routes added
                for (int i = 0, ModulesLength = Modules.Length; i < ModulesLength; i++)
                {
                    var Module = Modules[i];
                    Module.ConfigureRoutes(endpoints, Configuration, Environment);
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
        public void ConfigureHostSettings(ConfigureHostBuilder host)
        {
            if (host is null)
                return;
            // By default set the service provider to Canister which is just a wrapper for the
            // default provider.
            host.UseServiceProviderFactory(new CanisterServiceProviderFactory());

            for (int i = 0, ModulesLength = Modules.Length; i < ModulesLength; i++)
            {
                var Module = Modules[i];
                Module.ConfigureHostSettings(host, Configuration, Environment);
            }
        }

        /// <summary>
        /// Configures the logging settings.
        /// </summary>
        /// <param name="logging">The logging.</param>
        public void ConfigureLoggingSettings(ILoggingBuilder logging)
        {
            if (logging is null)
                return;
            for (int i = 0, ModulesLength = Modules.Length; i < ModulesLength; i++)
            {
                var Module = Modules[i];
                Module.ConfigureLoggingSettings(logging, Configuration, Environment);
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
            var MVCBuilder = services.AddControllersWithViews()
                                    .When(Environment.IsDevelopment(), x => x.AddRazorRuntimeCompilation(options => SetupFileProviders(options.FileProviders)));

            //Add modules
            for (int i = 0, ModulesLength = Modules.Length; i < ModulesLength; i++)
            {
                var Module = Modules[i];
                Module.ConfigureMVC(MVCBuilder, Configuration, Environment);
                MVCBuilder?.AddApplicationPart(Module.GetType().Assembly);
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
            services.AddOptions();

            //Add modules
            for (int i = 0, ModulesLength = Modules.Length; i < ModulesLength; i++)
            {
                var Module = Modules[i];
                Module.ConfigureServices(services, Configuration, Environment);
            }
            services?.AddCanisterModules();
            return services;
        }

        /// <summary>
        /// Configures the web host settings.
        /// </summary>
        /// <param name="webHost">The web host.</param>
        public void ConfigureWebHostSettings(ConfigureWebHostBuilder webHost)
        {
            if (webHost is null)
                return;
            for (int i = 0, ModulesLength = Modules.Length; i < ModulesLength; i++)
            {
                var Module = Modules[i];
                Module.ConfigureWebHostSettings(webHost, Configuration, Environment);
            }
        }

        /// <summary>
        /// Initializes any data associated with the modules.
        /// </summary>
        /// <param name="services">The services.</param>
        public void InitializeData(IServiceProvider services)
        {
            AsyncHelper.RunSync(() => InitializeDataAsync(services));
        }

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
            var Assemblies = AssembliesFound.ToArray();
            var TempModules = new List<Abstractions.Modules.Interfaces.IModule>();
            for (int i = 0, AssembliesLength = Assemblies.Length; i < AssembliesLength; i++)
            {
                Assembly? TempAssembly = Assemblies[i];
                try
                {
                    var ModuleTypes = TempAssembly.DefinedTypes
                                                  .Where(x => x.Is<Abstractions.Modules.Interfaces.IModule>()
                                                           && x.HasDefaultConstructor());
                    TempModules.Add(ModuleTypes.Create<Abstractions.Modules.Interfaces.IModule>());
                }
                catch { }
            }
            return TempModules.OrderBy(x => x.Order).ToArray();
        }

        /// <summary>
        /// Initializes the data.
        /// </summary>
        /// <param name="services">The services.</param>
        private async Task InitializeDataAsync(IServiceProvider services)
        {
            var DataService = services.GetRequiredService<IDataService>();
            for (int i = 0, ModulesLength = Modules.Length; i < ModulesLength; i++)
            {
                var Module = Modules[i];
                await Module.InitializeDataAsync(DataService).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Called when [started].
        /// </summary>
        private void OnStarted()
        {
            for (int i = 0, ModulesLength = Modules.Length; i < ModulesLength; i++)
            {
                var Module = Modules[i];
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
                var Module = Modules[i];
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
                var Module = Modules[i];
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