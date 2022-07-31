using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// IMvcBuilder extensions
    /// </summary>
    public static class IMvcBuilderExtensions
    {
        /// <summary>
        /// Adds the mithril options.
        /// </summary>
        /// <param name="mvcBuilder">The MVC builder.</param>
        /// <returns>The MVC builder</returns>
        public static IMvcBuilder AddMithrilOptions(this IMvcBuilder mvcBuilder) => mvcBuilder;
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
    /// Configures the services for MVC.
    /// </summary>
    /// <param name="services">The services collection.</param>
    /// <returns></returns>
    private static void ConfigureServices(IServiceCollection services)
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
}