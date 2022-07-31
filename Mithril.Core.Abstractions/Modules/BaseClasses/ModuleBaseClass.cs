using BigBook;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Mithril.Core.Abstractions.Modules.Interfaces;

namespace Mithril.Core.Abstractions.Modules.BaseClasses
{
    /// <summary>
    /// Module base class
    /// </summary>
    /// <typeparam name="TModule">The type of the module.</typeparam>
    /// <seealso cref="IModule"/>
    /// <seealso cref="IEquatable{TModule}"/>
    /// <seealso cref="IEquatable{ModuleBaseClass}"/>
    /// <seealso cref="IModule"/>
    public abstract class ModuleBaseClass<TModule> : IModule, IEquatable<TModule>
        where TModule : ModuleBaseClass<TModule>, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleBaseClass{TModule}"/> class.
        /// </summary>
        protected ModuleBaseClass()
        {
            ContentPath = $"wwwroot/Content/{ID}/";
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        public abstract string Category { get; }

        /// <summary>
        /// The content path
        /// </summary>
        /// <value>The content path.</value>
        public string ContentPath { get; }

        /// <summary>
        /// Gets the features.
        /// </summary>
        /// <value>The features.</value>
        public abstract IFeature[] Features { get; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string ID { get; } = typeof(TModule)
            .GetName()
            .Trim()
            .Replace(" ", "-", StringComparison.OrdinalIgnoreCase)
            .Replace("--", "-", StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Gets the last modified.
        /// </summary>
        /// <value>The last modified.</value>
        public DateTime LastModified { get; } = new FileInfo(typeof(TModule).Assembly.Location).LastWriteTimeUtc;

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public abstract string Name { get; }

        /// <summary>
        /// Gets the order that they are initialized in.
        /// </summary>
        /// <value>The order that they are initialized in.</value>
        public abstract int Order { get; }

        /// <summary>
        /// Gets the tags.
        /// </summary>
        /// <value>The tags.</value>
        public abstract string[] Tags { get; }

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>The version.</value>
        public string Version { get; } = typeof(TModule).Assembly.GetName().Version?.ToString() ?? string.Empty;

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="class1">The class1.</param>
        /// <param name="class2">The class2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(ModuleBaseClass<TModule>? class1, ModuleBaseClass<TModule>? class2)
        {
            return !(class1 == class2);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="class1">The class1.</param>
        /// <param name="class2">The class2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(ModuleBaseClass<TModule>? class1, ModuleBaseClass<TModule>? class2)
        {
            return EqualityComparer<ModuleBaseClass<TModule>>.Default.Equals(class1, class2);
        }

        /// <summary>
        /// Allows configuration of MVC related items.
        /// </summary>
        /// <param name="builder">The application builder.</param>
        public abstract void Configure(IApplicationBuilder builder);

        /// <summary>
        /// Configures the services for MVC.
        /// </summary>
        /// <param name="services">The services collection.</param>
        public abstract void ConfigureServices(IServiceCollection services);

        /// <summary>
        /// Determines whether the specified <see cref="object"/>, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj) => Equals(obj as TModule);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref>
        /// parameter; otherwise, false.
        /// </returns>
        public bool Equals(TModule? other)
        {
            return other is not null
                   && ID == other.ID;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data
        /// structures like a hash table.
        /// </returns>
        public override int GetHashCode() => HashCode.Combine(ID);

        /// <summary>
        /// Initializes the data.
        /// </summary>
        /// <returns>The async task.</returns>
        public abstract Task InitializeDataAsync();

        /// <summary>
        /// Loads the module using the bootstrapper
        /// </summary>
        /// <param name="bootstrapper">The bootstrapper.</param>
        public virtual void Load(Canister.Interfaces.IBootstrapper? bootstrapper)
        { }

        /// <summary>
        /// Shutdowns this instance.
        /// </summary>
        public abstract void Shutdown();
    }
}