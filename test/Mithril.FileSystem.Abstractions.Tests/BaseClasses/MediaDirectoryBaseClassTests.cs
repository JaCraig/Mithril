using FileCurator.Interfaces;
using Mithril.FileSystem.Abstractions.BaseClasses;
using Mithril.Tests.Helpers;

namespace Mithril.FileSystem.Abstractions.Tests.BaseClasses
{
    /// <summary>
    /// Media directory base class tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;MediaTestDirectory&gt;"/>
    public class MediaDirectoryBaseClassTests : TestBaseClass<MediaTestDirectory>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaDirectoryBaseClassTests"/> class.
        /// </summary>
        public MediaDirectoryBaseClassTests()
        {
            TestObject = new MediaTestDirectory();
            ObjectType = typeof(MediaTestDirectory);
        }
    }

    /// <summary>
    /// Test directory
    /// </summary>
    /// <seealso cref="MediaDirectoryBaseClass&lt;string, MediaTestDirectory&gt;"/>
    public class MediaTestDirectory : MediaDirectoryBaseClass<string, MediaTestDirectory>
    {
        /// <summary>
        /// Last time accessed (UTC time)
        /// </summary>
        public override DateTime Accessed { get; } = DateTime.Now;

        /// <summary>
        /// Date created (UTC time)
        /// </summary>
        public override DateTime Created { get; } = DateTime.Now;

        /// <summary>
        /// Does it exist?
        /// </summary>
        public override bool Exists { get; } = true;

        /// <summary>
        /// Full path
        /// </summary>
        public override string FullName { get; } = "Test";

        /// <summary>
        /// Date modified (UTC time)
        /// </summary>
        public override DateTime Modified { get; } = DateTime.Now;

        /// <summary>
        /// Name
        /// </summary>
        public override string Name { get; } = "Test";

        /// <summary>
        /// Parent directory
        /// </summary>
        public override IDirectory? Parent { get; } = null;

        /// <summary>
        /// Root directory
        /// </summary>
        public override IDirectory? Root { get; } = null;

        /// <summary>
        /// Size of the directory
        /// </summary>
        public override long Size { get; } = 0;

        /// <summary>
        /// Creates the directory
        /// </summary>
        /// <returns></returns>
        public override IDirectory Create() => this;

        /// <summary>
        /// Deletes the directory
        /// </summary>
        /// <returns></returns>
        public override IDirectory Delete() => this;

        /// <summary>
        /// Enumerates directories under this directory
        /// </summary>
        /// <param name="searchPattern">Search pattern</param>
        /// <param name="options">Search options</param>
        /// <returns>List of directories under this directory</returns>
        public override IEnumerable<IDirectory> EnumerateDirectories(string searchPattern = "*", SearchOption options = SearchOption.TopDirectoryOnly) => Array.Empty<IDirectory>();

        /// <summary>
        /// Enumerates files under this directory
        /// </summary>
        /// <param name="searchPattern">Search pattern</param>
        /// <param name="options">Search options</param>
        /// <returns>List of files under this directory</returns>
        public override IEnumerable<IFile> EnumerateFiles(string searchPattern = "*", SearchOption options = SearchOption.TopDirectoryOnly) => Array.Empty<IFile>();

        /// <summary>
        /// Renames the directory
        /// </summary>
        /// <param name="name">Name of the new directory</param>
        /// <returns></returns>
        public override IDirectory Rename(string name) => this;
    }
}