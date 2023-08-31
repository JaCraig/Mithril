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
    public interface IGenericGraphType
    {
        /// <summary>
        /// Automatically wires up the graph type.
        /// </summary>
        /// <param name="graphTypeManager">The graph type manager.</param>
        void AutoWire(GraphTypeManager graphTypeManager);
    }

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
            Type ObjectType = typeof(TClass);
            Name = GetName(ObjectType);
            Description = ObjectType.GetDescription();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericGraphType{TClass}"/> class.
        /// </summary>
        /// <inheritdoc/>
        public GenericGraphType()
            : this(null)
        {
            AutoWire(new GraphTypeManager());
        }

        /// <summary>
        /// The map generic
        /// </summary>
        private readonly MethodInfo? _AddBasicFieldGeneric = Array.Find(typeof(GenericGraphType<TClass>).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance), x => string.Equals(x.Name, nameof(GenericGraphType<TClass>.AddBasicField), StringComparison.OrdinalIgnoreCase));

        /// <summary>
        /// The add class field generic
        /// </summary>
        private readonly MethodInfo? _AddClassFieldGeneric = Array.Find(typeof(GenericGraphType<TClass>).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance), x => string.Equals(x.Name, nameof(GenericGraphType<TClass>.AddClassField), StringComparison.OrdinalIgnoreCase));

        /// <summary>
        /// The add method basic generic
        /// </summary>
        private readonly MethodInfo? _AddMethodBasicGeneric = Array.Find(typeof(GenericGraphType<TClass>).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance), x => string.Equals(x.Name, nameof(GenericGraphType<TClass>.AddBasicMethod), StringComparison.OrdinalIgnoreCase));

        /// <summary>
        /// The add method generic
        /// </summary>
        private readonly MethodInfo? _AddMethodClassGeneric = Array.Find(typeof(GenericGraphType<TClass>).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance), x => string.Equals(x.Name, nameof(GenericGraphType<TClass>.AddMethodClass), StringComparison.OrdinalIgnoreCase));

        /// <summary>
        /// The initialized
        /// </summary>
        private bool _Initialized = false;

        /// <summary>
        /// Automatically wires up known properties of the view model.
        /// </summary>
        public void AutoWire(GraphTypeManager graphTypeManager)
        {
            if (graphTypeManager is null || _Initialized)
                return;
            _Initialized = true;
            foreach (PropertyInfo Property in TypeCacheFor<TClass>.Properties)
            {
                Type? GraphType = Property.PropertyType.FindGraphType();
                if (GraphType is null)
                    continue;
                _ = Property.PropertyType.IsBuiltInType()
                    ? (_AddBasicFieldGeneric?.MakeGenericMethod(Property.PropertyType).Invoke(this, new[] { Property }))
                    : (_AddClassFieldGeneric?.MakeGenericMethod(GraphType).Invoke(this, new object?[] { Property, graphTypeManager.GetGraphType(Property.PropertyType) }));
            }
            foreach (MethodInfo Method in TypeCacheFor<TClass>.Methods)
            {
                Type? GraphType = Method.ReturnType.FindGraphType();
                if (GraphType is null)
                    continue;
                _ = Method.ReturnType.IsBuiltInType()
                    ? (_AddMethodBasicGeneric?.MakeGenericMethod(Method.ReturnType).Invoke(this, new object[] { Method }))
                    : (_AddMethodClassGeneric?.MakeGenericMethod(GraphType).Invoke(this, new object?[] { Method, graphTypeManager.GetGraphType(Method.ReturnType) }));
            }
        }

        /// <summary>
        /// Gets the parameter.
        /// </summary>
        /// <typeparam name="TReturn">The type of the return.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="name">The name.</param>
        /// <returns>The parameter</returns>
        private static TReturn? GetParameter<TReturn>(IResolveFieldContext<TClass> context, string name) => context.Arguments?.TryGetValue(name, out global::GraphQL.Execution.ArgumentValue Param) == true ? Param.Value.To<TReturn>() : default;

        /// <summary>
        /// Adds the basic field.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="property">The property.</param>
        private void AddBasicField<TProperty>(PropertyInfo property)
        {
            if (property is null || property.DeclaringType is null)
                return;

            ParameterExpression ObjectInstance = Expression.Parameter(typeof(TClass), "x");
            MemberExpression PropertyGet = Expression.Property(ObjectInstance, property);

            _ = (Field(Expression.Lambda<Func<TClass, TProperty>>(PropertyGet, ObjectInstance), nullable: property.PropertyType.IsNullable())
                .Description(property.GetDescription())
                ?.SetSecurity(property)
                ?.DeprecationReason(property.GetDeprecationReason()));
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

            Type ObjectType = typeof(IResolveFieldContext<TClass>);
            ParameterExpression ObjectInstance = Expression.Parameter(ObjectType, "x");
            PropertyInfo? SourceProperty = ObjectType.GetProperty(nameof(IResolveFieldContext<TClass>.Source));
            if (SourceProperty is null)
                return;

            MethodInfo? GenericGetParameter = typeof(GenericGraphType<TClass>).GetMethod(nameof(GetParameter), BindingFlags.Static | BindingFlags.NonPublic);
            if (GenericGetParameter is null)
                return;

            IEnumerable<MethodCallExpression> Arguments = method.GetParameters().Select(param => Expression.Call(null, GenericGetParameter.MakeGenericMethod(param.ParameterType), ObjectInstance, Expression.Constant(param.Name?.ToCamelCase() ?? "")));
            MethodCallExpression PropertyGet = Expression.Call(Expression.Property(ObjectInstance, SourceProperty), method, Arguments);

            _ = (Field<TReturn>(method.GetName(), nullable: method.ReturnType.IsNullable())
                .Description(method.GetDescription())
                .Resolve(Expression.Lambda<Func<IResolveFieldContext<TClass>, TReturn?>>(PropertyGet, ObjectInstance).Compile())
                .Arguments(method.GetParameters().ToArray(x => x.ToQueryArgument()!) ?? Array.Empty<QueryArgument>())
                ?.SetSecurity(method)
                ?.DeprecationReason(method.GetDeprecationReason()));
        }

        /// <summary>
        /// Adds the class field.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="property">The property.</param>
        /// <param name="graphType">Type of the graph.</param>
        [Obsolete]
        private void AddClassField<TProperty>(PropertyInfo property, IGraphType graphType)
            where TProperty : IGraphType
        {
            if (property is null || property.DeclaringType is null)
                return;

            Type ObjectType = typeof(IResolveFieldContext<TClass>);
            ParameterExpression ObjectInstance = Expression.Parameter(ObjectType, "x");
            PropertyInfo? SourceProperty = ObjectType.GetProperty(nameof(IResolveFieldContext<TClass>.Source));
            if (SourceProperty is null)
                return;

            MemberExpression SourcePropertyGet = Expression.Property(ObjectInstance, SourceProperty);
            MemberExpression PropertyGet = Expression.Property(SourcePropertyGet, property);
            _ = Field<TProperty>(property.GetName(),
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
        [Obsolete]
        private void AddMethodClass<TGraphType>(MethodInfo method, IGraphType graphType)
                    where TGraphType : IGraphType
        {
            if (method is null || method.DeclaringType is null)
                return;

            Type ObjectType = typeof(IResolveFieldContext<TClass>);
            ParameterExpression ObjectInstance = Expression.Parameter(ObjectType, "x");
            PropertyInfo? SourceProperty = ObjectType.GetProperty(nameof(IResolveFieldContext<TClass>.Source));
            if (SourceProperty is null)
                return;

            MethodInfo? GenericGetParameter = typeof(GenericGraphType<TClass>).GetMethod(nameof(GetParameter), BindingFlags.Static | BindingFlags.NonPublic);
            if (GenericGetParameter is null)
                return;

            IEnumerable<MethodCallExpression> Arguments = method.GetParameters().Select(param => Expression.Call(null, GenericGetParameter.MakeGenericMethod(param.ParameterType), ObjectInstance, Expression.Constant(param.Name?.ToCamelCase() ?? "")));

            MethodCallExpression PropertyGet = Expression.Call(Expression.Property(ObjectInstance, SourceProperty), method, Arguments);

            _ = Field<TGraphType>(method.GetName(),
                    method.GetDescription(),
                    arguments: new QueryArguments(method.GetParameters().ToArray(x => x.ToQueryArgument()!)),
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
                _ = Output.Append("void");
            }
            else
            {
                if (type.Name.Contains('`', StringComparison.Ordinal))
                {
                    Type[] GenericTypes = type.GetGenericArguments();
                    _ = Output.Append(type.Name, 0, type.Name.IndexOf("`", StringComparison.Ordinal));
                    for (int X = 0, GenericTypesLength = GenericTypes.Length; X < GenericTypesLength; X++)
                    {
                        _ = Output.Append(GetName(GenericTypes[X]));
                    }
                }
                else
                {
                    _ = Output.Append(type.Name);
                }
            }
            return Output.ToString().Replace("&", "", StringComparison.Ordinal);
        }
    }
}