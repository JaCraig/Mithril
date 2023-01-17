using BigBook;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Mithril.API.Abstractions.ExtensionMethods;
using Mithril.API.Abstractions.Query.Interfaces;
using Mithril.Core.Abstractions.Extensions;
using Mithril.Core.Abstractions.Modules.Interfaces;
using System.Security.Claims;

namespace Mithril.API.Abstractions.Query.BaseClasses
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
        /// <param name="logger">The logger.</param>
        /// <param name="featureManager">The feature manager.</param>
        protected QueryBaseClass(ILogger? logger, IFeatureManager? featureManager)
        {
            Logger = logger;
            FeatureManager = featureManager;
            Description = $"Returns {Name.SplitCamelCase().ToString(StringCase.SentenceCapitalize)} information";
            if (Arguments.Length > 0)
                Description += $" using the following arguments ({Arguments.ToString(x => x?.ToString() ?? "", ", ")})";
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
        /// Gets the features associated with this command.
        /// </summary>
        /// <value>The features associated with this command.</value>
        public virtual IFeature[] Features { get; } = Array.Empty<IFeature>();

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public virtual string Name { get; } = typeof(TClass).Name.Replace("`", "").Replace("&", "");

        /// <summary>
        /// Gets the type of the return.
        /// </summary>
        /// <value>The type of the return.</value>
        public Type ReturnType { get; } = typeof(TClass);

        /// <summary>
        /// Gets the feature manager.
        /// </summary>
        /// <value>The feature manager.</value>
        protected IFeatureManager? FeatureManager { get; }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        protected ILogger? Logger { get; }

        /// <summary>
        /// Used to resolve the data asked for by the query.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The data specified.</returns>
        public abstract Task<TClass?> ResolveAsync(ClaimsPrincipal? user, Arguments arguments);

        /// <summary>
        /// Determines whether the associated features are enabled.
        /// </summary>
        /// <returns><c>true</c> if all features are enabled; otherwise, <c>false</c>.</returns>
        protected bool IsFeatureEnabled()
        {
            return FeatureManager.AreFeaturesEnabled(Features);
        }
    }
}