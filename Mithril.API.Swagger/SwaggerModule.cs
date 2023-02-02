using BigBook;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Mithril.API.Abstractions.Configuration;
using Mithril.Core.Abstractions.Extensions;
using Mithril.Core.Abstractions.Modules.BaseClasses;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Mithril.API.Swagger
{
    /// <summary>
    /// Swagger module
    /// </summary>
    /// <seealso cref="ModuleBaseClass&lt;SwaggerModule&gt;"/>
    public class SwaggerModule : ModuleBaseClass<SwaggerModule>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SwaggerModule"/> class.
        /// </summary>
        public SwaggerModule()
            : base("Swagger Module", "API", "API", "Swagger")
        {
        }

        /// <summary>
        /// Configures the application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>Application builder</returns>
        public override IApplicationBuilder? ConfigureApplication(IApplicationBuilder? app, IConfiguration? configuration, IHostEnvironment? environment)
        {
            var SystemConfig = configuration.GetSystemConfig();
            var APIConfig = configuration.GetConfig<APIOptions>("Mithril:API");
            var EntryAssembly = Assembly.GetEntryAssembly();
            var EntryAssemblyName = EntryAssembly?.GetName().Name ?? "Mithril";
            return app?.When(environment?.IsDevelopment() ?? false, app =>
            {
                app?.UseSwagger()
                       .UseSwaggerUI(conf => conf.SwaggerEndpoint(APIConfig?.OpenAPIEndpoint ?? $"/swagger/v{EntryAssembly?.GetName().Version}/swagger.json",
                                                                  SystemConfig?.ApplicationName ?? $"{EntryAssemblyName} API v{EntryAssembly?.GetName().Version}"));
            });
        }

        /// <summary>
        /// Configures the services for the module.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>Services</returns>
        public override IServiceCollection? ConfigureServices(IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? environment)
        {
            var SystemConfig = configuration.GetSystemConfig();
            var EntryAssembly = Assembly.GetEntryAssembly();
            var EntryAssemblyName = EntryAssembly?.GetName().Name ?? "Mithril";
            services?.AddEndpointsApiExplorer();
            services?.AddSwaggerGen(options =>
            {
                options.SwaggerDoc($"v{EntryAssembly?.GetName().Version}", new OpenApiInfo
                {
                    Title = SystemConfig?.ApplicationName ?? $"{EntryAssemblyName} API v{EntryAssembly?.GetName().Version}",
                    Version = $"v{EntryAssembly?.GetName().Version}",
                    Description = SystemConfig?.ApplicationDescription ?? $"API endpoints for {EntryAssemblyName}.",
                });
                if (EntryAssembly is not null)
                {
                    ScanForCommentFiles(new FileInfo(EntryAssembly?.Location ?? "").Directory?.FullName, options);
                }
            });
            return services;
        }

        /// <summary>
        /// Scans for comment files.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="options">The options.</param>
        private static void ScanForCommentFiles(string? directory, SwaggerGenOptions options)
        {
            if (string.IsNullOrEmpty(directory))
                return;
            var AssemblyDirectories = new DirectoryInfo(directory);
            foreach (var File in AssemblyDirectories.EnumerateFiles("*.xml"))
            {
                options.IncludeXmlComments(File.FullName);
            }
        }
    }
}