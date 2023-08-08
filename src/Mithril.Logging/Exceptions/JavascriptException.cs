using System.Runtime.Serialization;

namespace Mithril.Logging.Exceptions
{
    /// <summary>
    /// Javascript exception
    /// </summary>
    /// <seealso cref="Exception"/>
    public class JavascriptException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JavascriptException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public JavascriptException(string message)
            : base(message ?? "Javascript exception")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JavascriptException"/> class.
        /// </summary>
        public JavascriptException()
            : this("Javascript exception")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JavascriptException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference ( <see
        /// langword="Nothing"/> in Visual Basic) if no inner exception is specified.
        /// </param>
        public JavascriptException(string message, Exception innerException) : base(message ?? "Javascript exception", innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JavascriptException"/> class.
        /// </summary>
        /// <param name="info">
        /// The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the
        /// serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains
        /// contextual information about the source or destination.
        /// </param>
        protected JavascriptException(SerializationInfo info, StreamingContext context)
            : base(info ?? new SerializationInfo(typeof(JavascriptException), new FormatterConverter()), context)
        {
        }
    }
}