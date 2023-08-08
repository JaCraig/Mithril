using FileCurator;
using FileCurator.BaseClasses;

namespace Mithril.FileSystem.Abstractions.BaseClasses
{
    /// <summary>
    /// Media file base class
    /// </summary>
    /// <typeparam name="TInternalFileType">The type of the internal file type.</typeparam>
    /// <typeparam name="TFileType">The type of the file type.</typeparam>
    /// <seealso cref="FileBase{TInternalFileType, TFileType}"/>
    public abstract class MediaFileBaseClass<TInternalFileType, TFileType> : FileBase<TInternalFileType, TFileType>
        where TFileType : MediaFileBaseClass<TInternalFileType, TFileType>, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaFileBaseClass{TInternalFileType,
        /// TFileType}"/> class.
        /// </summary>
        protected MediaFileBaseClass()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaFileBaseClass{TInternalFileType,
        /// TFileType}"/> class.
        /// </summary>
        /// <param name="internalFile">Internal file</param>
        protected MediaFileBaseClass(TInternalFileType internalFile)
            : base(internalFile)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaFileBaseClass{TInternalFileType,
        /// TFileType}"/> class.
        /// </summary>
        /// <param name="internalFileType">Type of the internal file.</param>
        /// <param name="credentials">The credentials.</param>
        protected MediaFileBaseClass(TInternalFileType internalFileType, Credentials? credentials = null)
            : base(internalFileType, credentials)
        {
        }
    }
}