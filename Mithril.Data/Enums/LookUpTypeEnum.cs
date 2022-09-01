using BigBook.Patterns.BaseClasses;
using Mithril.Core.Abstractions.Services;
using Mithril.Data.Models.General;
using System.Globalization;

namespace Mithril.Data.Enums
{
    /// <summary>
    /// LookUpType enum
    /// </summary>
    /// <seealso cref="StringEnumBaseClass{LookUpTypeEnum}"/>
    public class LookUpTypeEnum : StringEnumBaseClass<LookUpTypeEnum>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LookUpTypeEnum"/> class.
        /// </summary>
        public LookUpTypeEnum()
            : base("")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LookUpTypeEnum"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        public LookUpTypeEnum(string name, string? description = null)
            : base(name)
        {
            Description = description ?? name;
        }

        /// <summary>
        /// Gets the type of the component container.
        /// </summary>
        /// <value>The type of the component container.</value>
        public static LookUpTypeEnum ComponentContainerType { get; } = new LookUpTypeEnum("Component Container Type");

        /// <summary>
        /// Contact info type
        /// </summary>
        /// <value>Contact info type</value>
        public static LookUpTypeEnum ContactInfoType { get; } = new LookUpTypeEnum("Contact Info Type");

        /// <summary>
        /// Gets the type of the document.
        /// </summary>
        /// <value>The type of the document.</value>
        public static LookUpTypeEnum DocumentType { get; } = new LookUpTypeEnum("Document Type");

        /// <summary>
        /// Gets the type of the feed.
        /// </summary>
        /// <value>The type of the feed.</value>
        public static LookUpTypeEnum FeedType { get; } = new LookUpTypeEnum("Feed Type");

        /// <summary>
        /// Gets the type of the field.
        /// </summary>
        /// <value>The type of the field.</value>
        public static LookUpTypeEnum FieldType { get; } = new LookUpTypeEnum("Field Type");

        /// <summary>
        /// Gets the type of the form element.
        /// </summary>
        /// <value>The type of the form element.</value>
        public static LookUpTypeEnum FormElementType { get; } = new LookUpTypeEnum("Input Type");

        /// <summary>
        /// Gets the type of the parameter.
        /// </summary>
        /// <value>The type of the parameter.</value>
        public static LookUpTypeEnum ParameterType { get; } = new LookUpTypeEnum("Parameter Type");

        /// <summary>
        /// Gets the type of the query column.
        /// </summary>
        /// <value>The type of the query column.</value>
        public static LookUpTypeEnum QueryColumnType { get; } = new LookUpTypeEnum("Query Column Type");

        /// <summary>
        /// Gets the type of the query.
        /// </summary>
        /// <value>The type of the query.</value>
        public static LookUpTypeEnum QueryType { get; } = new LookUpTypeEnum("Query Type");

        /// <summary>
        /// Gets the type of the report.
        /// </summary>
        /// <value>The type of the report.</value>
        public static LookUpTypeEnum ReportType { get; } = new LookUpTypeEnum("Report Type");

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        public string? Description { get; }

        /// <summary>
        /// The name mapping
        /// </summary>
        /// <value>The name mapping.</value>
        private static Dictionary<string, LookUpTypeEnum> NameMapping { get; } = new Dictionary<string, LookUpTypeEnum>
        {
            [ReportType.ToString().ToUpper(CultureInfo.InvariantCulture)] = ReportType,
            [QueryType.ToString().ToUpper(CultureInfo.InvariantCulture)] = QueryType,
            [ParameterType.ToString().ToUpper(CultureInfo.InvariantCulture)] = ParameterType,
            [FormElementType.ToString().ToUpper(CultureInfo.InvariantCulture)] = FormElementType,
            [FieldType.ToString().ToUpper(CultureInfo.InvariantCulture)] = FieldType,
            [FeedType.ToString().ToUpper(CultureInfo.InvariantCulture)] = FeedType,
            [DocumentType.ToString().ToUpper(CultureInfo.InvariantCulture)] = DocumentType,
            [ContactInfoType.ToString().ToUpper(CultureInfo.InvariantCulture)] = ContactInfoType,
            [ComponentContainerType.ToString().ToUpper(CultureInfo.InvariantCulture)] = ComponentContainerType,
            [QueryColumnType.ToString().ToUpper(CultureInfo.InvariantCulture)] = QueryColumnType
        };

        /// <summary>
        /// Gets the enum types.
        /// </summary>
        /// <returns>The various enum types.</returns>
        public static IEnumerable<LookUpTypeEnum> GetLookUpTypes()
        {
            return NameMapping.Values;
        }

        /// <summary>
        /// Setups the look ups asynchronously.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        /// <returns>The async task.</returns>
        public static Task SetupLookUpTypesAsync(IDataService dataService)
        {
            if (dataService is null)
                return Task.CompletedTask;
            List<Task> Tasks = new List<Task>();
            foreach (var TempType in GetLookUpTypes())
            {
                Tasks.Add(LookUpType.LoadOrCreateAsync(TempType, TempType?.Description ?? "", dataService));
            }
            return Task.WhenAll(Tasks);
        }
    }
}