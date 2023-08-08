using Mithril.Core.Abstractions.BaseClasses;

namespace Mithril.Communication.Abstractions
{
    /// <summary>
    /// Message result
    /// </summary>
    /// <param name="Message">Message</param>
    /// <param name="Exception">Exception</param>
    /// <seealso cref="GenericResult"/>
    /// <seealso cref="IEquatable&lt;GenericResult&gt;"/>
    /// <seealso cref="IEquatable&lt;MessageResult&gt;"/>
    public record MessageResult(string Message, Exception? Exception = null)
        : GenericResult(Message, Exception);
}