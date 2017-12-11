using Bb.Oracle.Models;
using Pssa.Sdk.DataAccess.Dao.Oracle;
using Pssa.Tools.Databases.Generators.Queries;
using Pssa.Tools.Databases.Generators.Queries.Oracle;
using System;
using System.Collections.Generic;
using System.IO;

namespace Pssa.Tools.Databases.Generators
{

    public partial class Database
    {

        static Database()
        {
            OwnerNames = new List<string>();
        }

        public static List<string> OwnerNames { get; set; }
        public static List<string> ProcedureNames { get; internal set; }
        /// <summary>
        /// Gets the table names.
        /// </summary>
        /// <value>
        /// The table names.
        /// </value>
        public static List<string> TableNames { get; internal set; }

        /// <summary>
        /// Generates the file.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="outputfileFullPath">The outputfile full path.</param>
        /// <param name="use">The use.</param>
        /// <returns></returns>
        public static OracleDatabase GenerateFile(string sourceName, string connectionString, string outputfileFullPath,  Func<string, bool> use = null, bool excludeCode = false, string name = "")
        {

            System.Data.Common.DbConnectionStringBuilder builder = new System.Data.Common.DbConnectionStringBuilder();
            builder.ConnectionString = connectionString;
            
            var manager = new OracleManager(connectionString);
            if (System.Diagnostics.Debugger.IsAttached)
                Console.WriteLine("{0} Success", connectionString);

            if (use == null)
                use = shema => true;

            DbContextOracle dbContext = new DbContextOracle(manager) { Use = use, ExcludeCode = excludeCode };
        
            dbContext.database = new OracleDatabase()
            {
                Name = "Instance server " +  sourceName,
                SourceScript = false
            };

            if (!string.IsNullOrEmpty(name))
                dbContext.database.Name = name;

            //dbContext.database.Name = builder["data source"].ToString();
            
            Run(dbContext);

            if (!string.IsNullOrEmpty(outputfileFullPath))
            {

                FileInfo f = new FileInfo(outputfileFullPath);
                if (!f.Directory.Exists)
                    f.Directory.Create();

                Console.WriteLine("Writing file at " + outputfileFullPath);
                // dbContext.database.WriteFile(outputfileFullPath);
            }

            Console.WriteLine("the end");

            return dbContext.database;

        }

        private static void Run<T>(DbContextOracle dbContext, DbQueryBase<T> query, string text, bool exclude = false)
        {
            if (!exclude)
            {
                Console.WriteLine(text);
                query.Resolve(dbContext, null);
            }
            else
                Console.WriteLine("exclude " + text);

        }


        private static void Run(DbContextOracle dbContext)
        {

            //Run(dbContext, new OwnerNameQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "collect of schema");

            //Run(dbContext, new ObjectQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve objects");

            //Run(dbContext, new SequenceQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve sequences");

            //Run(dbContext, new TableQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve tables");
            //Run(dbContext, new ViewSourceQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve code views");
            //Run(dbContext, new MaterializedViewSourceQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve code materialized views");
            //Run(dbContext, new TableColumnQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve columns of tables");

            //Run(dbContext, new IndexColumnQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve indexes of tables");
            //Run(dbContext, new ConstraintsQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve constraints tables");
            //Run(dbContext, new ConstraintColumnQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve IndexColumnQuery columns");
            //Run(dbContext, new TableDefaultValueQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve columns default values");
            //Run(dbContext, new EncryptedTableColumnQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve Encrypted columns");

            


            //Run(dbContext, new PartitionsQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve partitions");
            //Run(dbContext, new SubPartitionsQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve sub partitions");
            //Run(dbContext, new TablePartitionColumnQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve table partitions");
            //Run(dbContext, new IndexPartitionColumnQuery() { OwnerNames = Database.OwnerNames }, "Resolve index partitions");
            //Run(dbContext, new PartitionColumnQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve partition index");
            //Run(dbContext, new SubpartitionColumnQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve sub partition index");

            //Run(dbContext, new ProcQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext, ProcedureNames = Database.ProcedureNames }, "Resolve oracle stored procedures");
            //Run(dbContext, new ProcQueryWithArgument() { OwnerNames = Database.OwnerNames, OracleContext = dbContext, ProcedureNames = Database.ProcedureNames }, "Resolve oracle stored procedures with arguments");
            //Run(dbContext, new TypeQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve oracle types");
            //// Run(dbContext, new ViewQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve oracle views");

            //Run(dbContext, new SynonymQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve oracle synonymes");
            Run(dbContext, new GrantQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve database grants");

            //Run(dbContext, new TriggerQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve triggers");
            //Run(dbContext, new ContentCodeQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve sources", dbContext.ExcludeCode);

            //// Run(dbContext, new TablespacesQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve sources", dbContext.ExcludeCode);



            

        }

    }


}

