using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Mithril.Communication.Abstractions;
using Mithril.Communication.Abstractions.BaseClasses;
using Mithril.Communication.Abstractions.Interfaces;
using Mithril.Communication.Email.Models;
using Mithril.Communication.Email.Utils;
using Mithril.Data.Abstractions.Services;
using Mithril.Mvc.Abstractions.Services;
using System.Text;

namespace Mithril.Communication.Email.Channel
{
    /// <summary>
    /// Email channel for communication
    /// </summary>
    /// <seealso cref="ChannelBaseClass&lt;EmailChannel, EmailMessage&gt;"/>
    /// <remarks>
    /// Initializes a new instance of the <see cref="EmailChannel"/> class.
    /// </remarks>
    /// <param name="logger">The logger.</param>
    /// <param name="featureManager">The feature manager.</param>
    /// <param name="dataService">The data service.</param>
    /// <param name="viewRendererService">The view renderer service.</param>
    public class EmailChannel(ILogger<EmailChannel>? logger, IFeatureManager? featureManager, IDataService? dataService, IViewRendererService? viewRendererService) : ChannelBaseClass<EmailChannel, EmailMessage>(logger, featureManager)
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public override string Name { get; } = "Email";

        /// <summary>
        /// Gets the data services.
        /// </summary>
        /// <value>The data services.</value>
        private IDataService? DataService { get; } = dataService;

        /// <summary>
        /// Gets the view renderer service.
        /// </summary>
        /// <value>The view renderer service.</value>
        private IViewRendererService? ViewRendererService { get; } = viewRendererService;

        /// <summary>
        /// Sends the message asynchronously.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The message result</returns>
        public override async Task<MessageResult> SendMessageAsync(IMessage? message)
        {
            if (message is null || !CanHandle(message))
                return new MessageResult("Message is empty", new ArgumentNullException(nameof(message)));
            Logger?.LogInformation("Sending email");
            var Sender = new EmailSender(FeatureManager, DataService);
            var Body = !string.IsNullOrEmpty(message.Body)
                ? message.Body
                : await GetBodyFromTemplate(message, ViewRendererService).ConfigureAwait(false);
            Sender.To = message.To ?? "";
            Sender.Subject = message.Subject ?? "";
            Sender.Body = Body;
            if (message.Attachments.Count > 0)
                Sender.Attachments.AddRange(message.Attachments.Where(x => x is not null).Select(x => new SimpleMail.Attachment(x!.FileName, x.MimeType, x.Content)));
            Sender.Bcc = message.BCC;
            Sender.Cc = message.CC;
            if (!string.IsNullOrEmpty(message.From))
                Sender.From = message.From;
            await Sender.SendAsync().ConfigureAwait(false);
            Logger?.LogInformation("Email successfully sent");
            return new MessageResult("Sent");
        }

        /// <summary>
        /// Gets the body from the template specified in the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="viewRendererService">The view renderer service.</param>
        /// <returns>The body if a template is specified.</returns>
        private static async Task<string> GetBodyFromTemplate(IMessage message, IViewRendererService? viewRendererService)
        {
            return string.IsNullOrEmpty(message.Template) || viewRendererService is null
                ? ""
                : Encoding.UTF8.GetString(await viewRendererService.RenderAsync($"MessageTemplates/{message.Template}", message.TemplateFields).ConfigureAwait(false));
        }
    }
}