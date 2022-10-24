namespace Mithril.API.Query.Interfaces
{
    /// <summary>
    /// Query interface
    /// </summary>
    public interface IQuery
    {
        /// <summary>
        /// Gets the arguments.
        /// </summary>
        /// <value>The arguments.</value>
        Argument[] Arguments { get; }

        /// <summary>
        /// Gets the deprecation reason.
        /// </summary>
        /// <value>The deprecation reason.</value>
        string DeprecationReason { get; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        string Description { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Gets the nullable.
        /// </summary>
        /// <value>The nullable.</value>
        bool? Nullable { get; }

        /// <summary>
        /// Gets the type of the return.
        /// </summary>
        /// <value>The type of the return.</value>
        Type ReturnType { get; }
    }

    /// <summary>
    /// Query interface
    /// </summary>
    /// <typeparam name="TClass">The type of the class.</typeparam>
    public interface IQuery<TClass> : IQuery
        where TClass : class
    {
        /// <summary>
        /// Gets the resolver.
        /// </summary>
        /// <value>The resolver.</value>
        Resolver<TClass> Resolver { get; }
    }
}