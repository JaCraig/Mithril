using BigBook;
using Mithril.Content.Abstractions.Interfaces;
using System.Globalization;

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
            Name = FixName(typeof(TClass).Name);
        }

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
        /// Gets the definition.
        /// </summary>
        /// <returns>The component's definition.</returns>
        public string GetDefinition()
        {
            if (DefaultProperties.Count == 0)
                return $"{{ \"type\": \"{Name}\", \"style\":\"\", \"class\":\"{DefaultClass}\" }}";
            return $"{{ \"type\": \"{Name}\", \"style\":\"\", \"class\":\"{DefaultClass}\", {DefaultProperties.ToString(x => $"\"{x.Key}\": {x.Value}", ", ")} }}";
        }

        /// <summary>
        /// Fixes the name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        private static string FixName(string? name)
        {
            if (string.IsNullOrEmpty(name))
                return "";
            return SplitCamelCase(name).ToLower(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Splits the camel case.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>Splits the camel case names</returns>
        private static string SplitCamelCase(string input)
        {
            return input.AddSpaces().Replace(" ", "-", StringComparison.Ordinal);
        }
    }
}