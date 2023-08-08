using FileCurator.Interfaces;

namespace Mithril.FileSystem.Abstractions.Interfaces
{
    /// <summary>
    /// Path converter
    /// </summary>
    public interface IPathConverter
    {
        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        int Order { get; }

        /// <summary>
        /// Determines whether this instance can convert the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>
        /// <c>true</c> if this instance can convert the specified path; otherwise, <c>false</c>.
        /// </returns>
        bool CanConvert(string path);

        /// <summary>
        /// Determines whether this instance can convert the specified path.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>
        /// <c>true</c> if this instance can convert the specified path; otherwise, <c>false</c>.
        /// </returns>
        bool CanConvert(IFile file);

        /// <summary>
        /// Converts the file in the path to a Uri.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The Uri corresponding to the path.</returns>
        Uri? GetUrl(string path);

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        Uri? GetUrl(IFile file);
    }
}