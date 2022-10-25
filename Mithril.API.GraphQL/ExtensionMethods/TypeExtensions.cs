using BigBook;
using GraphQL;
using GraphQL.Types;
using Mithril.API.GraphQL.GraphTypes;
using System.Dynamic;
using System.Numerics;
using System.Reflection;

namespace Mithril.API.GraphQL.ExtensionMethods
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Gets the assemblies.
        /// </summary>
        /// <value>The assemblies.</value>
        private static Assembly[] Assemblies { get; set; }

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
        };

        /// <summary>
        /// The assembly types
        /// </summary>
        private static Type[] GraphTypes { get; set; }

        /// <summary>
        /// Gets the type of the nullable base.
        /// </summary>
        /// <value>The type of the nullable base.</value>
        private static Type NullableBaseType { get; } = typeof(Nullable<>);

        /// <summary>
        /// Sets the assembly lock.
        /// </summary>
        /// <value>The assembly lock.</value>
        private static readonly object AssemblyLock = new();

        /// <summary>
        /// The graph type lock
        /// </summary>
        private static readonly object GraphTypeLock = new();

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
            else if (type.IsListType())
            {
                var ListType = type.GetIEnumerableElementType().FindGraphType();
                if (ListType is null)
                    return null;
                return typeof(ListGraphType<>).MakeGenericType(ListType);
            }
            else if (type.IsExpando())
            {
                return typeof(JsonGraphType);
            }
            else if (type.IsClassType() || type.IsInterfaceType())
            {
                return typeof(GenericGraphType<>).MakeGenericType(type);
            }
            return null;
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
            return type is not null && type.IsClass && type.IsConcrete() && !type.IsGenericType;
        }

        /// <summary>
        /// Determines whether the specified property type is expando.
        /// </summary>
        /// <param name="type">Type of the property.</param>
        /// <returns><c>true</c> if the specified property type is expando; otherwise, <c>false</c>.</returns>
        public static bool IsExpando(this Type? type)
        {
            return type is not null && type.IsAssignableTo(typeof(ExpandoObject));
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
            return type is not null && type.IsInterface && !type.IsGenericType;
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
            return ElementType != type && !ElementType.IsGenericType;
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
        /// Finds the assemblies.
        /// </summary>
        /// <returns></returns>
        private static void FindAssemblies()
        {
            if (Assemblies is not null)
                return;
            lock (AssemblyLock)
            {
                if (Assemblies is not null)
                    return;
                var EntryAssembly = Assembly.GetEntryAssembly();
                if (EntryAssembly is null)
                {
                    Assemblies = Array.Empty<Assembly>();
                    return;
                }
                var ExecutingAssembly = Assembly.GetExecutingAssembly();
                if (ExecutingAssembly is null)
                {
                    Assemblies = Array.Empty<Assembly>();
                    return;
                }
                var AssembliesFound = new List<Assembly>
                {
                    EntryAssembly,
                    ExecutingAssembly
                };
                var PathsFound = new List<string>
                {
                    EntryAssembly.Location,
                    ExecutingAssembly.Location
                };
                foreach (var Path in PathsFound)
                {
                    foreach (var TempAssembly in new FileInfo(Path).Directory?.EnumerateFiles("*.dll", SearchOption.TopDirectoryOnly) ?? Array.Empty<FileInfo>())
                    {
                        try
                        {
                            var LoadedTempAssembly = Assembly.Load(AssemblyName.GetAssemblyName(TempAssembly.FullName));
                            if (!AssembliesFound.Contains(LoadedTempAssembly))
                                AssembliesFound.Add(LoadedTempAssembly);
                        }
                        catch { }
                    }
                }
                Assemblies = AssembliesFound.ToArray();
            }
        }

        /// <summary>
        /// Finds the graph types.
        /// </summary>
        /// <returns></returns>
        private static Type[] FindGraphTypes()
        {
            FindAssemblies();
            if (GraphTypes is not null)
                return GraphTypes;
            lock (GraphTypeLock)
            {
                if (GraphTypes is not null)
                    return GraphTypes;
                GraphTypes = Assemblies.SelectMany(x => x.GetTypes().Where(x => x.IsAssignableTo(typeof(IGraphType)) && !x.IsGenericType)).ToArray();
            }
            return GraphTypes;
        }
    }
}