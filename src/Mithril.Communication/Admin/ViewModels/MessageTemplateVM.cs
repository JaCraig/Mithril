using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mithril.Admin.Abstractions.BaseClasses;
using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Communication.Models;
using Mithril.Core.Abstractions.Mvc.Context;
using Mithril.Data.Abstractions.Services;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Mithril.Communication.Admin.ViewModels
{
    /// <summary>
    /// Message template VM
    /// </summary>
    /// <seealso cref="EntityBaseClass&lt;MessageTemplate&gt;" />
    public class MessageTemplateVM : EntityBaseClass<MessageTemplate>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageTemplateVM" /> class.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="hostEnvironment">The host environment.</param>
        /// <param name="full">if set to <c>true</c> [full].</param>
        public MessageTemplateVM(MessageTemplate? model, IHostEnvironment? hostEnvironment, bool full)
            : base(model)
        {
            if (model is null)
                return;
            DisplayName = model.DisplayName;
            if (!full)
                return;
            Content = model.GetContent(hostEnvironment);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageTemplateVM"/> class.
        /// </summary>
        public MessageTemplateVM()
        { }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        [DoNotList]
        [Order(2)]
        [Html]
        public string? Content { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        [Order(1)]
        [Required]
        [MaxLength(128)]
        public string? DisplayName { get; set; }

        /// <summary>
        /// Saves the changes asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="dataService">The data service.</param>
        /// <param name="currentUser">The current user.</param>
        /// <returns>
        /// The async task.
        /// </returns>
        public override async Task<MessageTemplate?> SaveAsync(long id, IDataService? dataService, ClaimsPrincipal? currentUser)
        {
            if (string.IsNullOrEmpty(DisplayName))
                return null;
            var Template = MessageTemplate.Load(id, dataService) ?? new MessageTemplate(DisplayName);
            Template.SetContent(HttpContext.Current?.RequestServices.GetService<IHostEnvironment>(), Content);
            await Template.SaveAsync(dataService, currentUser).ConfigureAwait(false);
            return Template;
        }
    }
}