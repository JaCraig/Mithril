using BigBook;
using BigBook.Patterns.BaseClasses;
using System.Globalization;

namespace Mithril.Data.Abstractions.BaseClasses
{
    /// <summary>
    /// LookUp "enum" base class
    /// </summary>
    /// <seealso cref="StringEnumBaseClass&lt;LookUpTypeEnum&gt;"/>
    public abstract class LookUpEnumBaseClass<TClass> : StringEnumBaseClass<TClass>
        where TClass : LookUpEnumBaseClass<TClass>, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LookUpEnumBaseClass{TClass}"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="icon">The icon.</param>
        protected LookUpEnumBaseClass(string name, string icon)
            : base(name)
        {
            Icon = icon;
        }

        /// <summary>
        /// The lock object
        /// </summary>
        private static readonly object _LockObject = new();

        /// <summary>
        /// The name mapping
        /// </summary>
        private static Dictionary<string, TClass>? _NameMapping;

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>The icon.</value>
        public string? Icon { get; }

        /// <summary>
        /// The name mapping
        /// </summary>
        /// <value>
        /// The name mapping.
        /// </value>
        protected static Dictionary<string, TClass> NameMapping
        {
            get
            {
                if (_NameMapping is not null)
                    return _NameMapping;
                lock (_LockObject)
                {
                    if (_NameMapping is not null)
                        return _NameMapping;
                    _NameMapping = [];
                    _ = typeof(TClass).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
                        .Where(x => x.PropertyType == typeof(TClass))
                        .Select(x => x.GetValue(null) as TClass)
                        .Where(x => x is not null)
                        .ForEach(x => _NameMapping.Add(x!.Name.ToUpper(CultureInfo.InvariantCulture), x));
                }
                //_NameMapping;
                return _NameMapping;
            }
            set => _NameMapping = value;
        }

        /// <summary>
        /// Gets the type of the address.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The address type specified</returns>
        public static TClass? GetEnum(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;
            var KeyName = name.ToUpper(CultureInfo.InvariantCulture).Replace("-", "", StringComparison.OrdinalIgnoreCase);
            return NameMapping.TryGetValue(KeyName, out TClass? ReturnValue) ? ReturnValue : new TClass() { Name = name };
        }

        /// <summary>
        /// Gets the enum types.
        /// </summary>
        /// <returns>The various enum types.</returns>
        public static IEnumerable<TClass> GetEnums() => NameMapping.Values;
    }
}