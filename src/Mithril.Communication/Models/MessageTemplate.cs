using Microsoft.Extensions.Hosting;
using Mithril.Communication.Abstractions.Interfaces;
using Mithril.Data.Abstractions.BaseClasses;
using Mithril.Data.Abstractions.Services;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

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
        /// <param name="displayName">The display name.</param>
        /// <param name="dataService">The data service.</param>
        /// <returns>The message template.</returns>
        public static MessageTemplate? Load(string displayName, IDataService? dataService) => Query(dataService)?.Where(x => x.DisplayName == displayName).FirstOrDefault();

        /// <summary>
        /// Loads a specific message template or creates it.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="dataService">The data service.</param>
        /// <param name="user">The user.</param>
        /// <returns>The message template specified.</returns>
        public static async Task<IMessageTemplate> LoadOrCreateAsync(string displayName, IDataService? dataService, ClaimsPrincipal? user)
        {
            MessageTemplate? ReturnValue = Load(displayName, dataService);
            if (ReturnValue is null)
            {
                ReturnValue = new MessageTemplate(displayName);
                if (dataService is not null)
                    _ = await dataService.SaveAsync(user, ReturnValue).ConfigureAwait(false);
            }
            return ReturnValue;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(MessageTemplate left, MessageTemplate right) => !(left == right);

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(MessageTemplate left, MessageTemplate right) => left is null ? right is null : left.CompareTo(right) < 0;

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(MessageTemplate left, MessageTemplate right) => left is null ? right is null : left.CompareTo(right) <= 0;

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(MessageTemplate first, MessageTemplate second) => ReferenceEquals(first, second) || (first is not null && second is not null && first.CompareTo(second) == 0);

        /// <summary>
        /// Implements the operator &gt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(MessageTemplate left, MessageTemplate right) => left is null ? right is null : left.CompareTo(right) > 0;

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(MessageTemplate left, MessageTemplate right) => left is null ? right is null : left.CompareTo(right) >= 0;

        /// <summary>
        /// Compares the object to another object
        /// </summary>
        /// <param name="other">Object to compare to</param>
        /// <returns>0 if they are equal, -1 if this is smaller, 1 if it is larger</returns>
        public override int CompareTo(MessageTemplate? other) => base.CompareTo(other);

        /// <summary>
        /// Determines whether the specified <see cref="object"/>, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj) => base.Equals(obj);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref>
        /// parameter; otherwise, false.
        /// </returns>
        public bool Equals(MessageTemplate other) => base.Equals(other);

        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <param name="hostingEnvironment">The hosting environment.</param>
        /// <returns>The content</returns>
        public string GetContent(IHostEnvironment? hostingEnvironment)
        {
            return hostingEnvironment is null
                ? ""
                : string.IsNullOrEmpty(DisplayName)
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
        public override int GetHashCode() => base.GetHashCode();

        /// <summary>
        /// Sets the content.
        /// </summary>
        /// <param name="hostingEnvironment">The hosting environment.</param>
        /// <param name="content">The content.</param>
        public IMessageTemplate SetContent(IHostEnvironment? hostingEnvironment, string? content)
        {
            if (string.IsNullOrEmpty(DisplayName) || hostingEnvironment is null)
                return this;
            _ = new FileCurator.FileInfo($"{hostingEnvironment.ContentRootPath}/Views/MessageTemplates/{FixDisplayName()}.cshtml").Write(content ?? "");
            return this;
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents this instance.</returns>
        public override string ToString() => DisplayName ?? "";

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