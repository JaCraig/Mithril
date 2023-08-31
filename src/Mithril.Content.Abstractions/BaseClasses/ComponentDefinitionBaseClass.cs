using BigBook;
using Mithril.Content.Abstractions.Interfaces;
using System.Dynamic;
using System.Globalization;
using System.Text.Json;

namespace Mithril.Content.Abstractions.BaseClasses
{
    /// <summary>
    /// Component definition base class
    /// </summary>
    /// <typeparam name="TClass">The type of the class.</typeparam>
    /// <seealso cref="IComponentDefinition"/>
    public abstract class ComponentDefinitionBaseClass<TClass> : IComponentDefinition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentDefinitionBaseClass{TClass}"/> class.
        /// </summary>
        protected ComponentDefinitionBaseClass()
        {
            Name = typeof(TClass).Name.Split("`").FirstOrDefault().AddSpaces() ?? "Component";
            ComponentType = FixName(typeof(TClass).Name);
        }

        /// <summary>
        /// The lock object
        /// </summary>
        private readonly object LockObject = new();

        /// <summary>
        /// The schema
        /// </summary>
        private ExpandoObject? _schema;

        /// <summary>
        /// Gets the default class.
        /// </summary>
        /// <value>The default class.</value>
        public virtual string DefaultClass { get; } = "primary";

        /// <summary>
        /// Gets the default properties.
        /// </summary>
        /// <value>The default properties.</value>
        public abstract Dictionary<string, string> DefaultProperties { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; }

        /// <summary>
        /// Gets the schema.
        /// </summary>
        /// <value>
        /// The schema.
        /// </value>
        public ExpandoObject? Schema
        {
            get
            {
                if (_schema is not null)
                    return _schema;
                lock (LockObject)
                {
                    if (_schema is not null)
                        return _schema;
                    if (DefaultProperties.Count == 0)
                        _schema = JsonSerializer.Deserialize<ExpandoObject>($"{{ \"type\": \"{ComponentType}\", \"style\":\"\", \"class\":\"{DefaultClass}\" }}");
                    _schema = JsonSerializer.Deserialize<ExpandoObject>($"{{ \"type\": \"{ComponentType}\", \"style\":\"\", \"class\":\"{DefaultClass}\", {DefaultProperties.ToString(x => $"\"{x.Key}\": {x.Value}", ", ")} }}");
                }
                return _schema;
            }
        }

        /// <summary>
        /// Gets the script file.
        /// </summary>
        /// <value>
        /// The script file.
        /// </value>
        public virtual string ScriptFile { get; } = "/Core/js/core.umd.min.js";

        /// <summary>
        /// Gets or sets the type of the component.
        /// </summary>
        /// <value>
        /// The type of the component.
        /// </value>
        private string ComponentType { get; }

        /// <summary>
        /// Fixes the name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The fixed name</returns>
        private static string FixName(string? name) => string.IsNullOrEmpty(name) ? "" : SplitCamelCase(name.Split("`").FirstOrDefault()).ToLower(CultureInfo.InvariantCulture);

        /// <summary>
        /// Splits the camel case.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>Splits the camel case names</returns>
        private static string SplitCamelCase(string? input) => input.AddSpaces().Replace(" ", "-", StringComparison.Ordinal);
    }
}