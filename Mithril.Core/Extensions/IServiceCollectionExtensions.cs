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
        public static IServiceCollection RegisterMithril(this IServiceCollection services)
        {
            _ = services.AddCanisterModules();
            return services;
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

        private static void ConfigureServices(IServiceCollection services)
        {
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
}