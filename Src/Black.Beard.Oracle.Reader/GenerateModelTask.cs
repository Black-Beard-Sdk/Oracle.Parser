using Microsoft.Build.Utilities;
using Pssa.Tools.Databases.Generators.Configurations;
using Pssa.Tools.Databases.Generators.Helpers;
using Pssa.Tools.Databases.Generators.Queries;
using Pssa.Tools.Databases.Generators.Queries.Oracle;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Pssa.Tools.Databases.Generators
{
    /// <summary>
    /// 
    /// </summary>
    public class GenerateModelTask : Task
    {
        
        private DirectoryInfo Dir;

        public GenerateModelTask()
        {
            FileInfo f = new FileInfo(GetType().Assembly.Location);
            this.Dir = f.Directory;
        }

        /// <summary>
        /// Gets or sets the database schema config_ file name full path.
        /// </summary>
        /// <value>
        /// The database schema config_ file name full path.
        /// </value>
        [Required]
        public string DatabaseSchemaConfig_FileNameFullPath { get; set; }
        /// <summary>
        /// Gets or sets the database schema include specification_ file name full path.
        /// </summary>
        /// <value>
        /// The database schema include specification_ file name full path.
        /// </value>
        [Required]
        public string DatabaseSchemaIncludeSpecification_FileNameFullPath { get; set; }

        /// <summary>
        /// Gets or sets the project file.
        /// </summary>
        /// <value>
        /// The project file.
        /// </value>
        [Required]
        public string ConfigFile { get; set; }


        /// <summary>
        /// Gets or sets the name of the connection string.
        /// </summary>
        /// <value>
        /// The name of the connection string.
        /// </value>
        [Required]
        public string ConnectionStringName { get; set; }


        /// <summary>
        /// Lors d'une substitution dans une classe dérivée, exécute la tâche.
        /// </summary>
        /// <returns>
        /// true si la tâche s'est exécutée correctement ; sinon, false.
        /// </returns>
        /// <exception cref="System.Exception"></exception>
        public override bool Execute() 	
        {
            Log.LogMessage("Loading {0}", ConfigFile);

            var fileMap = new ExeConfigurationFileMap() { ExeConfigFilename = ConfigFile };
            var config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, System.Configuration.ConfigurationUserLevel.None);

            using (AppConfig.Change(ConfigFile))
            {

                #region get cnx string

                ConnectionStringSettings cnx = null;
                foreach (ConnectionStringSettings item in config.ConnectionStrings.ConnectionStrings)
                    if (item.Name == ConnectionStringName)
                    {
                        cnx = item;
                        break;
                    }

                if (cnx == null)
                    throw new Exception(string.Format("missing connection string name {0}", ConnectionStringName));

                #endregion get cnx string

                //string owner = ConfigurationManager.AppSettings["OWNER"];
                //string package = ConfigurationManager.AppSettings["PACKAGE"];
                //string proc = ConfigurationManager.AppSettings["PROCEDURE"];
                //string table = ConfigurationManager.AppSettings["TABLE"];

                DatabaseFiltersSection confDatabaseFilters = null;

                try
                {
                    AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
                    confDatabaseFilters = ConfigurationManager.GetSection("databaseFiltersSection") as DatabaseFiltersSection;
                    AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_AssemblyResolve;
                }
                catch
                {
                    //section cannot be found
                }

                if (confDatabaseFilters != null)
                {

                    var ownerLists = new List<String>();
                    for (var i = 0; i < confDatabaseFilters.Owners.Count; i++)
                    {
                        if (!String.IsNullOrWhiteSpace(confDatabaseFilters.Owners[i].value)) ownerLists.Add(confDatabaseFilters.Owners[i].value);
                    }
                    var packageLists = new List<String>();
                    for (var i = 0; i < confDatabaseFilters.Packages.Count; i++)
                    {
                        if (!String.IsNullOrWhiteSpace(confDatabaseFilters.Packages[i].value)) packageLists.Add(confDatabaseFilters.Packages[i].value);
                    }
                    var procLists = new List<String>();
                    for (var i = 0; i < confDatabaseFilters.Procedures.Count; i++)
                    {
                        if (!String.IsNullOrWhiteSpace(confDatabaseFilters.Procedures[i].value)) procLists.Add(confDatabaseFilters.Procedures[i].value);
                    }
                    var tableLists = new List<String>();
                    for (var i = 0; i < confDatabaseFilters.Tables.Count; i++)
                    {
                        if (!String.IsNullOrWhiteSpace(confDatabaseFilters.Tables[i].value)) tableLists.Add(confDatabaseFilters.Tables[i].value);
                    }

                    Database.OwnerNames = ownerLists.Count <= 0 ? null : ownerLists;
                    /// Selection d'un seul Package 
                    /// Selection d'un seul objet
                    Database.ProcedureNames = procLists.Count <= 0 ? null : procLists;
                    ///DbQueryBase.TableName = "PARCEL";
                    Database.TableNames = tableLists.Count <= 0 ? null : tableLists;
                }


                Func<string, bool> act =
                shema =>
                {
                    return true;
                };

                string file = DatabaseSchemaConfig_FileNameFullPath;

                AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

                Log.LogMessage("running with connection string name '{0}' -> '{1}'", cnx.Name, DatabaseSchemaIncludeSpecification_FileNameFullPath);

                try
                {
                    Database.GenerateFile("source", cnx.ConnectionString, file, act);
                }
                finally
                {
                    AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_AssemblyResolve;
                }

                Log.LogMessage("Writing {0}", @DatabaseSchemaIncludeSpecification_FileNameFullPath);
                var packages = Pssa.Tools.Databases.Generators.Helpers.IncludeFileParser.GetIncludes(@DatabaseSchemaIncludeSpecification_FileNameFullPath);
            }
            return true;

        }

        System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {

            var file = this.Dir.GetFiles(args.Name + ".dll").FirstOrDefault();

            if (file == null)
                throw new Exception(string.Format("missing assembly {0}. please copy missing assembly in the folder {1}", args.Name, Dir.FullName));

            Log.LogMessage("{0} resolved", args.Name);

            return Assembly.LoadFile(file.FullName);

        }


    }
}
