using Mithril.Communication.Abstractions;
using Mithril.Communication.Abstractions.Interfaces;
using Mithril.Data.Abstractions.BaseClasses;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Text.Json;

namespace Mithril.Communication.Email.Models
{
    /// <summary>
    /// Email message
    /// </summary>
    /// <seealso cref="ModelBase&lt;EmailMessage&gt;"/>
    public class EmailMessage : ModelBase<EmailMessage>, IMessage
    {
        /// <summary>
        /// Gets or sets the application the message originated from.
        /// </summary>
        /// <value>The application the message originated from.</value>
        [MaxLength(64)]
        public string? Application { get; set; }

        /// <summary>
        /// Gets or sets the attachments.
        /// </summary>
        /// <value>The attachments.</value>
        public virtual IList<Attachment?> Attachments { get; set; } = new List<Attachment?>();

        /// <summary>
        /// Gets or sets the BCC.
        /// </summary>
        /// <value>The BCC.</value>
        [MaxLength]
        public string? BCC { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>The body.</value>
        [MaxLength]
        public string? Body { get; set; }

        /// <summary>
        /// Gets or sets the cc.
        /// </summary>
        /// <value>The cc.</value>
        [MaxLength]
        public string? CC { get; set; }

        /// <summary>
        /// Gets or sets from.
        /// </summary>
        /// <value>From.</value>
        [Required]
        [MinLength(1)]
        [MaxLength(128)]
        public string? From { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>The subject.</value>
        [Required]
        [MinLength(1)]
        [MaxLength(256)]
        public string? Subject { get; set; }

        /// <summary>
        /// Gets or sets the template.
        /// </summary>
        /// <value>The template.</value>
        [MaxLength(128)]
        public string? Template { get; set; }

        /// <summary>
        /// Gets or sets the template data (JSON).
        /// </summary>
        /// <value>The template data in JSON format.</value>
        [MaxLength]
        public string? TemplateData { get; set; }

        /// <summary>
        /// Gets or sets the template data.
        /// </summary>
        /// <value>The template data.</value>
        public ExpandoObject? TemplateFields
        {
            get
            {
                try
                {
                    return JsonSerializer.Deserialize<ExpandoObject>(TemplateData ?? "{}");
                }
                catch { return null; }
            }
        }

        /// <summary>
        /// Gets or sets to.
        /// </summary>
        /// <value>To.</value>
        [Required]
        [MinLength(1)]
        [MaxLength]
        public string? To { get; set; }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(EmailMessage left, EmailMessage right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(EmailMessage left, EmailMessage right)
        {
            return left is null ? right is null : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(EmailMessage left, EmailMessage right)
        {
            return left is null ? right is null : left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(EmailMessage first, EmailMessage second)
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
        public static bool operator >(EmailMessage left, EmailMessage right)
        {
            return left is null ? right is null : left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(EmailMessage left, EmailMessage right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Compares the object to another object
        /// </summary>
        /// <param name="other">Object to compare to</param>
        /// <returns>0 if they are equal, -1 if this is smaller, 1 if it is larger</returns>
        public override int CompareTo(EmailMessage? other)
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
        public bool Equals(EmailMessage other)
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
            return Subject ?? "";
        }
    }
}