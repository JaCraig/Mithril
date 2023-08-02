using FileCurator.Interfaces;
using Mithril.FileSystem.Abstractions.BaseClasses;
using Mithril.Tests.Helpers;
using System.Text;

namespace Mithril.FileSystem.Abstractions.Tests.BaseClasses
{
    /// <summary>
    /// Media directory base class tests
    /// </summary>
    /// <seealso cref="TestBaseClass&lt;MediaTestDirectory&gt;"/>
    public class MediaFileBaseClassTests : TestBaseClass<MediaTestFile>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaFileBaseClassTests"/> class.
        /// </summary>
        public MediaFileBaseClassTests()
        {
            TestObject = new MediaTestFile();
            ObjectType = typeof(MediaTestFile);
        }
    }

    /// <summary>
    /// Test directory
    /// </summary>
    /// <seealso cref="MediaFileBaseClass&lt;string, MediaTestDirectory&gt;"/>
    public class MediaTestFile : MediaFileBaseClass<string, MediaTestFile>
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
        /// Directory the file is within
        /// </summary>
        public override IDirectory? Directory => null;

        /// <summary>
        /// Does it exist?
        /// </summary>
        public override bool Exists { get; } = true;

        /// <summary>
        /// File extension
        /// </summary>
        public override string Extension => "txt";

        /// <summary>
        /// Full path
        /// </summary>
        public override string FullName { get; } = "Test";

        /// <summary>
        /// Size of the file
        /// </summary>
        public override long Length => 0;

        /// <summary>
        /// Date modified (UTC time)
        /// </summary>
        public override DateTime Modified { get; } = DateTime.Now;

        /// <summary>
        /// Name
        /// </summary>
        public override string Name { get; } = "Test";

        /// <summary>
        /// Copies the file to another directory
        /// </summary>
        /// <param name="directory">Directory to copy the file to</param>
        /// <param name="overwrite">Should the file overwrite another file if found</param>
        /// <returns>The newly created file</returns>
        public override IFile CopyTo(IDirectory directory, bool overwrite) => this;

        /// <summary>
        /// Deletes the file
        /// </summary>
        /// <returns>Any response for deleting the resource (usually FTP, HTTP, etc)</returns>
        public override string Delete() => "";

        /// <summary>
        /// Moves the file to a new directory
        /// </summary>
        /// <param name="directory">Directory to move to</param>
        /// <returns></returns>
        public override IFile MoveTo(IDirectory directory) => this;

        /// <summary>
        /// Reads the file in as a string
        /// </summary>
        /// <returns>The file contents as a string</returns>
        public override string Read() => "";

        /// <summary>
        /// Reads a file as binary
        /// </summary>
        /// <returns>The file contents as a byte array</returns>
        public override byte[] ReadBinary() => Array.Empty<byte>();

        /// <summary>
        /// Renames the file
        /// </summary>
        /// <param name="newName">New name for the file</param>
        /// <returns></returns>
        public override IFile Rename(string newName) => this;

        /// <summary>
        /// Writes content to the file
        /// </summary>
        /// <param name="content">Content to write</param>
        /// <param name="mode">Mode to open the file as</param>
        /// <param name="encoding">Encoding to use for the content</param>
        /// <returns>The result of the write or original content</returns>
        public override string Write(string content, FileMode mode = FileMode.Create, Encoding? encoding = null) => "";

        /// <summary>
        /// Writes content to the file
        /// </summary>
        /// <param name="content">Content to write</param>
        /// <param name="mode">Mode to open the file as</param>
        /// <returns>The result of the write or original content</returns>
        public override byte[] Write(byte[] content, FileMode mode = FileMode.Create) => Array.Empty<byte>();
    }
}