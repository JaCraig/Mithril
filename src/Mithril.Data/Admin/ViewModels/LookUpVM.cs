using BigBook;
using DocumentFormat.OpenXml.Wordprocessing;
using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Data.Abstractions.Interfaces;
using Mithril.Data.Models.General;
using System.ComponentModel.DataAnnotations;

namespace Mithril.Data.Admin.ViewModels
{
    /// <summary>
    /// LookUp view model class
    /// </summary>
    public class LookUpVM
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LookUpVM"/> class.
        /// </summary>
        public LookUpVM()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LookUpVM"/> class.
        /// </summary>
        /// <param name="lookUp">The look up.</param>
        public LookUpVM(ILookUp lookUp)
        {
            DisplayName = lookUp.DisplayName;
            Icon = lookUp.Icon;
            ID = lookUp.ID;
        }

        /// <summary>
        /// Display name
        /// </summary>
        [Required]
        [MinLength(1)]
        [MaxLength(64)]
        [Order(2)]
        public string? DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>The icon.</value>
        [Required]
        [MinLength(1)]
        [MaxLength(64)]
        [Order(3)]
        public string? Icon { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Order(1)]
        public long ID { get; }

        /// <summary>
        /// Saves the changes asynchronously.
        /// </summary>
        /// <param name="lookUpType">Type of the look up.</param>
        /// <returns>
        /// The task
        /// </returns>
        public void Save(LookUpType lookUpType)
        {
            if (string.IsNullOrEmpty(DisplayName) || lookUpType is null)
                return;
            var Model = lookUpType.LookUps.FirstOrDefault(x => x.ID == ID || x.DisplayName == DisplayName)
                ?? lookUpType.LookUps.AddAndReturn(new LookUp(DisplayName, Icon ?? "fas fa-magnifying-glass", lookUpType));
            Model.DisplayName = DisplayName;
            Model.Icon = Icon;
            Model.Type = lookUpType;
            Model.DateModified = DateTime.UtcNow;
        }
    }
}