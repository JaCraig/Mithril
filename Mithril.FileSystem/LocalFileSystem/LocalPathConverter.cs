using FileCurator.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Mithril.Core.Abstractions.Mvc.Context;
using Mithril.FileSystem.Abstractions.Interfaces;
using System.Text.RegularExpressions;

namespace Mithril.FileSystem.LocalFileSystem
{
    /// <summary>
    /// Local path converter
    /// </summary>
    /// <seealso cref="IPathConverter"/>
    public class LocalPathConverter : IPathConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalPathConverter"/> class.
        /// </summary>
        /// <param name="webHostEnvironment">The web host environment.</param>
        public LocalPathConverter(IWebHostEnvironment? webHostEnvironment = null)
        {
            ContentRootPath = webHostEnvironment?.ContentRootPath ?? "./";
        }

        /// <summary>
        /// Gets the content root path.
        /// </summary>
        /// <value>The content root path.</value>
        public string ContentRootPath { get; }

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        public int Order { get; } = int.MaxValue;

        /// <summary>
        /// Gets the can handle.
        /// </summary>
        /// <value>The can handle.</value>
        private static Regex CanHandle { get; } = new Regex(@"^\w:", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// Determines whether this instance can convert the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>
        /// <c>true</c> if this instance can convert the specified path; otherwise, <c>false</c>.
        /// </returns>
        public bool CanConvert(string path)
        {
            return (path?.StartsWith("mithril://", StringComparison.OrdinalIgnoreCase) ?? false) || CanHandle.IsMatch(path ?? "");
        }

        /// <summary>
        /// Determines whether this instance can convert the specified path.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>
        /// <c>true</c> if this instance can convert the specified path; otherwise, <c>false</c>.
        /// </returns>
        public bool CanConvert(IFile file)
        {
            return CanConvert(file?.FullName ?? "");
        }

        /// <summary>
        /// Converts the file in the path to a Uri.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The Uri corresponding to the path.</returns>
        public Uri? GetUrl(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return null;
            var Request = HttpContext.Current?.Request;
            var Host = Request?.Host.ToUriComponent() ?? "";
            var PathBase = Request?.PathBase.ToUriComponent() ?? "";
            var RootDirectory = new FileCurator.DirectoryInfo("mithril://");
            var FilePath = new FileCurator.FileInfo(path).FullName.Replace(RootDirectory.FullName, "", StringComparison.OrdinalIgnoreCase).Replace("\\", "/", StringComparison.Ordinal);
            Uri.TryCreate($"{Request?.Scheme}://{Host}{PathBase}/{FilePath}", new UriCreationOptions { DangerousDisablePathAndQueryCanonicalization = true }, out var ReturnValue);
            return ReturnValue;
        }

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        public Uri? GetUrl(IFile file)
        {
            return GetUrl(file?.FullName ?? "");
        }
    }
}