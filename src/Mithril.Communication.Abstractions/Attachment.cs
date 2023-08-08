using Mithril.Data.Abstractions.BaseClasses;
using System.ComponentModel.DataAnnotations;

namespace Mithril.Communication.Abstractions
{
    /// <summary>
    /// Message file attachments
    /// </summary>
    /// <seealso cref="ModelBase&lt;Attachment&gt;"/>
    public class Attachment : ModelBase<Attachment>
    {
        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <value>The content.</value>
        public byte[] Content
        {
            get
            {
                var TempFile = new FileCurator.FileInfo($"Mithril://{Location}");
                return TempFile.ReadBinary();
            }
        }

        /// <summary>
        /// Gets or sets the file hash.
        /// </summary>
        /// <value>The file hash.</value>
        [MaxLength(512)]
        [Required]
        public string? FileHash { get; set; }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        [MaxLength(128)]
        public string? FileName { get; set; }

        /// <summary>
        /// Gets or sets the location on disk.
        /// </summary>
        /// <value>The location on disk.</value>
        [MaxLength(1028)]
        public string? Location { get; set; }

        /// <summary>
        /// Gets or sets the type of the MIME.
        /// </summary>
        /// <value>The type of the MIME.</value>
        [MaxLength(64)]
        public string? MimeType { get; set; }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>The size.</value>
        public long Size { get; set; }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(Attachment left, Attachment right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(Attachment left, Attachment right)
        {
            return left is null ? right is null : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(Attachment left, Attachment right)
        {
            return left is null ? right is null : left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(Attachment first, Attachment second)
        {
            if (ReferenceEquals(first, second))
                return true;

            if (first is null || second is null)
                return false;

            return first.CompareTo(second) == 0;
        }

        /// <summary>
        /// Implements the operator &gt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(Attachment left, Attachment right)
        {
            return left is null ? right is null : left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(Attachment left, Attachment right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Compares the object to another object
        /// </summary>
        /// <param name="other">Object to compare to</param>
        /// <returns>0 if they are equal, -1 if this is smaller, 1 if it is larger</returns>
        public override int CompareTo(Attachment? other)
        {
            return base.CompareTo(other);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/>, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref>
        /// parameter; otherwise, false.
        /// </returns>
        public bool Equals(Attachment other)
        {
            return base.Equals(other);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data
        /// structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            return FileName ?? "";
        }
    }
}