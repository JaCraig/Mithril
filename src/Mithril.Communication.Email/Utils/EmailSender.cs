using BigBook;
using Microsoft.FeatureManagement;
using Mithril.Communication.Email.Features;
using Mithril.Communication.Email.Models;
using Mithril.Core.Abstractions.Extensions;
using Mithril.Data.Abstractions.Services;

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
        /// <param name="featureManager">The feature manager.</param>
        /// <param name="dataService">The data service.</param>
        public EmailSender(IFeatureManager? featureManager, IDataService? dataService)
        {
            CanSend = featureManager.AreFeaturesEnabled(EmailFeature.Instance);
            EmailSettings? Settings = AsyncHelper.RunSync(() => EmailSettings.LoadOrCreateAsync(dataService, null));
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
            CheckValues();
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
            CheckValues();
            await base.SendAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Checks the values.
        /// </summary>
        private void CheckValues()
        {
            Subject ??= "";
            Body ??= "";
        }
    }
}