using Microsoft.AspNetCore.Http;
using Mithril.API.Abstractions.Commands.Interfaces;

namespace Mithril.API.Abstractions.Commands
{
    /// <summary>
    /// Command creation result.
    /// </summary>
    /// <seealso cref="IEquatable&lt;CommandCreationResult&gt;"/>
    public record CommandCreationResult(ICommand? Command, Exception? Exception = null, string? ResultText = "Success", int ReturnCode = StatusCodes.Status200OK);
}