using Bb.Oracle.Models.Comparer;
using Bb.Oracle.Structures.Models;
using CompareModel;
using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;

namespace Exe.CompareModel
{

    /*
     
        -source "F:\test.oracle.models\ora_dev_v.config" -target "F:\test.oracle.models\tfs_dev_v.config" -output "F:\test.oracle.models\rapports\Compare_ora_dev_v_2_tfs_dev_v.xls" -TfsProjectName "PLSQL" 
        //-pathSource "$/PLSQL/Pickup/dev-v/Schemas" -RuleFilename "Configurations\rules.config"


     */

    class Program
    {

        static StringBuilder OutputDbSource = new StringBuilder();
        static StringBuilder OutputDbTarget = new StringBuilder();

        public static void Main(string[] args)
        {

            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");

            var argCxt = new ArgumentContext(args);
            bool withoutTfs = false;

            if (!File.Exists(argCxt.Source))
                throw new Exception(string.Format("missing source file {0}", argCxt.Source));

            if (!File.Exists(argCxt.Target))
                throw new Exception(string.Format("missing target file {0}", argCxt.Target));

            Console.WriteLine("loading source " + argCxt.Source);
            var source = OracleDatabase.ReadFile(argCxt.Source);
            OutputDbSource.AppendLine("-- update for " + argCxt.Source);
            AddSupport(source);

            Console.WriteLine("loading target " + argCxt.Target);
            var target = OracleDatabase.ReadFile(argCxt.Target);
            AddSupport(target);

            OutputDbTarget.AppendLine("-- compare " + argCxt.Source + " to " + argCxt.Target);
            Console.WriteLine(string.Format("compare " + argCxt.Source + " to " + argCxt.Target));

            var sf = new FileInfo(argCxt.Output);
            string folderForSource = Path.Combine(sf.Directory.FullName, Path.GetFileNameWithoutExtension(sf.Name), Path.GetFileNameWithoutExtension(argCxt.Source));
            string folderForTarget = Path.Combine(sf.Directory.FullName, Path.GetFileNameWithoutExtension(sf.Name), Path.GetFileNameWithoutExtension(argCxt.Target));

            ModelComparer comparer = new ModelComparer();
            DifferenceModels diff = new DifferenceModels(folderForSource, folderForTarget, c => Console.WriteLine(c));
            comparer.CompareModels(source, target, diff, new CompareContext() { });

            Console.WriteLine(string.Empty);
            Console.WriteLine("fin de la comparaison");
            Console.WriteLine(string.Empty);

            ProcessorExcel proc = new ProcessorExcel(withoutTfs, argCxt.UrlTfs, source.SourceScript, target.SourceScript, argCxt.TfsProjectName)
            {
                EnvironmentSource = source.Name,
                EnvironmentTarget = target.Name,
            };

            proc.Run(diff, argCxt.PathSource, argCxt.PathTarget);
            DeleteFile(sf);
            proc.Save(sf.FullName);

        }

        //private static ResponsabilitiesSection CheckArguments(ArgumentContext argCxt, ref bool withoutTfs)
        //{

        //    ResponsabilitiesSection section = null;

        //    if (!string.IsNullOrEmpty(argCxt.ConfigApplication))
        //    {
        //        Console.WriteLine("Load responsabilities configuration " + argCxt.ConfigApplication);
        //        section = ResponsabilitiesSection.ReadFile(argCxt.ConfigApplication);
        //    }
        //    else
        //        Console.WriteLine("no responsabilities configuration loeaded");

        //    if (string.IsNullOrEmpty(argCxt.Source))
        //        throw new InvalidOperationException("no input file specified. please specify argument 'source'");

        //    if (string.IsNullOrEmpty(argCxt.Target))
        //        throw new InvalidOperationException("no target file specified. please specify argument 'target'");

        //    if (string.IsNullOrEmpty(argCxt.Output))
        //    {
        //        BuildOutputFilename(argCxt);
        //        Console.WriteLine("output in {0}", argCxt.Output);
        //    }

        //    if (string.IsNullOrEmpty(argCxt.UrlTfs))
        //        argCxt.UrlTfs = @"http://alm:8080/tfs/WebTeamCollection";

        //    if (string.IsNullOrEmpty(argCxt.TfsProjectName) || string.IsNullOrEmpty(argCxt.TfsProjectName) || string.IsNullOrEmpty(argCxt.UrlTfs))
        //    {
        //        withoutTfs = true;
        //        Console.WriteLine("Launched without Tfs feature");
        //    }
        //    else
        //        Console.WriteLine("Launched with Tfs feature");

        //    argCxt.UrlTfs = argCxt.UrlTfs.Trim('/');

        //    if (string.IsNullOrEmpty(argCxt.PathTarget))
        //        argCxt.PathTarget = argCxt.PathSource;

        //    return section;

        //}

        private static void DeleteFile(FileInfo sf)
        {
            try
            {
                if (sf.Exists)
                    sf.Delete();
            }
            catch (Exception)
            {
                Console.WriteLine("Can't access to file '{0}'. please close and press 'R' to retry", sf.Name);
                string r = Console.ReadLine();
                r = r.Trim().ToUpper();
                if (r == "R")
                    DeleteFile(sf);
                else
                    throw;
            }
        }

        private static void BuildOutputFilename(ArgumentContext argCxt)
        {
            var n = DateTime.Now;
            var f1 = new FileInfo(argCxt.Source);
            string sourceName = f1.Name.Substring(0, f1.Name.Length - f1.Extension.Length);
            f1 = new FileInfo(argCxt.Target);
            string targetName = f1.Name.Substring(0, f1.Name.Length - f1.Extension.Length);
            argCxt.Output = string.Format("{0}_2_{1}___{2}_{3}h{4}.xls", sourceName, targetName, n.ToString("M"), n.Hour, n.Minute);

            argCxt.Output = Path.Combine(Environment.CurrentDirectory, "Rapports", argCxt.Output);

            var f2 = new FileInfo(argCxt.Output);
            if (!f2.Directory.Exists)
                f2.Directory.Create();


        }

        private static void AddSupport(OracleDatabase source)
        {

            if (!source.SourceScript)
            {

                var p = "Database " + source.Name;

                foreach (GrantModel item in source.Grants)
                    item.Files.AddIfNotExist(new FileElement() { Path = p });

                foreach (PackageModel item in source.Packages)
                    item.Files.AddIfNotExist(new FileElement() { Path = p });

                foreach (PartitionModel item in source.Partitions)
                    item.Files.AddIfNotExist(new FileElement() { Path = p });

                foreach (ProcedureModel item in source.Procedures)
                    item.Files.AddIfNotExist(new FileElement() { Path = p });

                foreach (SequenceModel item in source.Sequences)
                    item.Files.AddIfNotExist(new FileElement() { Path = p });

                foreach (SynonymModel item in source.Synonyms)
                    item.Files.AddIfNotExist(new FileElement() { Path = p });

                foreach (TableModel item in source.Tables)
                {

                    item.Files.Add(new FileElement() { Path = p });
                    foreach (ColumnModel item2 in item.Columns)
                    {
                        item2.Files.AddIfNotExist(new FileElement() { Path = p });
                        //foreach (var item3 in item2.Constraints)
                        //{
                        //    item3.Files.AddIfNotExist(new FileElement() { Path = p });
                        //    foreach (ConstraintColumnModel item4 in item3.Columns)
                        //        item4.Files.AddIfNotExist(new FileElement() { Path = p });
                        //}
                    }

                    foreach (PartitionRefModel item2 in item.Partitions)
                    {
                        item2.Files.AddIfNotExist(new FileElement() { Path = p });
                        item2.Partition.Files.AddIfNotExist(new FileElement() { Path = p });
                    }

                }

                foreach (TriggerModel item2 in source.Triggers)
                    item2.Files.AddIfNotExist(new FileElement() { Path = p });

                foreach (ConstraintModel item2 in source.Constraints)
                {
                    item2.Files.AddIfNotExist(new FileElement() { Path = p });
                    foreach (ConstraintColumnModel item3 in item2.Columns)
                        item3.Files.AddIfNotExist(new FileElement() { Path = p });
                }

                foreach (IndexModel item2 in source.Indexes)
                {
                    item2.Files.AddIfNotExist(new FileElement() { Path = p });
                    foreach (IndexColumnModel item3 in item2.Columns)
                        item3.Files.AddIfNotExist(new FileElement() { Path = p });
                }

                foreach (TablespaceModel item in source.Tablespaces)
                    item.Files.AddIfNotExist(new FileElement() { Path = p });

                foreach (TypeItem item in source.Types)
                    item.Files.AddIfNotExist(new FileElement() { Path = p });

            }


        }
    }


}
