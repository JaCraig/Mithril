using Mithril.Admin.Abstractions.DataEditor;
using Mithril.Admin.Abstractions.Services;
using Mithril.Content.Abstractions.BaseClasses;
using System.Text.Json;

namespace Mithril.Admin.Abstractions.Components
{
    /// <summary>
    /// Settings editor component
    /// </summary>
    /// <seealso cref="ComponentDefinitionBaseClass&lt;SettingsEditorComponent&gt;"/>
    public class SettingsEditorComponent<TEntity> : ComponentDefinitionBaseClass<SettingsEditorComponent<TEntity>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsEditorComponent{TEntity}"/> class.
        /// </summary>
        public SettingsEditorComponent()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsEditorComponent{TEntity}"/> class.
        /// </summary>
        /// <param name="dataType">Type of the data.</param>
        /// <param name="entityMetadataService">The entity metadata service.</param>
        public SettingsEditorComponent(string dataType, IEntityMetadataService? entityMetadataService)
        {
            DataType = dataType;
            DefaultProperties["dataType"] = $"\"{DataType}\"";
            DefaultProperties["modelSchema"] = JsonSerializer.Serialize(entityMetadataService?.ExtractMetadata<TEntity>()?.Properties ?? []);
        }

        /// <summary>
        /// Gets the default properties.
        /// </summary>
        /// <value>The default properties.</value>
        public override Dictionary<string, string> DefaultProperties { get; } = new Dictionary<string, string>
        {
            ["dataType"] = "\"\"",
            ["modelSchema"] = "[]"
        };

        /// <summary>
        /// Gets the type of the data.
        /// </summary>
        /// <value>The type of the data.</value>
        private string? DataType { get; }
    }
}