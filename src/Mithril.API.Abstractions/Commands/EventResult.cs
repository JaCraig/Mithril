using Mithril.API.Abstractions.Commands.Enums;
using Mithril.API.Abstractions.Commands.Interfaces;
using Mithril.Core.Abstractions.BaseClasses;

namespace Mithril.API.Abstractions.Commands
{
    /// <summary>
    /// Event result
    /// </summary>
    /// <seealso cref="IEquatable&lt;EventResult&gt;"/>
    public record EventResult(IEvent? Event, EventStateTypes NewState, IEventHandler EventHandler, Exception? Exception = null)
        : GenericResult(Exception is null ? $"{Event} moved to new state {NewState} by {EventHandler.Name}" : "Exception occurred", Exception)
    {
        /// <summary>
        /// Gets a value indicating whether this instance is error state.
        /// </summary>
        /// <value><c>true</c> if this instance is error state; otherwise, <c>false</c>.</value>
        public override bool IsErrorState => NewState == EventStateTypes.Error || NewState == EventStateTypes.Retrying;
    }
}