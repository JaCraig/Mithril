using BigBook;
using Fast.Activator;
using GraphQL;
using GraphQL.Types;
using Mithril.API.Abstractions.Attributes;
using Mithril.API.Abstractions.ExtensionMethods;
using Mithril.API.GraphQL.ExtensionMethods;
using Mithril.API.GraphQL.GraphTypes.Interfaces;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

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
            Name = GetName(typeof(TClass));
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
        private readonly PropertyInfo[] ReadableProperties = GetProperties();

        /// <summary>
        /// Automatically wires up known properties of the view model.
        /// </summary>
        protected void AutoWire()
        {
            foreach (var Property in ReadableProperties.Where(x => x.GetCustomAttribute<ApiIgnoreAttribute>() is null))
            {
                var GraphType = Property.PropertyType.FindGraphType();
                if (GraphType is null)
                    continue;
                if (Property.PropertyType.IsBuiltInType())
                {
                    AddBasicFieldGeneric?.MakeGenericMethod(Property.PropertyType).Invoke(this, new[] { Property });
                }
                else
                {
                    AddClassFieldGeneric?.MakeGenericMethod(GraphType).Invoke(this, new object[] { Property, FastActivator.CreateInstance(GraphType) });
                }
            }
        }

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <returns>The properties for the type.</returns>
        private static PropertyInfo[] GetProperties()
        {
            var ClassType = typeof(TClass);
            var Properties = new List<PropertyInfo>();
            Properties.AddRange(ClassType.GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public).Where(x => x.CanRead));
            foreach (var Property in ClassType.GetInterfaces().SelectMany(Interface => Interface.GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public).Where(x => x.CanRead)))
            {
                if (Properties.Any(x => x.Name == Property.Name))
                    continue;
                Properties.Add(Property);
            }
            return Properties.ToArray();
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
            var ObjectInstance = Expression.Parameter(typeof(TClass), "x");
            var PropertyGet = Expression.Property(ObjectInstance, property);

            var FieldBuilder = Field(Expression.Lambda<Func<TClass, TProperty>>(PropertyGet, ObjectInstance), nullable: property.PropertyType.IsNullable());

            var AnonymousAttribute = property.GetCustomAttribute<ApiAllowAnonymousAttribute>();
            if (AnonymousAttribute is not null)
            {
                return;
            }
            var AuthorizeAttribute = property.GetCustomAttribute<ApiAuthorizeAttribute>();
            if (AuthorizeAttribute is null)
                return;
            if (!string.IsNullOrEmpty(AuthorizeAttribute.Roles))
                FieldBuilder.AuthorizeWithRoles(AuthorizeAttribute.Roles ?? "");
            else
                FieldBuilder.AuthorizeWithPolicy(AuthorizeAttribute.PolicyName ?? "");
        }

        /// <summary>
        /// Adds the class field.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="property">The property.</param>
        /// <param name="graphType">Type of the graph.</param>
        private void AddClassField<TProperty>(PropertyInfo property, IGraphType graphType)
            where TProperty : IGraphType
        {
            if (property is null || property.DeclaringType is null)
                return;
            var PropertyName = (new string(new char[] { property.Name[0] }).ToLower()) + property.Name.Right(property.Name.Length - 1);
            var DescriptionAttribute = property.GetCustomAttribute<ApiDescriptionAttribute>();
            var Description = string.IsNullOrEmpty(DescriptionAttribute?.Description) ?
                $"Returns {property.Name.SplitCamelCase().ToString(StringCase.FirstCharacterUpperCase)} information." :
                DescriptionAttribute.Description;

            var ObjectType = typeof(IResolveFieldContext<TClass>);
            var ObjectInstance = Expression.Parameter(ObjectType, "x");
            var SourceProperty = ObjectType.GetProperty("Source");
            if (SourceProperty is null)
                return;
            var SourcePropertyGet = Expression.Property(ObjectInstance, SourceProperty);
            var PropertyGet = Expression.Property(SourcePropertyGet, property);
            var FieldBuilder = Field<TProperty>(PropertyName, Description, resolve: Expression.Lambda<Func<IResolveFieldContext<TClass>, object?>>(PropertyGet, ObjectInstance).Compile());
            var AnonymousAttribute = property.GetCustomAttribute<ApiAllowAnonymousAttribute>();
            if (AnonymousAttribute is not null)
            {
                FieldBuilder.AllowAnonymous();
                return;
            }
            var AuthorizeAttribute = property.GetCustomAttribute<ApiAuthorizeAttribute>();
            if (AuthorizeAttribute is null)
                return;
            if (string.IsNullOrEmpty(AuthorizeAttribute.PolicyName) && string.IsNullOrEmpty(AuthorizeAttribute.Roles))
                FieldBuilder.Authorize();
            else if (!string.IsNullOrEmpty(AuthorizeAttribute.Roles))
                FieldBuilder.AuthorizeWithRoles(AuthorizeAttribute.Roles ?? "");
            else
                FieldBuilder.AuthorizeWithPolicy(AuthorizeAttribute.PolicyName ?? "");
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The type's name</returns>
        private string GetName(Type? type)
        {
            if (type is null)
            {
                return "";
            }

            var Output = new StringBuilder();
            if (type.Name == "Void")
            {
                Output.Append("void");
            }
            else
            {
                if (type.Name.Contains('`', StringComparison.Ordinal))
                {
                    var GenericTypes = type.GetGenericArguments();
                    Output.Append(type.Name, 0, type.Name.IndexOf("`", StringComparison.Ordinal));
                    for (int x = 0, GenericTypesLength = GenericTypes.Length; x < GenericTypesLength; x++)
                    {
                        Output.Append(GetName(GenericTypes[x]));
                    }
                }
                else
                {
                    Output.Append(type.Name);
                }
            }
            return Output.ToString().Replace("&", "", StringComparison.Ordinal);
        }
    }
}