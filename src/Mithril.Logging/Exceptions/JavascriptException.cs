﻿namespace Mithril.Logging.Exceptions
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
    }
}