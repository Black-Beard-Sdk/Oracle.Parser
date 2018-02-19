using Bb.Oracle.Reader.Dao;
using Bb.Oracle.Models;
using Bb.Oracle.Models.Configurations;
using Bb.Oracle.Reader.Queries;
using System;
using System.Collections.Generic;
using System.IO;

namespace Bb.Oracle.Reader
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
        public static OracleDatabase GenerateFile(ArgumentContext ctx,  Func<string, bool> use = null)
        {

            System.Data.Common.DbConnectionStringBuilder builder = new System.Data.Common.DbConnectionStringBuilder();
            builder.ConnectionString = string.Format(@"Data source={0};USER ID={1};Password={2};", ctx.Source, ctx.Login, ctx.Pwd);
            
            var manager = new OracleManager(builder.ConnectionString);
            if (System.Diagnostics.Debugger.IsAttached)
                Console.WriteLine("{0} Success", builder.ConnectionString);

            if (use == null)
                use = shema => true;

            DbContextOracle dbContext = new DbContextOracle(manager) { Use = use, ExcludeCode = ctx.ExcludeCode };
        
            dbContext.database = new OracleDatabase()
            {
                Name = "Instance server " + ctx.Source,
                SourceScript = false
            };

            if (!string.IsNullOrEmpty(ctx.Name))
                dbContext.database.Name = ctx.Name;
            else
                dbContext.database.Name = ctx.Source;

            Filtre(ctx, builder.ConnectionString);

            Run(dbContext);

            if (!string.IsNullOrEmpty(ctx.Filename))
            {

                FileInfo f = new FileInfo(ctx.Filename);
                if (!f.Directory.Exists)
                    f.Directory.Create();

                Console.WriteLine("Writing file at " + ctx.Filename);
                dbContext.database.WriteFile(ctx.Filename);
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

            Run(dbContext, new OwnerNameQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "collect of schema");

            Run(dbContext, new ObjectQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve objects");

            Run(dbContext, new SequenceQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve sequences");

            Run(dbContext, new TableQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve tables");
            //Run(dbContext, new ViewSourceQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve code views");
            Run(dbContext, new MaterializedViewSourceQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve code materialized views");
            Run(dbContext, new TableColumnQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve columns of tables");

            Run(dbContext, new IndexColumnQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve indexes of tables");
            Run(dbContext, new ConstraintsQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve constraints tables");
            Run(dbContext, new ConstraintColumnQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve IndexColumnQuery columns");
            Run(dbContext, new TableDefaultValueQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve columns default values");
            Run(dbContext, new EncryptedTableColumnQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve Encrypted columns");

            


            Run(dbContext, new PartitionsQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve partitions");
            Run(dbContext, new SubPartitionsQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve sub partitions");
            Run(dbContext, new TablePartitionColumnQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve table partitions");
            Run(dbContext, new IndexPartitionColumnQuery() { OwnerNames = Database.OwnerNames }, "Resolve index partitions");
            Run(dbContext, new PartitionColumnQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve partition index");
            Run(dbContext, new SubpartitionColumnQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve sub partition index");

            Run(dbContext, new ProcQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext, ProcedureNames = Database.ProcedureNames }, "Resolve oracle stored procedures");
            Run(dbContext, new ProcQueryWithArgument() { OwnerNames = Database.OwnerNames, OracleContext = dbContext, ProcedureNames = Database.ProcedureNames }, "Resolve oracle stored procedures with arguments");
            Run(dbContext, new TypeQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve oracle types");
            //Run(dbContext, new ViewQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve oracle views");

            Run(dbContext, new SynonymQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve oracle synonymes");
            Run(dbContext, new GrantQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve database grants");

            Run(dbContext, new TriggerQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve triggers");
            Run(dbContext, new ContentCodeQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve sources", dbContext.ExcludeCode);

            //Run(dbContext, new TablespacesQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve sources", dbContext.ExcludeCode);
            

        }



        /// <summary>
        /// Filtre
        /// Utilisation
        /// Le filtrage réagit lorsqu'on affecte la valeur 
        /// à une ou plusieurs propriétés quelconques
        /// (OwnerName, PackageName, ProcedureName, TableName)
        /// ci-dessous. Sans aucune affectation, ou avec "" chaine vide, la génération se fait sur 
        /// la totalité de la base de données selon le droit d'accés autorisé
        /// par le login.
        /// </summary>
        static void Filtre(ArgumentContext ctx, string connectionString)
        {

            if (!string.IsNullOrEmpty(ctx.ExcludeFile))
                ExcludeSection.Configuration = ExcludeSection.LoadFile(ctx.ExcludeFile);

            if (ctx.OwnerFilter == null || ctx.OwnerFilter == "*")
                ctx.OwnerFilter = ContextLoader.GetOwners(connectionString, ctx);

            SetFilter(Database.OwnerNames, ctx.OwnerFilter);
            SetFilter(Database.ProcedureNames, ctx.Procedures);
            SetFilter(Database.TableNames, ctx.Tables);

        }

        private static void SetFilter(List<string> list, string arg)
        {
            if (!string.IsNullOrEmpty(arg) && !string.IsNullOrEmpty(arg = arg.Trim()))
                foreach (var item in arg.Split(';'))
                    list.Add(item);
        }


    }


}

