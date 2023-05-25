using Mithril.Content.Abstractions.BaseClasses;

namespace Mithril.Admin.Abstractions.Components
{
    /// <summary>
    /// Settings editor component
    /// TODO: Add tests
    /// </summary>
    /// <seealso cref="ComponentDefinitionBaseClass&lt;SettingsEditorComponent&gt;" />
    public class SettingsEditorComponent<TEntity> : ComponentDefinitionBaseClass<SettingsEditorComponent<TEntity>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsEditorComponent"/> class.
        /// </summary>
        public SettingsEditorComponent()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsEditorComponent" /> class.
        /// </summary>
        /// <param name="dataType">Type of the data.</param>
        public SettingsEditorComponent(string dataType)
        {
            DataType = dataType;
            DefaultProperties["dataType"] = $"\"{DataType}\"";
        }

        /// <summary>
        /// Gets the default properties.
        /// </summary>
        /// <value>
        /// The default properties.
        /// </value>
        public override Dictionary<string, string> DefaultProperties { get; } = new Dictionary<string, string>
        {
            ["dataType"] = "\"\"",
            ["modelSchema"] = "[]"
        };

        /// <summary>
        /// Gets the type of the data.
        /// </summary>
        /// <value>
        /// The type of the data.
        /// </value>
        private string? DataType { get; }
    }
}