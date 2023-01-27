namespace Mithril.Data.Apm
{
    /// <summary>
    /// Query metrics data holder
    /// </summary>
    public class QueryMetrics
    {
        /// <summary>
        /// Gets or sets the command text.
        /// </summary>
        /// <value>The command text.</value>
        public string? CommandText { get; set; }

        /// <summary>
        /// Gets or sets the database.
        /// </summary>
        /// <value>The database.</value>
        public string? Database { get; set; }

        /// <summary>
        /// Gets or sets the data source.
        /// </summary>
        /// <value>The data source.</value>
        public string? DataSource { get; set; }

        /// <summary>
        /// Gets or sets the exception number.
        /// </summary>
        /// <value>The exception number.</value>
        public int ExceptionNumber { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the time taken in ms.
        /// </summary>
        /// <value>The time taken in ms.</value>
        public long StartTime { get; set; }
    }
}