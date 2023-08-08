using FileCurator;
using FileCurator.BaseClasses;

namespace Mithril.FileSystem.Abstractions.BaseClasses
{
    /// <summary>
    /// Media directory base class
    /// </summary>
    /// <typeparam name="TInternalDirectoryType">The type of the internal directory type.</typeparam>
    /// <typeparam name="TDirectoryType">The type of the directory type.</typeparam>
    /// <seealso cref="DirectoryBase{TInternalDirectoryType, TDirectoryType}"/>
    public abstract class MediaDirectoryBaseClass<TInternalDirectoryType, TDirectoryType> : DirectoryBase<TInternalDirectoryType, TDirectoryType>
        where TDirectoryType : MediaDirectoryBaseClass<TInternalDirectoryType, TDirectoryType>, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="MediaDirectoryBaseClass{TInternalDirectoryType, TDirectoryType}"/> class.
        /// </summary>
        protected MediaDirectoryBaseClass()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="MediaDirectoryBaseClass{TInternalDirectoryType, TDirectoryType}"/> class.
        /// </summary>
        /// <param name="internalDirectory">Internal directory object</param>
        /// <param name="credentials">The credentials.</param>
        protected MediaDirectoryBaseClass(TInternalDirectoryType internalDirectory, Credentials? credentials = null)
            : base(internalDirectory, credentials)
        {
        }
    }
}