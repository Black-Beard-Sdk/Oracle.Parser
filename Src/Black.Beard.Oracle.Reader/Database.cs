using Bb.Oracle.Reader.Dao;
using Bb.Oracle.Models;
using Bb.Oracle.Models.Configurations;
using Bb.Oracle.Reader.Queries;
using System;
using System.Collections.Generic;
using System.IO;
using Bb.Oracle.Structures.Models;
using System.Diagnostics;

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
                Trace.WriteLine("{0} Success", builder.ConnectionString.Replace(ctx.Pwd, "".PadLeft(ctx.Pwd.Length, '*')));

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

            var version = new OracleVersionQuery().GetVersion(dbContext);

            dbContext.Version = version;

            Trace.WriteLine($"server version {version}");

            Run(dbContext);

            if (!string.IsNullOrEmpty(ctx.Filename))
            {

                FileInfo f = new FileInfo(ctx.Filename);
                if (!f.Directory.Exists)
                    f.Directory.Create();

                Trace.WriteLine("Writing file at " + ctx.Filename);
                dbContext.database.WriteFile(ctx.Filename);
            }

            Trace.WriteLine("the end");

            return dbContext.database;

        }

        private static void Run<T>(DbContextOracle dbContext, DbQueryBase<T> query, string text, bool exclude = false)
        {
            if (!exclude)
            {
                Trace.WriteLine(text);
                query.Resolve(dbContext, null);
            }
            else
                Trace.WriteLine("exclude " + text);

        }

        private static void Run(DbContextOracle dbContext)
        {

            switch (dbContext.Version.Major)
            {
            
                case 11:
                    Run(dbContext, new OwnerNameQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "collect of schema");
                    Run(dbContext, new ObjectQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve objects");
                    Run(dbContext, new SequenceQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve sequences");
                    Run(dbContext, new TableQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve tables");
                    //Run(dbContext, new ViewSourceQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve code views");
                    Run(dbContext, new MaterializedViewSourceQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve code materialized views");
                    Run(dbContext, new TableColumnQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve columns of tables");
                    Run(dbContext, new IndexColumnQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve indexes of tables");
                    Run(dbContext, new ConstraintsQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve constraints tables");
                    Run(dbContext, new ConstraintColumnQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve IndexColumnQuery columns");
                    Run(dbContext, new TableDefaultValueQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve columns default values");
                    Run(dbContext, new EncryptedTableColumnQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve Encrypted columns");
                    Run(dbContext, new PartitionsQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve partitions");
                    Run(dbContext, new SubPartitionsQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve sub partitions");
                    Run(dbContext, new TablePartitionColumnQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve table partitions");
                    Run(dbContext, new IndexPartitionColumnQuery_11() { OwnerNames = Database.OwnerNames }, "Resolve index partitions");
                    Run(dbContext, new PartitionColumnQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve partition index");
                    Run(dbContext, new SubpartitionColumnQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve sub partition index");
                    Run(dbContext, new ProcQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext, ProcedureNames = Database.ProcedureNames }, "Resolve oracle stored procedures");
                    Run(dbContext, new ProcQueryWithArgument_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext, ProcedureNames = Database.ProcedureNames }, "Resolve oracle stored procedures with arguments");
                    Run(dbContext, new TypeQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve oracle types");
                    //Run(dbContext, new ViewQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve oracle views");
                    Run(dbContext, new SynonymQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve oracle synonymes");
                    Run(dbContext, new GrantQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve database grants");
                    Run(dbContext, new TriggerQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve triggers");
                    Run(dbContext, new ContentCodeQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve sources", dbContext.ExcludeCode);
                    //Run(dbContext, new TablespacesQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve sources", dbContext.ExcludeCode);
                    break;

                case 12:
                    Run(dbContext, new OwnerNameQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "collect of schema");
                    Run(dbContext, new ObjectQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve objects");
                    Run(dbContext, new SequenceQuery_12() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve sequences");
                    Run(dbContext, new TableQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve tables");
                    //Run(dbContext, new ViewSourceQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve code views");
                    Run(dbContext, new MaterializedViewSourceQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve code materialized views");
                    Run(dbContext, new TableColumnQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve columns of tables");
                    Run(dbContext, new IndexColumnQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve indexes of tables");
                    Run(dbContext, new ConstraintsQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve constraints tables");
                    Run(dbContext, new ConstraintColumnQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve IndexColumnQuery columns");
                    Run(dbContext, new TableDefaultValueQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve columns default values");
                    Run(dbContext, new EncryptedTableColumnQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve Encrypted columns");
                    Run(dbContext, new PartitionsQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve partitions");
                    Run(dbContext, new SubPartitionsQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve sub partitions");
                    Run(dbContext, new TablePartitionColumnQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve table partitions");
                    Run(dbContext, new IndexPartitionColumnQuery_11() { OwnerNames = Database.OwnerNames }, "Resolve index partitions");
                    Run(dbContext, new PartitionColumnQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve partition index");
                    Run(dbContext, new SubpartitionColumnQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve sub partition index");
                    Run(dbContext, new ProcQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext, ProcedureNames = Database.ProcedureNames }, "Resolve oracle stored procedures");
                    Run(dbContext, new ProcQueryWithArgument_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext, ProcedureNames = Database.ProcedureNames }, "Resolve oracle stored procedures with arguments");
                    Run(dbContext, new TypeQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve oracle types");
                    //Run(dbContext, new ViewQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve oracle views");
                    Run(dbContext, new SynonymQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve oracle synonymes");
                    Run(dbContext, new GrantQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve database grants");
                    Run(dbContext, new TriggerQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve triggers");
                    Run(dbContext, new ContentCodeQuery_11() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve sources", dbContext.ExcludeCode);
                    //Run(dbContext, new TablespacesQuery() { OwnerNames = Database.OwnerNames, OracleContext = dbContext }, "Resolve sources", dbContext.ExcludeCode);
                    break;

                default:
                    throw new NotImplementedException(dbContext.Version.ToString());

            }

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
                    list.Add(item.ToUpper());
        }


    }


}

