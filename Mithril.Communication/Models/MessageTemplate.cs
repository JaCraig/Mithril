using Microsoft.Extensions.Hosting;
using Mithril.Communication.Abstractions.Interfaces;
using Mithril.Data.Abstractions.BaseClasses;
using Mithril.Data.Abstractions.Services;
using System.ComponentModel.DataAnnotations;

namespace Mithril.Communication.Models
{
    /// <summary>
    /// Message template
    /// </summary>
    /// <seealso cref="ModelBase&lt;MessageTemplate&gt;"/>
    public class MessageTemplate : ModelBase<MessageTemplate>, IMessageTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageTemplate"/> class.
        /// </summary>
        public MessageTemplate()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageTemplate"/> class.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <exception cref="System.ArgumentNullException">displayName</exception>
        /// <exception cref="ArgumentException">displayName</exception>
        public MessageTemplate(string displayName)
        {
            if (string.IsNullOrEmpty(displayName))
                throw new ArgumentNullException(nameof(displayName));
            if (displayName.Length > 128)
                throw new ArgumentException(nameof(displayName) + " is too long. 128 characters max allowed.");

            DisplayName = displayName;
        }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        [Required]
        [MinLength(1)]
        [MaxLength(128)]
        public string? DisplayName { get; set; }

        /// <summary>
        /// Loads the message template based on the name specified.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        /// <param name="displayName">The display name.</param>
        /// <returns>The message template.</returns>
        public static MessageTemplate? Load(IDataService? dataService, string displayName)
        {
            return Query(dataService)?.Where(x => x.DisplayName == displayName).FirstOrDefault();
        }

        /// <summary>
        /// Loads a specific message template or creates it.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        /// <param name="displayName">The display name.</param>
        /// <returns>The message template specified.</returns>
        public static async Task<IMessageTemplate> LoadOrCreateAsync(IDataService? dataService, string displayName)
        {
            var ReturnValue = Load(dataService, displayName);
            if (ReturnValue is null)
            {
                ReturnValue = new MessageTemplate(displayName);
                if (dataService is not null)
                    await dataService.SaveAsync(ReturnValue).ConfigureAwait(false);
            }
            return ReturnValue;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(MessageTemplate left, MessageTemplate right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(MessageTemplate left, MessageTemplate right)
        {
            return left is null ? right is null : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(MessageTemplate left, MessageTemplate right)
        {
            return left is null ? right is null : left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(MessageTemplate first, MessageTemplate second)
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
        public static bool operator >(MessageTemplate left, MessageTemplate right)
        {
            return left is null ? right is null : left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(MessageTemplate left, MessageTemplate right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Compares the object to another object
        /// </summary>
        /// <param name="other">Object to compare to</param>
        /// <returns>0 if they are equal, -1 if this is smaller, 1 if it is larger</returns>
        public override int CompareTo(MessageTemplate? other)
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
        public bool Equals(MessageTemplate other)
        {
            return base.Equals(other);
        }

        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <param name="hostingEnvironment">The hosting environment.</param>
        /// <returns>The content</returns>
        public string GetContent(IHostEnvironment? hostingEnvironment)
        {
            if (hostingEnvironment is null)
                return "";
            return string.IsNullOrEmpty(DisplayName)
                ? ""
                : new FileCurator.FileInfo($"{hostingEnvironment.ContentRootPath}/Views/MessageTemplates/{FixDisplayName()}.cshtml").Read();
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
        /// Sets the content.
        /// </summary>
        /// <param name="hostingEnvironment">The hosting environment.</param>
        /// <param name="content">The content.</param>
        public IMessageTemplate SetContent(IHostEnvironment? hostingEnvironment, string content)
        {
            if (string.IsNullOrEmpty(DisplayName) || hostingEnvironment is null)
                return this;
            new FileCurator.FileInfo($"{hostingEnvironment.ContentRootPath}/Views/MessageTemplates/{FixDisplayName()}.cshtml").Write(content);
            return this;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            return DisplayName ?? "";
        }

        /// <summary>
        /// Fixes the display name.
        /// </summary>
        /// <returns>The fixed display name path.</returns>
        private string? FixDisplayName()
        {
            return DisplayName?.Replace(".", "_", StringComparison.OrdinalIgnoreCase)
                               .Replace("/", "_", StringComparison.OrdinalIgnoreCase)
                               .Replace("\\", "_", StringComparison.OrdinalIgnoreCase);
        }
    }
}