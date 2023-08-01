using Mithril.Admin.Abstractions.DataEditor;
using Mithril.Admin.Abstractions.Services;
using Mithril.Content.Abstractions.BaseClasses;
using System.Text.Json;

namespace Mithril.Admin.Abstractions.Components
{
    /// <summary>
    /// Data editor component
    /// </summary>
    /// <seealso cref="ComponentDefinitionBaseClass&lt;DataEditorComponent&gt;"/>
    public class DataEditorComponent<TEntity> : ComponentDefinitionBaseClass<DataEditorComponent<TEntity>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataEditorComponent{TEntity}"/> class.
        /// </summary>
        public DataEditorComponent()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataEditorComponent{TEntity}"/> class.
        /// </summary>
        /// <param name="dataType">Type of the data.</param>
        /// <param name="entityMetadataService">The entity metadata service.</param>
        public DataEditorComponent(string dataType, IEntityMetadataService? entityMetadataService)
        {
            DataType = dataType;
            DefaultProperties["dataType"] = $"\"{DataType}\"";
            DefaultProperties["modelSchema"] = JsonSerializer.Serialize(entityMetadataService?.ExtractMetadata<TEntity>()?.Properties ?? Array.Empty<PropertyMetadata>());
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