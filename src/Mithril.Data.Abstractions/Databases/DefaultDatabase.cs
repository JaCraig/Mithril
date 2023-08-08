using Inflatable.DataSource;
using Inflatable.Enums;
using Inflatable.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace Mithril.Data.Abstractions.Databases
{
    /// <summary>
    /// Default database
    /// </summary>
    /// <seealso cref="IDatabase"/>
    public class DefaultDatabase : IDatabase
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
            Optimize = true,
            Access = SourceAccess.Read | SourceAccess.Write,
            Audit = false,
            SchemaUpdate = SchemaGeneration.UpdateSchema,
            Analysis = SchemaAnalysis.NoAnalysis
        };
    }
}