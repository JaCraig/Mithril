using BigBook;
using GraphQL;
using GraphQL.Types;
using Mithril.API.GraphQL.GraphTypes.Interfaces;
using System.Dynamic;
using System.Linq.Expressions;
using System.Reflection;

namespace Mithril.API.GraphQL.GraphTypes
{
    public class GenericGraphType<TClass> : ObjectGraphType<TClass>, IGraph
    {
        public GenericGraphType()
        {
            Name = typeof(TClass).Name.Replace("`1", "");
            Description = $"{Name} information";
            AutoWire();
        }

        /// <summary>
        /// Gets the type of the nullable base.
        /// </summary>
        /// <value>The type of the nullable base.</value>
        private static Type NullableBaseType { get; } = typeof(Nullable<>);

        /// <summary>
        /// The assembly types
        /// </summary>
        private static readonly Type[] AssemblyTypes = typeof(GenericGraphType<>).Assembly.GetTypes().Where(x => x.IsAssignableTo(typeof(IGraph))).ToArray();

        /// <summary>
        /// The map generic
        /// </summary>
        private readonly MethodInfo? AddBasicFieldGeneric = Array.Find(typeof(GenericGraphType<TClass>).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance), x => string.Equals(x.Name, nameof(GenericGraphType<TClass>.AddBasicField), StringComparison.OrdinalIgnoreCase));

        /// <summary>
        /// The add class field generic
        /// </summary>
        private readonly MethodInfo? AddClassFieldGeneric = Array.Find(typeof(GenericGraphType<TClass>).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance), x => string.Equals(x.Name, nameof(GenericGraphType<TClass>.AddClassField), StringComparison.OrdinalIgnoreCase));

        /// <summary>
        /// The add list field generic
        /// </summary>
        private readonly MethodInfo? AddListFieldGeneric = Array.Find(typeof(GenericGraphType<TClass>).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance), x => string.Equals(x.Name, nameof(GenericGraphType<TClass>.AddListClassField), StringComparison.OrdinalIgnoreCase));

        /// <summary>
        /// The readable properties
        /// </summary>
        private readonly PropertyInfo[] ReadableProperties = typeof(TClass)?.GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public).Where(x => x.CanRead).ToArray() ?? Array.Empty<PropertyInfo>();

        /// <summary>
        /// Automatically wires up known properties of the view model.
        /// </summary>
        protected void AutoWire(params string[] ignoreProperties)
        {
            ignoreProperties ??= Array.Empty<string>();
            foreach (var Property in ReadableProperties
                .Where(x => !ignoreProperties.Any(y => string.Equals(x.Name, y, StringComparison.OrdinalIgnoreCase))))
            {
                if (IsBuiltInType(Property.PropertyType))
                    AddBasicFieldGeneric?.MakeGenericMethod(Property.PropertyType).Invoke(this, new[] { Property });
                else if (IsListType(Property.PropertyType))
                    AddListFieldGeneric?.MakeGenericMethod(FindGraphType(Property.PropertyType.GetIEnumerableElementType())).Invoke(this, new[] { Property });
                else if (IsExpando(Property.PropertyType))
                    AddClassFieldGeneric?.MakeGenericMethod(typeof(JsonGraphType)).Invoke(this, new[] { Property });
                else if (IsClassType(Property.PropertyType) || IsInterfaceType(Property.PropertyType))
                    AddClassFieldGeneric?.MakeGenericMethod(FindGraphType(Property.PropertyType)).Invoke(this, new[] { Property });
            }
        }

        /// <summary>
        /// Finds the type of the graph.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The graph type for the property type specified.</returns>
        private static Type? FindGraphType(Type property)
        {
            var Types = AssemblyTypes.Where(x => x.IsAssignableTo(typeof(ObjectGraphType<>).MakeGenericType(property)) && !x.IsGenericType);
            return Types.FirstOrDefault(x => x.BaseType?.GetGenericArguments()[0] == property);
        }

        /// <summary>
        /// Determines whether [is built in type] [the specified type].
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if [is built in type] [the specified type]; otherwise, <c>false</c>.</returns>
        private static bool IsBuiltInType(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == NullableBaseType)
                type = type.GenericTypeArguments[0];
            return type?.IsPrimitive == true || type == typeof(string) || type == typeof(decimal) || type == typeof(DateTime) || type == typeof(TimeSpan);
        }

        /// <summary>
        /// Determines whether [is class type] [the specified property type].
        /// </summary>
        /// <param name="propertyType">Type of the property.</param>
        /// <returns>
        /// <c>true</c> if [is class type] [the specified property type]; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsClassType(Type propertyType)
        {
            return propertyType.IsClass && propertyType.IsConcrete() && !propertyType.IsGenericType;
        }

        /// <summary>
        /// Determines whether the specified property type is expando.
        /// </summary>
        /// <param name="propertyType">Type of the property.</param>
        /// <returns><c>true</c> if the specified property type is expando; otherwise, <c>false</c>.</returns>
        private static bool IsExpando(Type propertyType)
        {
            return propertyType.IsAssignableTo(typeof(ExpandoObject));
        }

        /// <summary>
        /// Determines whether [is interface type] [the specified property type].
        /// </summary>
        /// <param name="propertyType">Type of the property.</param>
        /// <returns>
        /// <c>true</c> if [is interface type] [the specified property type]; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsInterfaceType(Type propertyType)
        {
            return propertyType.IsInterface && !propertyType.IsGenericType;
        }

        /// <summary>
        /// Determines whether [is list type] [the specified property type].
        /// </summary>
        /// <param name="propertyType">Type of the property.</param>
        /// <returns>
        /// <c>true</c> if [is list type] [the specified property type]; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsListType(Type propertyType)
        {
            var ElementType = propertyType.GetIEnumerableElementType();
            return ElementType != propertyType && !ElementType.IsGenericType;
        }

        /// <summary>
        /// Determines whether the specified property is nullable.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns><c>true</c> if the specified property is nullable; otherwise, <c>false</c>.</returns>
        private static bool IsNullable(PropertyInfo property)
        {
            return !property.PropertyType.IsValueType || Nullable.GetUnderlyingType(property.PropertyType) is not null;
        }

        /// <summary>
        /// Splits the camel case.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        private static string SplitCamelCase(string input)
        {
            return System.Text.RegularExpressions.Regex.Replace(input, "([A-Z])", " $1", System.Text.RegularExpressions.RegexOptions.Compiled).Trim();
        }

        /// <summary>
        /// Adds the basic field.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="property">The property.</param>
        private void AddBasicField<TProperty>(PropertyInfo property)
        {
            if (property is null || property.DeclaringType is null)
                return;
            var ObjectInstance = Expression.Parameter(property.DeclaringType, "x");
            var PropertyGet = Expression.Property(ObjectInstance, property);

            Field(Expression.Lambda<Func<TClass, TProperty>>(PropertyGet, ObjectInstance), nullable: GenericGraphType<TClass>.IsNullable(property));
        }

        /// <summary>
        /// Adds the class field.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="property">The property.</param>
        private void AddClassField<TProperty>(PropertyInfo property)
            where TProperty : class, IGraphType
        {
            if (property is null || property.DeclaringType is null)
                return;
            var PropertyName = (new string(new char[] { property.Name[0] }).ToLower()) + property.Name.Right(property.Name.Length - 1);
            var Description = $"{SplitCamelCase(property.DeclaringType.Name.Replace("VM", "")).Trim()}'s {SplitCamelCase(property.Name).Trim()}.";

            var ObjectType = typeof(IResolveFieldContext<TClass>);
            var ObjectInstance = Expression.Parameter(ObjectType, "x");
            var SourcePropertyGet = Expression.Property(ObjectInstance, ObjectType.GetProperty("Source"));
            var PropertyGet = Expression.Property(SourcePropertyGet, property);

            Field<TProperty>(PropertyName, Description, resolve: Expression.Lambda<Func<IResolveFieldContext<TClass>, object?>>(PropertyGet, ObjectInstance).Compile());
        }

        /// <summary>
        /// Adds the list class field.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="property">The property.</param>
        private void AddListClassField<TProperty>(PropertyInfo property)
            where TProperty : class, IGraphType
        {
            if (property is null || property.DeclaringType is null)
                return;
            var PropertyName = (new string(new char[] { property.Name[0] }).ToLower()) + property.Name.Right(property.Name.Length - 1);
            var Description = $"{SplitCamelCase(property.DeclaringType.Name).Trim()}'s {SplitCamelCase(property.Name).Trim()}.";

            var ObjectType = typeof(IResolveFieldContext<TClass>);
            var ObjectInstance = Expression.Parameter(ObjectType, "x");
            var SourcePropertyGet = Expression.Property(ObjectInstance, ObjectType.GetProperty("Source"));
            var PropertyGet = Expression.Property(SourcePropertyGet, property);

            Field<ListGraphType<TProperty>>(PropertyName, Description, resolve: Expression.Lambda<Func<IResolveFieldContext<TClass>, object?>>(PropertyGet, ObjectInstance).Compile());
        }
    }
}