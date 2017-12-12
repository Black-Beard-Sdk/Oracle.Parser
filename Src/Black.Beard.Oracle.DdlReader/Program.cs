using Bb.Beard.Oracle.Reader;
using Bb.Oracle.Models.Configurations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Black.Beard.Oracle.DdlReader
{
    class Program
    {

        static void Main(string[] args)
        {

            var ctx = new ArgumentContext(args);

            Func<string, bool> act = shema => { return true; };

            string connectionString = string.Format(@"Data source={0};USER ID={1};Password={2};", ctx.Source, ctx.Login, ctx.Pwd);

            Filtre(ctx, connectionString);

            Database.GenerateFile(ctx.Source, connectionString, ctx.Filename, act, ctx.ExcludeCode, ctx.Name);

            string directory = new FileInfo(ctx.Filename).Directory.FullName;

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
            {
                ExcludeSection.Configuration = ExcludeSection.LoadFile(ctx.ExcludeFile);
            }

            if (ctx.OwnerFilter == "*")
                ctx.OwnerFilter = ContextLoader.GetOwners(connectionString);

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
