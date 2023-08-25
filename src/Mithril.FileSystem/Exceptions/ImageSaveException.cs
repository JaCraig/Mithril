﻿namespace Mithril.FileSystem.Exceptions
{
    /// <summary>
    /// Image save exception
    /// </summary>
    public class ImageSaveException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageSaveException"/> class.
        /// </summary>
        public ImageSaveException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageSaveException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference ( <see
        /// langword="Nothing"/> in Visual Basic) if no inner exception is specified.
        /// </param>
        public ImageSaveException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageSaveException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ImageSaveException(string message) : base(message)
        {
        }
    }
}