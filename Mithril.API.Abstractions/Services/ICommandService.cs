using Mithril.API.Abstractions.Commands.Interfaces;
using System.Dynamic;

namespace Mithril.API.Abstractions.Services
{
    /// <summary>
    /// Command service interface
    /// </summary>
    public interface ICommandService
    {
        /// <summary>
        /// Converts the specified value to a command.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <returns>The command based on the type.</returns>
        ICommand? Convert(string type, ExpandoObject value);

        /// <summary>
        /// Runs this instance.
        /// </summary>
        Task RunAsync();
    }
}