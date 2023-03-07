using BigBook;
using GraphQL.Types;
using Mithril.API.Abstractions.Attributes;
using System.Dynamic;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Mithril.API.GraphQL.GraphTypes.ExtensionMethods
{
    /// <summary>
    /// Type extensions
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Gets the built in graph types.
        /// </summary>
        /// <value>The built in graph types.</value>
        private static Dictionary<Type, Type> BuiltInGraphTypes { get; } = new Dictionary<Type, Type>
        {
            [typeof(string)] = typeof(StringGraphType),
            [typeof(int)] = typeof(IntGraphType),
            [typeof(float)] = typeof(FloatGraphType),
            [typeof(bool)] = typeof(BooleanGraphType),
            [typeof(BigInteger)] = typeof(BigIntGraphType),
            [typeof(byte)] = typeof(ByteGraphType),
            [typeof(DateTime)] = typeof(DateTimeGraphType),
            [typeof(DateOnly)] = typeof(DateOnlyGraphType),
            [typeof(DateTimeOffset)] = typeof(DateTimeOffsetGraphType),
            [typeof(decimal)] = typeof(DecimalGraphType),
            [typeof(double)] = typeof(DecimalGraphType),
            [typeof(Guid)] = typeof(GuidGraphType),
            [typeof(long)] = typeof(LongGraphType),
            [typeof(TimeSpan)] = typeof(TimeSpanMillisecondsGraphType),
            [typeof(sbyte)] = typeof(ShortGraphType),
            [typeof(short)] = typeof(ShortGraphType),
            [typeof(TimeOnly)] = typeof(TimeOnlyGraphType),
            [typeof(uint)] = typeof(UIntGraphType),
            [typeof(ulong)] = typeof(ULongGraphType),
            [typeof(Uri)] = typeof(UriGraphType),
            [typeof(ushort)] = typeof(UShortGraphType),

            [typeof(int?)] = typeof(IntGraphType),
            [typeof(float?)] = typeof(FloatGraphType),
            [typeof(bool?)] = typeof(BooleanGraphType),
            [typeof(byte?)] = typeof(ByteGraphType),
            [typeof(DateTime?)] = typeof(DateTimeGraphType),
            [typeof(DateOnly?)] = typeof(DateOnlyGraphType),
            [typeof(DateTimeOffset?)] = typeof(DateTimeOffsetGraphType),
            [typeof(decimal?)] = typeof(DecimalGraphType),
            [typeof(double?)] = typeof(DecimalGraphType),
            [typeof(Guid?)] = typeof(GuidGraphType),
            [typeof(long?)] = typeof(LongGraphType),
            [typeof(TimeSpan?)] = typeof(TimeSpanMillisecondsGraphType),
            [typeof(sbyte?)] = typeof(ShortGraphType),
            [typeof(short?)] = typeof(ShortGraphType),
            [typeof(TimeOnly?)] = typeof(TimeOnlyGraphType),
            [typeof(uint?)] = typeof(UIntGraphType),
            [typeof(ulong?)] = typeof(ULongGraphType),
            [typeof(ushort?)] = typeof(UShortGraphType),
        };

        /// <summary>
        /// Gets the type of the nullable base.
        /// </summary>
        /// <value>The type of the nullable base.</value>
        private static Type NullableBaseType { get; } = typeof(Nullable<>);

        /// <summary>
        /// Finds the graph type associated with the C# type.
        /// </summary>
        /// <param name="type">The property.</param>
        /// <returns>The graph type</returns>
        public static Type? FindGraphType(this Type? type)
        {
            if (type is null)
                return null;
            if (type.IsBuiltInType())
            {
                BuiltInGraphTypes.TryGetValue(type, out var graphType);
                return graphType;
            }
            else if (type.IsExpando())
            {
                return typeof(JsonGraphType);
            }
            else if (type.IsListType())
            {
                var ListType = type.GetIEnumerableElementType().FindGraphType();
                if (ListType is null)
                    return null;
                return typeof(ListGraphType<>).MakeGenericType(ListType);
            }
            else if (type.IsClassType() || type.IsInterfaceType())
            {
                return typeof(GenericGraphType<>).MakeGenericType(type);
            }
            return null;
        }

        /// <summary>
        /// Gets the methods that are mappable by the system.
        /// </summary>
        /// <param name="classType">Type of the class.</param>
        /// <returns>The methods.</returns>
        public static MethodInfo[] GetMappableMethods(this Type classType)
        {
            if (classType is null)
                return Array.Empty<MethodInfo>();
            var Methods = new List<MethodInfo>();
            Methods.AddRange(classType.GetMethods(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public));
            foreach (var Method in classType.GetInterfaces().SelectMany(Interface => Interface.GetMethods(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public)))
            {
                if (Methods.Any(x => x.Name == Method.Name))
                    continue;
                Methods.Add(Method);
            }
            return Methods.Where(x => !x.IsGenericMethod
                                    && !string.Equals(x.Name, "<Clone>$", StringComparison.OrdinalIgnoreCase)
                                    && !string.Equals(x.Name, "GetHashCode", StringComparison.OrdinalIgnoreCase)
                                    && !string.Equals(x.Name, "ToString", StringComparison.OrdinalIgnoreCase)
                                    && !string.Equals(x.Name, "GetType", StringComparison.OrdinalIgnoreCase)
                                    && !x.Name.StartsWith("set_", StringComparison.OrdinalIgnoreCase)
                                    && !x.Name.StartsWith("get_", StringComparison.OrdinalIgnoreCase)
                                    && x.GetCustomAttribute<ApiIgnoreAttribute>() is null
                                    && IsTypeValidForGraph(x.ReturnType)
                                    && x.GetParameters().All(x => IsTypeValidForGraph(x.ParameterType))
                                    && x.GetParameters().All(x => x.ParameterType.FindGraphType()?.IsAssignableTo(typeof(ScalarGraphType)) ?? false))
                          .ToArray();
        }

        /// <summary>
        /// Gets the mappable properties.
        /// </summary>
        /// <param name="classType">Type of the class.</param>
        /// <returns>The mappable properties</returns>
        public static PropertyInfo[] GetMappableProperties(this Type classType)
        {
            if (classType is null)
                return Array.Empty<PropertyInfo>();
            var Properties = new List<PropertyInfo>();
            Properties.AddRange(classType.GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public).Where(x => x.CanRead));
            foreach (var Property in classType.GetInterfaces().SelectMany(Interface => Interface.GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public).Where(x => x.CanRead)))
            {
                if (Properties.Any(x => x.Name == Property.Name))
                    continue;
                Properties.Add(Property);
            }
            return Properties.Where(x => x.GetCustomAttribute<ApiIgnoreAttribute>() is null && x.PropertyType.FindGraphType() is not null && x.GetIndexParameters().Length == 0).ToArray();
        }

        /// <summary>
        /// Determines whether [is built in type] [the specified type].
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if [is built in type] [the specified type]; otherwise, <c>false</c>.</returns>
        public static bool IsBuiltInType(this Type? type)
        {
            if (type is null)
                return false;
            if (type.IsGenericType && type.GetGenericTypeDefinition() == NullableBaseType)
                type = type.GenericTypeArguments[0];
            return type.IsPrimitive || BuiltInGraphTypes.ContainsKey(type);
        }

        /// <summary>
        /// Determines whether [is class type] [the specified property type].
        /// </summary>
        /// <param name="type">Type of the property.</param>
        /// <returns>
        /// <c>true</c> if [is class type] [the specified property type]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsClassType(this Type? type)
        {
            return type?.IsClass == true && !type.IsAbstract && !type.IsInterface && !type.ContainsGenericParameters;
        }

        /// <summary>
        /// Determines whether the specified property type is expando.
        /// </summary>
        /// <param name="type">Type of the property.</param>
        /// <returns><c>true</c> if the specified property type is expando; otherwise, <c>false</c>.</returns>
        public static bool IsExpando(this Type? type)
        {
            return type?.IsAssignableTo(typeof(ExpandoObject)) == true;
        }

        /// <summary>
        /// Determines whether [is interface type] [the specified property type].
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// <c>true</c> if [is interface type] [the specified property type]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsInterfaceType(this Type? type)
        {
            return type?.IsInterface == true && !type.ContainsGenericParameters;
        }

        /// <summary>
        /// Determines whether [is list type] [the specified property type].
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// <c>true</c> if [is list type] [the specified property type]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsListType(this Type? type)
        {
            if (type is null)
                return false;
            var ElementType = type.GetIEnumerableElementType();
            return ElementType != type && !ElementType.ContainsGenericParameters;
        }

        /// <summary>
        /// Determines whether the specified property is nullable.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if the specified property is nullable; otherwise, <c>false</c>.</returns>
        public static bool IsNullable(this Type? type)
        {
            return type is not null && (!type.IsValueType || Nullable.GetUnderlyingType(type) is not null);
        }

        /// <summary>
        /// Determines whether [is type valid for graph] [the specified type].
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// <c>true</c> if [is type valid for graph] [the specified type]; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsTypeValidForGraph(Type type)
        {
            return !type.IsByRef
                && type != typeof(TaskAwaiter)
                && type != typeof(TaskStatus)
                && !type.IsAssignableTo(typeof(Task))
                && type.FindGraphType() is not null
                && type != typeof(void);
        }
    }
}