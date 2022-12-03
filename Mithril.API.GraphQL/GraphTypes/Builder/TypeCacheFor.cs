using Mithril.API.GraphQL.GraphTypes.ExtensionMethods;
using System.Reflection;

namespace Mithril.API.GraphQL.GraphTypes.Builder
{
    /// <summary>
    /// Type cache info
    /// </summary>
    /// <typeparam name="T">Type to cache.</typeparam>
    public static class TypeCacheFor<T>
    {
        /// <summary>
        /// The interfaces
        /// </summary>
        public static readonly Type[] Interfaces = typeof(T).GetInterfaces();

        /// <summary>
        /// The methods
        /// </summary>
        public static readonly MethodInfo[] Methods = typeof(T).GetMappableMethods();

        /// <summary>
        /// The properties
        /// </summary>
        public static readonly PropertyInfo[] Properties = typeof(T).GetMappableProperties();

        /// <summary>
        /// The type
        /// </summary>
        public static readonly Type Type = typeof(T);
    }
}