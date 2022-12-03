using Fast.Activator;
using GraphQL.Types;
using Mithril.API.GraphQL.GraphTypes.ExtensionMethods;

namespace Mithril.API.GraphQL.GraphTypes.Builder
{
    /// <summary>
    /// GraphType manager
    /// </summary>
    public class GraphTypeManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphTypeManager"/> class.
        /// </summary>
        public GraphTypeManager()
        { }

        /// <summary>
        /// Gets or sets the graph types.
        /// </summary>
        /// <value>The graph types.</value>
        private static Dictionary<Type, GraphType> GraphTypes { get; } = new Dictionary<Type, GraphType>();

        /// <summary>
        /// The lock
        /// </summary>
        private static readonly object LockObject = new();

        /// <summary>
        /// Gets the graph type associated with the type of the object.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <returns>The graph type.</returns>
        public GraphType? GetGraphType<TObject>()
        {
            return GetGraphType(typeof(TObject));
        }

        /// <summary>
        /// Gets the graph type associated with the type of the object.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>The graph type.</returns>
        public GraphType? GetGraphType(Type objectType)
        {
            if (objectType is null)
                return null;
            if (GraphTypes.TryGetValue(objectType, out var graphType))
                return graphType;
            lock (LockObject)
            {
                if (GraphTypes.TryGetValue(objectType, out graphType))
                    return graphType;

                var GraphTypeType = objectType.FindGraphType();
                if (GraphTypeType is null)
                    return null;

                var Params = Array.Empty<object?>();
                if (objectType.IsClass || objectType.IsInterface)
                    Params = new object?[] { this };

                GraphTypes.Add(objectType, (GraphType)FastActivator.CreateInstance(GraphTypeType, Params));

                if (GraphTypes.TryGetValue(objectType, out graphType))
                    return graphType;
                return null;
            }
        }
    }
}