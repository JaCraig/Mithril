using BigBook;
using Canister.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
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
            Name = Assembly.GetEntryAssembly()?.FullName ?? string.Empty;
            Assemblies = Array.Empty<Assembly>();
            Modules = Array.Empty<Abstractions.Modules.Interfaces.IModule>();
            FindModules();
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
        public Abstractions.Modules.Interfaces.IModule[] Modules { get; private set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; }

        /// <summary>
        /// Gets the log.
        /// </summary>
        /// <value>The log.</value>
        private static ILogger Log => Canister.Builder.Bootstrapper?.Resolve<ILogger>() ?? Serilog.Log.Logger;

        /// <summary>
        /// Gets or sets the assemblies.
        /// </summary>
        /// <value>The assemblies.</value>
        private Assembly[] Assemblies { get; set; }

        /// <summary>
        /// Gets or sets the web root path.
        /// </summary>
        /// <value>The web root path.</value>
        private string WebRootPath { get; }

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
            _ = builder.UseEndpoints(endpoints =>
            {
                //Module specific routes added
                for (int i = 0, ModulesLength = Modules.Length; i < ModulesLength; i++)
                {
                    var Module = Modules[i];
                    Module.ConfigureRoutes(endpoints);
                }
            });

            _ = applicationLifetime.ApplicationStopping.Register(OnShutdown);
        }

        /// <summary>
        /// Configures the services for MVC.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <returns></returns>
        public void ConfigureServices(IServiceCollection services)
        {
            var MVCBuilder = services.AddControllersWithViews().AddNewtonsoftJson();

            MVCBuilder.AddMvcOptions(options =>
            {
                options.InputFormatters
                        .Where(item => item.GetType() == typeof(NewtonsoftJsonInputFormatter))
                        .Cast<NewtonsoftJsonInputFormatter>()
                        .Single()
                        .SupportedMediaTypes
                        .Add("application/csp-report");
            });

            //Set up razor so runtime compilation occurs.
            MVCBuilder.AddRazorRuntimeCompilation(options => SetupFileProviders(options.FileProviders));

            //Add services
            for (int i = 0, ModulesLength = Modules.Length; i < ModulesLength; i++)
            {
                var Module = Modules[i];
                Module.ConfigureServices(services, Configuration, Environment);
                MVCBuilder.AddApplicationPart(Module.GetType().Assembly);
            }
        }

        /// <summary>
        /// Setups the file providers.
        /// </summary>
        /// <param name="fileProviders">The file providers.</param>
        public void SetupFileProviders(IList<IFileProvider> fileProviders)
        {
            if (fileProviders is null)
                return;

            for (var i = 0; i < Modules.Length; i++)
            {
                var libraryPath = Path.GetFullPath(Path.Combine(WebRootPath, "..", Modules[i].GetType().Assembly.GetName().Name ?? string.Empty));
                if (new DirectoryInfo(libraryPath).Exists)
                    fileProviders.Add(new PhysicalFileProvider(libraryPath));
            }
        }

        /// <summary>
        /// Starts up the system using the specified assemblies.
        /// </summary>
        /// <param name="bootstrapper">The bootstrapper.</param>
        public void Startup(IBootstrapper bootstrapper)
        {
            var Builder = bootstrapper
                            ?.RegisterInflatable()
                            ?.RegisterSerialBox()
                            ?.RegisterFileCurator()
                            ?.RegisterBigBookOfDataTypes()
                            ?.RegisterSimpleHtmlToPdf()
                            ?.AddAssembly(Assemblies);

            for (int i = 0, ModulesLength = Modules.Length; i < ModulesLength; i++)
            {
                var Module = Modules[i];
                Module.ConfigureCanister(Builder);
            }
        }

        /// <summary>
        /// Finds the modules.
        /// </summary>
        private void FindModules()
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
            Assemblies = AssembliesFound.ToArray();
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
            Modules = TempModules.OrderBy(x => x.Order).ToArray();
        }

        /// <summary>
        /// Initializes the data.
        /// </summary>
        /// <returns></returns>
        private async Task InitializeDataAsync()
        {
            var FeaturesFound = Feature.All();

            var ModuleFeatures = Modules.SelectMany(x => x.Features);

            foreach (var ModuleFeature in ModuleFeatures)
            {
                if (!FeaturesFound.Any(x => x.Identifier == ModuleFeature.Id))
                {
                    var TempFeature = new Feature(ModuleFeature.Name ?? ModuleFeature.Id, ModuleFeature.Id, ModuleFeature.Category)
                    {
                        Description = ModuleFeature.Description,
                    };
                    await TempFeature.SaveAsync().ConfigureAwait(false);
                }
            }
            foreach (var ModuleFeature in FeaturesFound)
            {
                if (!ModuleFeatures.Any(x => x.Id == ModuleFeature.Identifier))
                {
                    ModuleFeature.Delete(false);
                }
            }
            FeaturesFound = Feature.All();
            foreach (var ModuleFeature in ModuleFeatures)
            {
                var TempFeature = FeaturesFound.FirstOrDefault(x => x.Identifier == ModuleFeature.Id);
                if (TempFeature is null)
                    continue;

                await TempFeature.SaveAsync().ConfigureAwait(false);
            }

            for (int i = 0, ModulesLength = Modules.Length; i < ModulesLength; i++)
            {
                var Module = Modules[i];
                await Module.InitializeDataAsync().ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Called when [shutdown].
        /// </summary>
        private void OnShutdown()
        {
            for (int i = 0, ModulesLength = Modules.Length; i < ModulesLength; i++)
            {
                var Module = Modules[i];
                Module.Shutdown();
            }
        }
    }
}