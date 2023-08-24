﻿using BigBook;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;
using Mithril.API.Abstractions.Attributes;
using Mithril.API.Abstractions.Commands;
using Mithril.API.Abstractions.Commands.BaseClasses;
using Mithril.API.Abstractions.Commands.Interfaces;
using Mithril.Communication.Abstractions;
using Mithril.Communication.Abstractions.Commands;
using Mithril.Communication.Abstractions.Events;
using Mithril.Communication.Abstractions.Interfaces;
using Mithril.Communication.Abstractions.Services;
using Mithril.Communication.Commands.ViewModels;
using Mithril.Core.Abstractions.Configuration;
using Mithril.Data.Abstractions.Services;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Mithril.Communication.Commands
{
    /// <summary>
    /// Send message command handler
    /// </summary>
    /// <seealso cref="CommandHandlerBaseClass&lt;SendMessageCommand, SendMessageCommandVM&gt;"/>
    [ApiIgnore]
    public class SendMessageCommandHandler : CommandHandlerBaseClass<SendMessageCommand, SendMessageCommandVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendMessageCommandHandler"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="featureManager">The feature manager.</param>
        /// <param name="communicationService">The communication service.</param>
        /// <param name="dataService">The data service.</param>
        /// <param name="mithrilConfig">The mithril configuration.</param>
        /// <param name="channels">The channels.</param>
        public SendMessageCommandHandler(
            ILogger<SendMessageCommandHandler>? logger,
            IFeatureManager? featureManager,
            ICommunicationService? communicationService,
            IDataService? dataService,
            IOptions<MithrilConfig>? mithrilConfig,
            IEnumerable<IChannel> channels)
            : base(logger, featureManager)
        {
            DataService = dataService;
            Channels = channels;
            CommunicationService = communicationService;
            MithrilConfig = mithrilConfig?.Value;
            HashAlgorithm = SHA256.Create();
        }

        /// <summary>
        /// Gets the command type accepted.
        /// </summary>
        /// <value>The command type accepted.</value>
        public override string CommandName => "SendCommunication";

        /// <summary>
        /// Gets the tags (Used by OpenAPI, etc).
        /// </summary>
        /// <value>The tags (Used by OpenAPI, etc).</value>
        public override string[] Tags => new[] { "Communication", "Email", "Notifications", "SMS" };

        /// <summary>
        /// Gets the channels.
        /// </summary>
        /// <value>The channels.</value>
        private IEnumerable<IChannel> Channels { get; }

        /// <summary>
        /// Gets the communication service.
        /// </summary>
        /// <value>The communication service.</value>
        private ICommunicationService? CommunicationService { get; }

        /// <summary>
        /// Gets the data service.
        /// </summary>
        /// <value>The data service.</value>
        private IDataService? DataService { get; }

        /// <summary>
        /// Gets the hash algorithm.
        /// </summary>
        /// <value>The hash algorithm.</value>
        private HashAlgorithm? HashAlgorithm { get; }

        /// <summary>
        /// Gets the mithril configuration.
        /// </summary>
        /// <value>The mithril configuration.</value>
        private MithrilConfig? MithrilConfig { get; }

        /// <summary>
        /// Creates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="user">The user.</param>
        /// <returns>A command value converted from the ExpandoObject.</returns>
        public override async ValueTask<CommandCreationResult?> CreateAsync(SendMessageCommandVM? value, ClaimsPrincipal user)
        {
            if (string.IsNullOrEmpty(value?.Channel) || CommunicationService is null)
                return new CommandCreationResult(null);
            var Message = CommunicationService.CreateMessage(value.Channel);
            if (Message is null)
                return new CommandCreationResult(null);
            Message.BCC = value.BCC;
            Message.Application = MithrilConfig?.ApplicationName ?? Assembly.GetEntryAssembly()?.GetName().Name;
            Message.Attachments = value.Attachments?.Select(ConvertAttachment).Where(x => x is not null).ToList() ?? new List<Attachment?>();
            Message.Body = value.Body;
            Message.CC = value.CC;
            Message.From = value.From;
            Message.Subject = value.Subject;
            Message.Template = value.Template;
            Message.To = value.To;
            await Message.SaveAsync(DataService, user).ConfigureAwait(false);
            return new CommandCreationResult(new SendMessageCommand(Message));
        }

        /// <summary>
        /// Handles the command.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>The events generated by the command.</returns>
        protected override async Task<IEvent[]> HandleCommandAsync(SendMessageCommand?[]? args)
        {
            if (args is null || Logger is null)
                return Array.Empty<IEvent>();
            List<IEvent> ReturnValues = new List<IEvent>();
            for (var x = 0; x < args.Length; ++x)
            {
                var Arg = args[x];
                if (Arg is null)
                    continue;
                var Channel = Channels.FirstOrDefault(channel => channel.CanHandle(Arg.Message));
                MessageResult? Result = null;
                if (Channel is not null)
                    Result = await Channel.SendMessageAsync(Arg.Message).ConfigureAwait(false);
                Result ??= new MessageResult(
                    $"Channel that can handle {Arg.Message?.GetType().Name ?? "NULL"} not found",
                    new ArgumentOutOfRangeException(nameof(args), $"Channel that can handle {Arg.Message?.GetType().Name ?? "NULL"} not found"));
                ReturnValues.Add(new MessageSentEvent(Arg.Message, Result.Message));
            }
            return ReturnValues.ToArray();
        }

        /// <summary>
        /// Converts the attachment.
        /// </summary>
        /// <param name="attachment">The attachment.</param>
        /// <returns>The resulting Attachment object.</returns>
        private Attachment? ConvertAttachment(AttachmentVM? attachment)
        {
            if (string.IsNullOrEmpty(attachment?.FileName) || string.IsNullOrEmpty(attachment?.Location) || !attachment.FileName.StartsWith("mithril://", StringComparison.OrdinalIgnoreCase))
                return null;
            var ReturnValue = new Attachment
            {
                FileName = attachment.FileName.Replace("..", "")
            };
            attachment.Location = attachment.Location.Replace("..", "");
            var CurrentFile = new FileCurator.FileInfo(attachment.Location?.Trim().TrimEnd('/') + "/" + attachment.FileName.Trim());
            if (!CurrentFile.Exists)
                return null;
            ReturnValue.Size = CurrentFile.Length;
            ReturnValue.FileHash = HashAlgorithm?.ComputeHash(CurrentFile.ReadBinary()).ToString(Encoding.UTF8) ?? "";
            return ReturnValue;
        }
    }
}