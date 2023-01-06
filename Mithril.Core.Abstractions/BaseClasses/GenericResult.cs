namespace Mithril.Core.Abstractions.BaseClasses
{
    /// <summary>
    /// Generic result
    /// </summary>
    /// <param name="Message">Message</param>
    /// <param name="Exception">Exception</param>
    /// <seealso cref="IEquatable&lt;GenericResult&gt;"/>
    public abstract record GenericResult(string Message, Exception? Exception = null)
    {
        /// <summary>
        /// Gets a value indicating whether this instance is error state.
        /// </summary>
        /// <value><c>true</c> if this instance is error state; otherwise, <c>false</c>.</value>
        public virtual bool IsErrorState => Exception is not null;
    }
}