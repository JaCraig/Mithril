using Mithril.Content.Abstractions.BaseClasses;

namespace Mithril.Admin.Abstractions.Components
{
    /// <summary>
    /// Data editor component
    /// TODO: Add tests
    /// </summary>
    /// <seealso cref="ComponentDefinitionBaseClass&lt;DataEditorComponent&gt;" />
    public class DataEditorComponent<TEntity> : ComponentDefinitionBaseClass<DataEditorComponent<TEntity>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataEditorComponent"/> class.
        /// </summary>
        public DataEditorComponent()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataEditorComponent" /> class.
        /// </summary>
        /// <param name="dataType">Type of the data.</param>
        public DataEditorComponent(string dataType)
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
            ["listingSchema"] = "[]",
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