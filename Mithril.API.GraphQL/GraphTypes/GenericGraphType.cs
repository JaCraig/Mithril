using BigBook;
using GraphQL;
using GraphQL.Types;
using Mithril.API.Attributes;
using Mithril.API.ExtensionMethods;
using Mithril.API.GraphQL.ExtensionMethods;
using Mithril.API.GraphQL.GraphTypes.Interfaces;
using System.Linq.Expressions;
using System.Reflection;

namespace Mithril.API.GraphQL.GraphTypes
{
    /// <summary>
    /// Generic graph type
    /// </summary>
    /// <typeparam name="TClass">The type of the class.</typeparam>
    /// <seealso cref="ObjectGraphType&lt;TClass&gt;"/>
    /// <seealso cref="IGraph"/>
    public class GenericGraphType<TClass> : ObjectGraphType<TClass>, IGraph
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericGraphType{TClass}"/> class.
        /// </summary>
        /// <inheritdoc/>
        public GenericGraphType()
        {
            Name = typeof(TClass).Name.Replace("`1", "");
            Description = $"{Name} information";
            AutoWire();
        }

        /// <summary>
        /// The map generic
        /// </summary>
        private readonly MethodInfo? AddBasicFieldGeneric = Array.Find(typeof(GenericGraphType<TClass>).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance), x => string.Equals(x.Name, nameof(GenericGraphType<TClass>.AddBasicField), StringComparison.OrdinalIgnoreCase));

        /// <summary>
        /// The add class field generic
        /// </summary>
        private readonly MethodInfo? AddClassFieldGeneric = Array.Find(typeof(GenericGraphType<TClass>).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance), x => string.Equals(x.Name, nameof(GenericGraphType<TClass>.AddClassField), StringComparison.OrdinalIgnoreCase));

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
            foreach (var Property in ReadableProperties.Where(x => x.GetCustomAttribute<ApiIgnoreAttribute>() is null))
            {
                if (Property.PropertyType.IsBuiltInType())
                {
                    AddBasicFieldGeneric?.MakeGenericMethod(Property.PropertyType).Invoke(this, new[] { Property });
                }
                else
                {
                    var GraphType = Property.PropertyType.FindGraphType();
                    if (GraphType is null)
                        continue;
                    AddClassFieldGeneric?.MakeGenericMethod(GraphType).Invoke(this, new[] { Property });
                }
            }
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

            Field(Expression.Lambda<Func<TClass, TProperty>>(PropertyGet, ObjectInstance), nullable: property.PropertyType.IsNullable());
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
            var Description = $"Returns {property.Name.SplitCamelCase().ToString(StringCase.FirstCharacterUpperCase)} information.";

            var ObjectType = typeof(IResolveFieldContext<TClass>);
            var ObjectInstance = Expression.Parameter(ObjectType, "x");
            var SourcePropertyGet = Expression.Property(ObjectInstance, ObjectType.GetProperty("Source"));
            var PropertyGet = Expression.Property(SourcePropertyGet, property);

            Field<TProperty>(PropertyName, Description, resolve: Expression.Lambda<Func<IResolveFieldContext<TClass>, object?>>(PropertyGet, ObjectInstance).Compile());
        }
    }
}