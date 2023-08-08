using Microsoft.AspNetCore.Http;
using Mithril.API.Abstractions.Commands.Interfaces;
using Mithril.Core.Abstractions.BaseClasses;

namespace Mithril.API.Abstractions.Commands
{
    /// <summary>
    /// Command creation result.
    /// </summary>
    /// <seealso cref="IEquatable&lt;CommandCreationResult&gt;"/>
    public record CommandCreationResult(ICommand? Command, Exception? Exception = null, string? ResultText = "Success", int ReturnCode = StatusCodes.Status200OK)
        : GenericResult(Exception is null ? $"{Command} handled with result: {ResultText}" : "Exception occurred", Exception);
}