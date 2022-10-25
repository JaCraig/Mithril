using BigBook;
using Mithril.API.ExtensionMethods;
using Mithril.API.Query.Interfaces;
using System.Security.Claims;

namespace Mithril.API.Query.BaseClasses
{
    /// <summary>
    /// Query base class
    /// </summary>
    /// <typeparam name="TClass">The type of the class.</typeparam>
    /// <seealso cref="IQuery&lt;TClass&gt;"/>
    public abstract class QueryBaseClass<TClass> : IQuery<TClass>
        where TClass : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryBaseClass{TClass}"/> class.
        /// </summary>
        protected QueryBaseClass()
        {
            Description = $"Returns {Name.SplitCamelCase().ToString(StringCase.SentenceCapitalize)} information";
            if (Arguments.Length > 0)
                Description += $" using the following arguments ({Arguments.ToString(x => x.ToString(), ", ")})";
            Description += ".";
        }

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        /// <value>The arguments.</value>
        public virtual IArgument[] Arguments { get; } = Array.Empty<IArgument>();

        /// <summary>
        /// Gets the deprecation reason.
        /// </summary>
        /// <value>The deprecation reason.</value>
        public virtual string? DeprecationReason { get; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        public virtual string? Description { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public virtual string Name { get; } = typeof(TClass).Name.Replace("`", "").Replace("&", "");

        /// <summary>
        /// Gets the nullable.
        /// </summary>
        /// <value>The nullable.</value>
        public virtual bool? Nullable { get; } = IsNullable(typeof(TClass));

        /// <summary>
        /// Gets the type of the return.
        /// </summary>
        /// <value>The type of the return.</value>
        public Type ReturnType { get; } = typeof(TClass);

        /// <summary>
        /// Used to resolve the data asked for by the query.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The data specified.</returns>
        public abstract Task<TClass?> ResolveAsync(ClaimsPrincipal? user, Arguments arguments);

        /// <summary>
        /// Determines whether the specified property is nullable.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if the specified property is nullable; otherwise, <c>false</c>.</returns>
        private static bool IsNullable(Type? type)
        {
            return type is not null && (!type.IsValueType || System.Nullable.GetUnderlyingType(type) is not null);
        }
    }
}