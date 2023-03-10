using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mithril.Core.Abstractions.Modules.BaseClasses;
using Mithril.Themes.Abstractions.Interfaces;
using Mithril.Themes.Abstractions.Services;
using Mithril.Themes.LocationExpanders;
using Mithril.Themes.LocationExpanders.Interfaces;
using Mithril.Themes.Services;

namespace Mithril.Themes
{
    public class ThemesModule : ModuleBaseClass<ThemesModule>
    {
        public override IServiceCollection? ConfigureServices(IServiceCollection? services, IConfiguration? configuration, IHostEnvironment? environment)
        {
            //View location expander providers.
            return services?.AddScoped<IViewLocationExpanderProvider, DefaultViewLocationExpanderProvider>()
                            .AddScoped<IViewLocationExpanderProvider, ModuleViewLocationExpanderProvider>()
                            //Add theme service and themes
                            .AddAllSingleton<ITheme>()
                           ?.AddScoped<IThemeService, ThemeService>()

                           //Add the view location expanders to the razor engine.
                           ?.Configure<RazorViewEngineOptions>(options => options.ViewLocationExpanders.Add(new CompositeViewLocationExpanderProvider()));
        }
    }
}