using BigBook;
using GraphQL;
using GraphQL.Types;
using Mithril.API.GraphQL.GraphTypes.Builder;
using Mithril.API.GraphQL.GraphTypes.ExtensionMethods;
using ObjectCartographer;
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
    public class GenericGraphType<TClass> : ObjectGraphType<TClass>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericGraphType{TClass}"/> class.
        /// </summary>
        /// <inheritdoc/>
        public GenericGraphType(GraphTypeManager? graphTypeManager)
        {
            var ObjectType = typeof(TClass);
            Name = GetName(ObjectType);
            Description = ObjectType.GetDescription();
            AutoWire(graphTypeManager ?? new GraphTypeManager());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericGraphType{TClass}"/> class.
        /// </summary>
        /// <inheritdoc/>
        public GenericGraphType()
            : this(null)
        {
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
        /// The add method basic generic
        /// </summary>
        private readonly MethodInfo? AddMethodBasicGeneric = Array.Find(typeof(GenericGraphType<TClass>).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance), x => string.Equals(x.Name, nameof(GenericGraphType<TClass>.AddBasicMethod), StringComparison.OrdinalIgnoreCase));

        /// <summary>
        /// The add method generic
        /// </summary>
        private readonly MethodInfo? AddMethodClassGeneric = Array.Find(typeof(GenericGraphType<TClass>).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance), x => string.Equals(x.Name, nameof(GenericGraphType<TClass>.AddMethodClass), StringComparison.OrdinalIgnoreCase));

        /// <summary>
        /// Automatically wires up known properties of the view model.
        /// </summary>
        protected void AutoWire(GraphTypeManager graphTypeManager)
        {
            foreach (var Property in TypeCacheFor<TClass>.Properties)
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
                    AddClassFieldGeneric?.MakeGenericMethod(GraphType).Invoke(this, new object?[] { Property, graphTypeManager.GetGraphType(Property.PropertyType) });
                }
            }
            foreach (var Method in TypeCacheFor<TClass>.Methods)
            {
                var GraphType = Method.ReturnType.FindGraphType();
                if (GraphType is null)
                    continue;
                if (Method.ReturnType.IsBuiltInType())
                {
                    AddMethodBasicGeneric?.MakeGenericMethod(Method.ReturnType).Invoke(this, new object[] { Method });
                }
                else
                {
                    AddMethodClassGeneric?.MakeGenericMethod(GraphType).Invoke(this, new object?[] { Method, graphTypeManager.GetGraphType(Method.ReturnType) });
                }
            }
        }

        /// <summary>
        /// Gets the parameter.
        /// </summary>
        /// <typeparam name="TReturn">The type of the return.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="name">The name.</param>
        /// <returns>The parameter</returns>
        private static TReturn? GetParameter<TReturn>(IResolveFieldContext<TClass> context, string name)
        {
            if (context.Arguments?.TryGetValue(name, out var param) == true)
                return param.Value.To<TReturn>();
            return default;
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

            Field(Expression.Lambda<Func<TClass, TProperty>>(PropertyGet, ObjectInstance), nullable: property.PropertyType.IsNullable())
                .Description(property.GetDescription())
                ?.SetSecurity(property)
                ?.DeprecationReason(property.GetDeprecationReason());
        }

        /// <summary>
        /// Adds the basic method.
        /// </summary>
        /// <typeparam name="TReturn">The type of the return.</typeparam>
        /// <param name="method">The method.</param>
        private void AddBasicMethod<TReturn>(MethodInfo method)
        {
            if (method is null || method.DeclaringType is null)
                return;

            var ObjectType = typeof(IResolveFieldContext<TClass>);
            var ObjectInstance = Expression.Parameter(ObjectType, "x");
            var SourceProperty = ObjectType.GetProperty(nameof(IResolveFieldContext<TClass>.Source));
            if (SourceProperty is null)
                return;

            var GenericGetParameter = typeof(GenericGraphType<TClass>).GetMethod(nameof(GetParameter), BindingFlags.Static | BindingFlags.NonPublic);
            if (GenericGetParameter is null)
                return;

            var Arguments = method.GetParameters().Select(Param => Expression.Call(null, GenericGetParameter.MakeGenericMethod(Param.ParameterType), ObjectInstance, Expression.Constant(Param.Name?.ToCamelCase() ?? "")));
            var PropertyGet = Expression.Call(Expression.Property(ObjectInstance, SourceProperty), method, Arguments);

            Field<TReturn>(method.GetName(), nullable: method.ReturnType.IsNullable())
                .Description(method.GetDescription())
                .Resolve(Expression.Lambda<Func<IResolveFieldContext<TClass>, TReturn?>>(PropertyGet, ObjectInstance).Compile())
                .Arguments(method.GetParameters().ToArray(x => x.ToQueryArgument()) ?? Array.Empty<QueryArgument>())
                ?.SetSecurity(method)
                ?.DeprecationReason(method.GetDeprecationReason());
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

            var ObjectType = typeof(IResolveFieldContext<TClass>);
            var ObjectInstance = Expression.Parameter(ObjectType, "x");
            var SourceProperty = ObjectType.GetProperty(nameof(IResolveFieldContext<TClass>.Source));
            if (SourceProperty is null)
                return;

            var SourcePropertyGet = Expression.Property(ObjectInstance, SourceProperty);
            var PropertyGet = Expression.Property(SourcePropertyGet, property);
            Field<TProperty>(property.GetName(),
                    property.GetDescription(),
                    resolve: Expression.Lambda<Func<IResolveFieldContext<TClass>, object?>>(PropertyGet, ObjectInstance).Compile(),
                    deprecationReason: property.GetDeprecationReason())
                .SetSecurity(property);
        }

        /// <summary>
        /// Adds the method class type.
        /// </summary>
        /// <typeparam name="TGraphType">The type of the graph type.</typeparam>
        /// <param name="method">The method.</param>
        /// <param name="graphType">Type of the graph.</param>
        private void AddMethodClass<TGraphType>(MethodInfo method, IGraphType graphType)
                    where TGraphType : IGraphType
        {
            if (method is null || method.DeclaringType is null)
                return;

            var ObjectType = typeof(IResolveFieldContext<TClass>);
            var ObjectInstance = Expression.Parameter(ObjectType, "x");
            var SourceProperty = ObjectType.GetProperty(nameof(IResolveFieldContext<TClass>.Source));
            if (SourceProperty is null)
                return;

            var GenericGetParameter = typeof(GenericGraphType<TClass>).GetMethod(nameof(GetParameter), BindingFlags.Static | BindingFlags.NonPublic);
            if (GenericGetParameter is null)
                return;

            var Arguments = method.GetParameters().Select(Param => Expression.Call(null, GenericGetParameter.MakeGenericMethod(Param.ParameterType), ObjectInstance, Expression.Constant(Param.Name?.ToCamelCase() ?? "")));

            var PropertyGet = Expression.Call(Expression.Property(ObjectInstance, SourceProperty), method, Arguments);

            Field<TGraphType>(method.GetName(),
                    method.GetDescription(),
                    arguments: new QueryArguments(method.GetParameters().ToArray(x => x.ToQueryArgument())),
                    resolve: Expression.Lambda<Func<IResolveFieldContext<TClass>, object?>>(PropertyGet, ObjectInstance).Compile(),
                    deprecationReason: method.GetDeprecationReason())
                .SetSecurity(method);
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