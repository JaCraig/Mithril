using Mithril.Admin.Abstractions.Interfaces;
using Mithril.Content.Abstractions.Interfaces;

namespace Mithril.Admin.Queries.ViewModels
{
    /// <summary>
    /// Editor VM
    /// </summary>
    public class EditorVM
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditorVM" /> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public EditorVM(IEditor? model)
        {
            if (model is null)
                return;
            ComponentDefinition = model.ComponentDefinition;
            Category = model.Category ?? "";
            Description = model.Description ?? "";
            Icon = model.Icon ?? "";
            Name = model.Name ?? "";
        }

        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public string? Category { get; }

        /// <summary>
        /// Gets the component definition.
        /// </summary>
        /// <value>
        /// The component definition.
        /// </value>
        public IComponentDefinition? ComponentDefinition { get; }

        /// <summary>
        /// Gets the data model.
        /// </summary>
        /// <value>
        /// The data model.
        /// </value>
        public Dictionary<string, string> DataModel { get; } = [];

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string? Description { get; }

        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        public string? Icon { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string? Name { get; }
    }
}