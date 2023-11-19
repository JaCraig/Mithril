using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mithril.API.Abstractions.Attributes;
using Mithril.API.Abstractions.Commands.Interfaces;
using Mithril.API.Abstractions.Configuration;
using Mithril.API.Abstractions.Services;
using Mithril.API.Commands.Services;
using Mithril.API.Commands.Utils;
using Mithril.Core.Abstractions.Extensions;
using Mithril.Core.Abstractions.Modules.BaseClasses;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Mithril.API.Commands
{
    /// <summary>
    /// Command module
    /// </summary>
    /// <seealso cref="ModuleBaseClass&lt;CommandModule&gt;"/>
    public class CommandModule : ModuleBaseClass<CommandModule>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandModule"/> class.
        /// </summary>
        public CommandModule()
            : base("Command Module", "API", "API", "CQRS", "Command")
        {
        }

        /// <summary>
        /// Gets or sets the services.
        /// </summary>
        /// <value>The services.</value>
        private IServiceCollection? Services { get; set; }

        /// <summary>
        /// Configures the MVC.
        /// </summary>
        /// <param name="mvcBuilder">The MVC builder.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>
        /// MVC Builder
        /// </returns>
        public override IMvcBuilder? ConfigureMVC(IMvcBuilder? mvcBuilder, IConfiguration? configuration, IHostEnvironment? environment)
        {
            return mvcBuilder is null
                ? mvcBuilder
                : mvcBuilder.AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
        }

        /// <summary>
        /// Configures the routes.
        /// </summary>
        /// <param name="endpoints">The endpoints.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The environment.</param>
        /// <returns>Endpoint route builder</returns>
        public override IEndpointRouteBuilder? ConfigureRoutes(IEndpointRouteBuilder? endpoints, IConfiguration? configuration, IHostEnvironment? environment)
        {
            if (endpoints is null || Services is null)
                return endpoints;
            MethodInfo? EndPointMethod = typeof(CommandEndpointBuilder).GetMethod(nameof(CommandEndpointBuilder.SetupEndPoint), BindingFlags.Static | BindingFlags.Public);
            IServiceProvider TempProvider = endpoints.ServiceProvider;
            APIOptions? SystemConfig = configuration.GetConfig<APIOptions>("Mithril:API");
            var CommandEndpoint = SystemConfig?.CommandEndpoint ?? "/api/command/";
            foreach (IGrouping<string?, ICommandHandler> Versions in TempProvider.GetServices<ICommandHandler>().Where(x => x.GetType().GetCustomAttribute<ApiIgnoreAttribute>() is null).GroupBy(x => x.Version))
            {
                foreach (ICommandHandler? Handler in Versions)
                {
                    _ = (EndPointMethod?.MakeGenericMethod(Handler.ViewModelType).Invoke(this, [endpoints, CommandEndpoint + Versions.Key + "/", Handler, configuration.GetSystemConfig(), SystemConfig]));
                }
            }
            return endpoints;
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
            Services = services?.AddSingleton<ICommandService, CommandService>()
                        .AddSingleton<IEventService, EventService>();
            _ = (services?.AddAllTransient<IEventHandler>()
                .AddAllTransient<IEvent>()
                .AddAllTransient<ICommand>()
                .AddAllTransient<ICommandHandler>()
                ?.Configure<JsonOptions>(o => o.SerializerOptions.Converters.Add(new JsonStringEnumConverter())));
            return Services;
        }
    }
}