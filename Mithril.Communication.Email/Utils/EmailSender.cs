using Microsoft.FeatureManagement;
using Mithril.Communication.Email.Models;
using Mithril.Core.Abstractions.Modules.Features;

namespace Mithril.Communication.Email.Utils
{
    /// <summary>
    /// Email sender
    /// </summary>
    /// <seealso cref="SimpleMail.Email"/>
    public class EmailSender : SimpleMail.Email
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="featureService">The feature service.</param>
        public Email(IFeatureManager? featureManager)
        {
            CanSend = featureManager.?.Load<EmailFeature>()?.Active == true;
            var Settings = EmailSettings.Load();
            if (Settings is null)
                return;
            From = Settings.SystemAddress;
            Server = Settings.Server;
            LocalDomain = Settings.LocalDomain;
            Password = Settings.Password;
            Port = Settings.Port;
            UserName = Settings.UserName;
            UseSSL = Settings.UseSSL;
            Priority = MimeKit.MessagePriority.Normal;
        }

        /// <summary>
        /// Gets a value indicating whether this instance can send.
        /// </summary>
        /// <value><c>true</c> if this instance can send; otherwise, <c>false</c>.</value>
        public bool CanSend { get; }

        /// <summary>
        /// Logs and sends the email.
        /// </summary>
        public new void Send()
        {
            if (string.IsNullOrEmpty(Server) || !CanSend)
                return;
            new EmailRecord
            {
                Body = Body,
                From = From,
                Subject = Subject,
                To = To,
                Application = System.Reflection.Assembly.GetEntryAssembly()?.GetName().Name ?? string.Empty
            }.Save();
            base.Send();
        }

        /// <summary>
        /// Logs and sends the email.
        /// </summary>
        /// <returns>The resulting Task</returns>
        public new async Task SendAsync()
        {
            if (string.IsNullOrEmpty(Server) || !CanSend)
                return;
            await new EmailRecord
            {
                Body = Body,
                From = From,
                Subject = Subject,
                To = To,
                Application = System.Reflection.Assembly.GetEntryAssembly()?.GetName().Name ?? string.Empty
            }.SaveAsync().ConfigureAwait(false);
            await base.SendAsync().ConfigureAwait(false);
        }
    }
}