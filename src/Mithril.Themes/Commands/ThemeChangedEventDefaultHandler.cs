using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Mithril.API.Abstractions.Commands;
using Mithril.API.Abstractions.Commands.BaseClasses;
using Mithril.API.Abstractions.Commands.Enums;
using Mithril.Themes.Models;

namespace Mithril.Themes.Commands
{
    /// <summary>
    /// Theme changed event default handler
    /// </summary>
    public class ThemeChangedEventDefaultHandler : EventHandlerBaseClass<ThemeChangedEventDefaultHandler, ThemeChangedEvent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeChangedEventDefaultHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="featureManager">The feature manager.</param>
        public ThemeChangedEventDefaultHandler(ILogger<ThemeChangedEventDefaultHandler>? logger, IFeatureManager? featureManager) : base(logger, featureManager)
        {
        }

        /// <summary>
        /// Handles the specified argument.
        /// </summary>
        /// <param name="arg">The argument.</param>
        /// <returns>The result from processing the event.</returns>
        protected override EventResult Handle(ThemeChangedEvent arg) => new(arg, EventStateTypes.Completed, this);
    }
}