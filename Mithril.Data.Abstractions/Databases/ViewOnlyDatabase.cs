using Inflatable.DataSource;
using Inflatable.Enums;
using Inflatable.Interfaces;
using System.Data.Common;
using System.Data.SqlClient;

namespace Mithril.Data.Inflatable.Databases
{
    /// <summary>
    /// View database
    /// </summary>
    /// <seealso cref="IDatabase"/>
    public class ViewOnlyDatabase : IDatabase
    {
        /// <summary>
        /// Name associated with the database/connection string
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; } = "Default";

        /// <summary>
        /// Order that this database should be in (if only one database is being used, it is ignored)
        /// </summary>
        /// <value>The order.</value>
        public int Order { get; } = 1;

        /// <summary>
        /// Gets the provider.
        /// </summary>
        /// <value>The provider.</value>
        public DbProviderFactory Provider { get; } = SqlClientFactory.Instance;

        /// <summary>
        /// Gets the source options.
        /// </summary>
        /// <value>The source options.</value>
        public Options SourceOptions { get; } = new Options
        {
            Optimize = false,
            Access = SourceAccess.Read,
            Audit = false,
            SchemaUpdate = SchemaGeneration.NoGeneration,
            Analysis = SchemaAnalysis.NoAnalysis
        };
    }
}