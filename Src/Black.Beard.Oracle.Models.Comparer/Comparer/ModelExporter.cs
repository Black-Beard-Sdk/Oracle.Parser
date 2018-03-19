using Bb.Oracle.Helpers;
using Bb.Oracle.Models;
using Bb.Oracle.Structures.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Oracle.Models.Comparer
{

    public class ModelExporter : DifferenceModels
    {

        private readonly string rootFolderSource;
        private HashSet<object> _buckets = new HashSet<object>();

        public ModelExporter(string folderForSource, string folderForTarget, string rootFolderSource, Action<string> log)
            : base(folderForSource, folderForTarget, log)
        {

            this.rootFolderSource = rootFolderSource;
        }

        #region Missing

        public override void AppendMissing(SequenceModel source)
        {
            if (_buckets.Add(source))
            {
                var d = base.AppendDifference(source, false);
                Append(source, d);
            }
        }

        public override void AppendMissing(PackageModel source)
        {
            if (_buckets.Add(source))
            {
                var d = base.AppendDifference(source, false);
                Append(source, d, null);
            }
        }

        public override void AppendMissing(ProcedureModel source)
        {
            var d = base.AppendDifference(source, false);
            Append(source, d, null);
        }

        public override void AppendMissing(GrantModel source, OracleDatabase targetModel)
        {
            if (_buckets.Add(source))
            {
                var d = base.AppendDifference(source, false);
                Append(source, d);
            }
        }

        public override void AppendMissing(ArgumentModel argSource, ProcedureModel source, ProcedureModel target)
        {
            if (_buckets.Add(source))
            {
                var d = base.AppendDifference(source, false);
                Append(source, d, target);
            }
        }

        public override void AppendMissing(ColumnModel source)
        {
            if (_buckets.Add(source))
            {
                var d = base.AppendDifference(source, false);
            }
        }

        public override void AppendMissing(ConstraintModel source)
        {
            if (_buckets.Add(source))
            {
                var d = base.AppendDifference(source, false);
            }
        }

        public override void AppendMissing(IndexModel source)
        {
            if (_buckets.Add(source))
            {
                var d = base.AppendDifference(source, false);
            }
        }

        public override void AppendMissing(PropertyModel source)
        {
            if (_buckets.Add(source))
            {
                var d = base.AppendDifference(source, false);
                Append(source.Parent as TypeItem, d);
            }
        }

        public override void AppendMissing(SynonymModel source)
        {
            if (_buckets.Add(source))
            {
                var d = base.AppendDifference(source, false);
                Append(source, d);
            }
        }

        public override void AppendMissing(TableModel source)
        {
            if (_buckets.Add(source))
            {
                var d = base.AppendDifference(source, false);
                Append(source, d);
            }
        }

        public override void AppendMissing(TriggerModel source)
        {
            if (_buckets.Add(source))
            {
                var d = base.AppendDifference(source, false);
                Append(source, d);
            }
        }

        public override void AppendMissing(TypeItem source)
        {
            if (_buckets.Add(source))
            {
                var d = base.AppendDifference(source, false);
                Append(source, d);
            }
        }

        #endregion Missing


        #region Changes


        public override void AppendChange(ArgumentModel argSource, ArgumentModel argTarget, string propertyName, ProcedureModel source, ProcedureModel target)
        {
            if (_buckets.Add(source))
            {
                var d = base.AppendDifference(source, target, propertyName);
                Append(source, d, target);
            }
        }

        public override void AppendChange(ColumnModel source, ColumnModel target, string propertyName)
        {
            if (_buckets.Add(source))
            {
                var d = base.AppendDifference(source, target, propertyName);
            }

        }

        public override void AppendChange(ConstraintModel source, ConstraintModel target, string propertyName)
        {
            if (_buckets.Add(source))
            {
                var d = base.AppendDifference(source, target, propertyName);
            }

        }

        public override void AppendChange(GrantModel source, GrantModel target, string propertyName)
        {
            if (_buckets.Add(source))
            {
                var d = base.AppendDifference(source, target, propertyName);
                Append(source, d);
            }
        }

        public override void AppendChange(IndexModel source, IndexModel target, string propertyName, string columnsources = "", string columntarget = "")
        {
            if (_buckets.Add(source))
            {
                var d = base.AppendDifference(source, target, propertyName);
            }
        }

        public override void AppendChange(PackageModel source, PackageModel target, string propertyName)
        {
            if (_buckets.Add(source + propertyName))
            {
                var d = base.AppendDifference(source, target, propertyName);
                Append(source, d, target);
            }
        }

        public override void AppendChange(ProcedureModel source, ProcedureModel target, string propertyName)
        {
            if (_buckets.Add(source))
            {
                var d = base.AppendDifference(source, target, propertyName);
                Append(source, d, target);
            }
        }

        public override void AppendChange(PropertyModel source, PropertyModel target, string propertyName)
        {
            if (_buckets.Add(source))
            {
                var d = base.AppendDifference(source, target, propertyName);
                Append(source.Parent as GrantModel, d);
            }
        }

        public override void AppendChange(SequenceModel source, SequenceModel target, string propertyName)
        {
            if (_buckets.Add(source))
            {
                base.AppendChange(source, target, propertyName);
            }
        }

        public override void AppendChange(SynonymModel source, SynonymModel target, string propertyName)
        {
            if (_buckets.Add(source))
            {
                var d = base.AppendDifference(source, target, propertyName);
                Append(source, d);
            }
        }

        public override void AppendChange(TableModel source, TableModel target, string propertyName)
        {
            if (_buckets.Add(source))
            {
                var d = base.AppendDifference(source, target, propertyName);
                if (source.IsView)
                {
                    Append(source, d);
                }
                else
                {

                }
            }
        }

        public override void AppendChange(TriggerModel source, TriggerModel target, string propertyName)
        {
            if (_buckets.Add(source))
            {
                var d = base.AppendDifference(source, target, propertyName);
                Append(source, d);
            }
        }

        public override void AppendChange(TypeItem source, TypeItem target, string propertyName)
        {
            if (_buckets.Add(source))
            {
                var d = base.AppendDifference(source, target, propertyName);
                Append(source, d);
            }
        }

        #endregion Changes


        private void Append(GrantModel source, DifferenceModel d)
        {
            string p = BuildPath(Path.Combine(this.folderForTarget, source.ObjectSchema), "UserObjectPrivileges", source.Role);
            StringBuilder sb = new StringBuilder();
            string sql = string.Format(@"GRANT {0} ON {1} TO {2}{3};", string.Join(", ", source.Privileges), source.FullObjectName.Replace(@"""", ""), source.Role, source.Grantable ? " WITH GRANT OPTION" : string.Empty);
            sb.AppendLine(sql);

            if (sb.Length > 0)
            {
                WriteFile(p, sb.ToString());
                d.Addfile(p);
            }

        }

        private void Append(SequenceModel source, DifferenceModel d)
        {
            string p = BuildPath(Path.Combine(this.folderForTarget, source.Owner), "Sequences", source.Key);

            if (!File.Exists(p))
            {
                d.Addfile(p);
                StringBuilder sb = new StringBuilder();

                var file = source.Files.OfType<FileElement>().FirstOrDefault();

                if (file != null && file.Exist(this.rootFolderSource))
                    sb = new StringBuilder(Helpers.ContentHelper.LoadContentFromFile(this.rootFolderSource, file.Path));

                else
                {

                }

                if (sb.Length > 0)
                {
                    WriteFile(p, sb.ToString());
                    d.Addfile(p);
                }
            }

        }

        private void Append(TableModel source, DifferenceModel d)
        {

            string typeObject = "Tables";

            if (source.IsMatrializedView)
                typeObject = "MaterializedViews";

            else if (source.IsView)
                typeObject = "Views";
            else
                typeObject = "Tables";

            string p = BuildPath(Path.Combine(this.folderForTarget, source.Owner), typeObject, source.Name);

            if (!File.Exists(p))
            {
                StringBuilder sb = new StringBuilder();

                var file = source.Files.OfType<FileElement>().FirstOrDefault();

                if (file != null && file.Exist(this.rootFolderSource))
                    sb.Append(Helpers.ContentHelper.LoadContentFromFile(this.rootFolderSource, file.Path));

                else
                {
                    if (source.IsView)
                    {
                        sb.Append(Utils.Unserialize(source.codeView, true));
                    }
                    else
                    {
                        // generer le script de la table
                    }
                }

                if (sb.Length > 0)
                {
                    WriteFile(p, sb.ToString());
                    d.Addfile(p);
                }

                // dans le cas ou la source est une instance de base

                //      Gerer les commentaires qui sont sur les tables

                //      Gerer les trigers qui sont sur les tables

                //      Gerer les index qui sont sur les tables

                //      Gerer les contraints qui sont sur les tables 

            }
        }

        private void Append(TriggerModel source, DifferenceModel d)
        {
            string p = BuildPath(Path.Combine(this.folderForTarget, source.Owner), "Triggers", source.Name);
            StringBuilder sb = new StringBuilder();
            if (!File.Exists(p))
            {
                var file = source.Files.OfType<FileElement>().FirstOrDefault();
                if (file != null && file.Exist(this.rootFolderSource))
                    sb = new StringBuilder(Helpers.ContentHelper.LoadContentFromFile(this.rootFolderSource, file.Path));

                else
                {
                    sb = new StringBuilder(Helpers.ContentHelper.LoadContentFromFile(this.rootFolderSource, file.Path));
                    sb.Append(CreateOrReplace);
                    sb.Append(Utils.Unserialize(source.Code, true));
                }

                if (sb.Length > 0)
                {
                    WriteFile(p, sb.ToString());
                    d.Addfile(p);
                }
            }

        }

        private void Append(SynonymModel source, DifferenceModel d)
        {
            string p = BuildPath(Path.Combine(this.folderForTarget, source.ObjectTargetOwner), "Synonyms", source.Name);
            StringBuilder sb = new StringBuilder();

            if (!File.Exists(p))
            {
                var file = source.Files.OfType<FileElement>().FirstOrDefault();
                if (file != null && file.Exist(this.rootFolderSource))
                    sb = new StringBuilder(Helpers.ContentHelper.LoadContentFromFile(this.rootFolderSource, file.Path));

                else
                    sb.Append(string.Format("CREATE OR REPLACE {0} SYNONYM {1}.{2} FOR {3};", "PUBLIC", source.ObjectTargetOwner, source.Name, source.ObjectTargetName));

                if (sb.Length > 0)
                {
                    WriteFile(p, sb.ToString());
                    d.Addfile(p);
                }
            }
        }

        private void Append(TypeItem source, DifferenceModel d)
        {
            string p = BuildPath(Path.Combine(this.folderForTarget, source.Owner), "Types", source.Name);
            StringBuilder sb = new StringBuilder();

            if (!File.Exists(p))
            {

                var file = source.Files.OfType<FileElement>().FirstOrDefault();
                if (file != null && file.Exist(this.rootFolderSource))
                    sb = new StringBuilder(Helpers.ContentHelper.LoadContentFromFile(this.rootFolderSource, file.Path));

                else
                {
                    sb = new StringBuilder(Helpers.ContentHelper.LoadContentFromFile(this.rootFolderSource, file.Path));
                    sb.Append(CreateOrReplace);
                    sb.Append(source.Code.GetSource());
                    sb.AppendLine("\r\n");
                    sb.Append(source.CodeBody.GetSource());
                }

                if (sb.Length > 0)
                {
                    WriteFile(p, sb.ToString());
                    d.Addfile(p);
                }

            }
        }

        private void Append(ProcedureModel source, DifferenceModel d, ProcedureModel target)
        {

            string p = BuildPath(Path.Combine(this.folderForTarget, source.Owner), "Procedures", source.Name);
            StringBuilder sb = new StringBuilder();

            if (!File.Exists(p))
            {

                var txt = CreateOrReplace + Utils.Unserialize(source.Code, true).Trim();
                if (!txt.EndsWith(@"\"))
                    txt = txt + Environment.NewLine + @"\";
                sb.AppendLine(txt);

                if (sb.Length > 0)
                {
                    WriteFile(p, sb.ToString());
                    d.Addfile(p);
                }

            }

            if (target != null)
            {

                p = BuildPath(Path.Combine(this.folderForTarget, source.Owner), "Procedures", source.Name, true);
                sb = new StringBuilder();

                if (!File.Exists(p))
                {

                    var txt = CreateOrReplace + Utils.Unserialize(target.Code, true).Trim();
                    if (!txt.EndsWith(@"\"))
                        txt = txt + Environment.NewLine + @"\";
                    sb.AppendLine(txt);

                    if (sb.Length > 0)
                    {
                        WriteFile(p, sb.ToString());
                        d.Addfile(p);
                    }

                }



            }

        }

        private void Append(PackageModel source, DifferenceModel d, PackageModel target)
        {

            string p = BuildPath(Path.Combine(this.folderForTarget, source.Owner), "PackageBodies", source.Name);

            if (d.PropertyName == "CodeBody")
            {
                if (!File.Exists(p))
                {
                    string txt = CreateOrReplace + source.CodeBody.GetSource().Trim();
                    if (!txt.EndsWith(@"\"))
                        txt = txt+ Environment.NewLine + @"\";
                    WriteFile(p, txt);
                    d.Addfile(p);
                }
            }
            else
            {
                p = BuildPath(Path.Combine(this.folderForTarget, source.Owner), "Packages", source.Name);
                if (!File.Exists(p))
                {
                    string txt = CreateOrReplace + source.Code.GetSource().Trim();
                    if (!txt.EndsWith(@"\"))
                        txt = txt + Environment.NewLine + @"\";
                    WriteFile(p, txt);
                    d.Addfile(p);
                }
            }

            if (target != null)
            {

                if (d.PropertyName == "CodeBody")
                {
                    p = BuildPath(Path.Combine(this.folderForTarget, target.Owner), "PackageBodies", target.Name, true);
                    if (!File.Exists(p))
                    {
                        string txt = CreateOrReplace + target.CodeBody.GetSource().Trim();
                        if (!txt.EndsWith(@"\"))
                            txt = txt + Environment.NewLine + @"\"; WriteFile(p, txt);
                        d.Addfile(p);
                    }
                }
                else
                {
                    p = BuildPath(Path.Combine(this.folderForTarget, target.Owner), "Packages", target.Name, true);
                    if (!File.Exists(p))
                    {
                        string txt = CreateOrReplace + target.Code.GetSource().Trim();
                        if (!txt.EndsWith(@"\"))
                            txt = txt + Environment.NewLine + @"\"; WriteFile(p, txt);
                        d.Addfile(p);
                    }
                }
            }

        }


        public void GenerateRunner(string source, bool isRollBack)
        {

            HashSet<string> _doublons = new HashSet<string>();
            var lst = this.Changes.ToList();
            StringBuilder sb = new StringBuilder(1024 * 10);

            string[] jobs = new string[] { };

            Initialise(sb, source, jobs);
            string cmd = @"START ""@@{0}"";";
            string outputPath = isRollBack
                ? Path.Combine(this.folderForTarget, "runner_back.sql")
                : Path.Combine(this.folderForTarget, "runner.sql");

            if (File.Exists(outputPath))
                File.Delete(outputPath);

            Func<DifferenceModel, string> o = c =>
            {
                var ii = c.Source as ItemBase;
                if (ii != null)
                    return ii.GetOwner();

                var s2 = (c.Source as DoublonModel).Source as ItemBase;
                if (s2 != null)
                    return s2.GetOwner();

                return "";
            };

            sb.AppendLine("\r\nPROMPT Append missings tables");
            Append(lst, sb, cmd, lst.Where(c => c.Source is TableModel && c.Kind == TypeDifferenceEnum.MissingInTarget).OrderBy(o).ToList(), _doublons, isRollBack);

            sb.AppendLine("\r\nPROMPT Append changed tables");
            AppendChangedTable(lst, sb, cmd, lst.Where(c => c.Source is TableModel).OrderBy(o).ToList(), _doublons, isRollBack);

            sb.AppendLine("\r\nPROMPT Append columns");
            Append(lst, sb, cmd, lst.Where(c => c.Source is ColumnModel).OrderBy(o).ToList(), _doublons, isRollBack);

            sb.AppendLine("\r\nPROMPT Append indexes");
            Append(lst, sb, cmd, lst.Where(c => c.Source is IndexModel).OrderBy(o).ToList(), _doublons, isRollBack);

            sb.AppendLine("\r\nPROMPT Append triggers");
            Append(lst, sb, cmd, lst.Where(c => c.Source is TriggerModel).OrderBy(o).ToList(), _doublons, isRollBack);

            sb.AppendLine("\r\nPROMPT Append sequences");
            Append(lst, sb, cmd, lst.Where(c => c.Source is SequenceModel).OrderBy(o).ToList(), _doublons, isRollBack);

            sb.AppendLine("\r\nPROMPT Append types");
            Append(lst, sb, cmd, lst.Where(c => c.Source is TypeItem).OrderBy(o).ToList(), _doublons, isRollBack);

            sb.AppendLine("\r\nPROMPT Append packages");
            Append(lst, sb, cmd, lst.Where(c => c.Source is PackageModel).OrderBy(o).ToList(), _doublons, isRollBack);

            sb.AppendLine("\r\nPROMPT Append procedures");
            Append(lst, sb, cmd, lst.Where(c => c.Source is ProcedureModel).OrderBy(o).ToList(), _doublons, isRollBack);

            sb.AppendLine("\r\nPROMPT Append synonyms");
            Append(lst, sb, cmd, lst.Where(c => c.Source is SynonymModel).OrderBy(o).ToList(), _doublons, isRollBack);

            sb.AppendLine("\r\nPROMPT Append constraints");
            Append(lst, sb, cmd, lst.Where(c => c.Source is ConstraintModel).OrderBy(o).ToList(), _doublons, isRollBack);

            sb.AppendLine("\r\nPROMPT Append grants");
            Append(lst, sb, cmd, lst.Where(c => c.Source is GrantModel).OrderBy(o).ToList(), _doublons, isRollBack);

            //sb.AppendLine("\r\nPROMPT Append duplicate objects");
            //AppendDuplicateObject(lst, sb, cmd, lst.Where(c => c.Source is DoublonModel).OrderBy(o).ToList(), _doublons);

            //if (lst.Any())
            //{
            //    if (System.Diagnostics.Debugger.IsAttached)
            //        System.Diagnostics.Debugger.Break();

            //    sb.AppendLine("\r\nPROMPT Append others");
            //    Append(lst, sb, cmd, lst.OrderBy(o).ToList(), _doublons, isRollBack);
            //}

            Close(sb, jobs);

            WriteFile(outputPath, sb.ToString());

        }

        private bool IsRollBack(DifferenceModel c, bool isRollBack)
        {

            return true;

        }

        private void Initialise(StringBuilder sb, string source, string[] jobs)
        {

            sb.AppendLine("-- ********************************************************** --");
            sb.AppendLine("-- Author  : Auto generated");
            sb.AppendLine(string.Format("-- Created : {0}", DateTime.Now.ToString("f")));
            sb.AppendLine(string.Format("-- Base : {0}", source));
            sb.AppendLine("-- ********************************************************** -- ");
            sb.AppendLine("-- ********************************************************** --");
            sb.AppendLine("-- Generate Log file for debug ");
            sb.AppendLine("-- ********************************************************** --");
            sb.AppendLine("SET ECHO OFF");
            sb.AppendLine("SET SERVEROUTPUT ON ");
            sb.AppendLine("SET FEEDBACK ON ");
            sb.AppendLine("SET HEADING OFF");
            sb.AppendLine("SET VERIFY ON ");
            sb.AppendLine("SET SQLBLANKLINES ON ");
            sb.AppendLine("SET DEFINE ON");
            sb.AppendLine("--Add date to file name");
            sb.AppendLine("COL TS NEW_VALUE TS NOPRI ");
            sb.AppendLine("COL NOMENV NEW_VALUE NOMENV NOPRI ");
            sb.AppendLine("COL NOMLIV NEW_VALUE NOMLIV NOPRI ");

            sb.AppendLine(string.Empty);
            sb.AppendLine("SELECT TO_CHAR(SYSDATE,'YYYYMMDDHH24MISS') TS FROM DUAL;");
            //sb.AppendLine("-- ********************************************************** --");
            //sb.AppendLine("-- Directories");
            //sb.AppendLine("-- ********************************************************** --");
            //sb.AppendLine(@"--SELECT '\01_Recette' NOMENV FROM DUAL;                                                   --on décommente l’environnement souhaité…");
            //sb.AppendLine(@"--SELECT '\02_Integration' NOMENV FROM DUAL;");
            //sb.AppendLine(@"--SELECT '\03_Preproduction' NOMENV FROM DUAL;");
            //sb.AppendLine(@"--SELECT '\04_Production' NOMENV FROM DUAL;");

            //sb.AppendLine(string.Empty);
            //sb.AppendLine(@"--SELECT '\LIV_20150522_MCO_R5S8_TEST' NOMLIV FROM DUAL;           --on nomme le dossier livré…");

            sb.AppendLine(string.Empty);
            sb.AppendLine(@"spool "".\logs\01_LANCEUR_1_ & TS..log""");

            sb.AppendLine(string.Empty);
            sb.AppendLine("-- ********************************************************** --");
            sb.AppendLine("-- -- get the invalid object before execute the script");
            sb.AppendLine("-- ********************************************************** --");
            sb.AppendLine("--SELECT substr(c.OWNER,1,20),substr(c.OBJECT_NAME,1,30),substr(c.OBJECT_TYPE,1,25)  FROM dba_objects c WHERE c.status != 'VALID' ORDER BY c.owner,c.object_type;");
            sb.AppendLine("");

            if (jobs.Length > 0)
            {
                sb.AppendLine(string.Empty);
                foreach (var job in jobs)
                {
                    sb.AppendLine("-- ********************************************************** --");
                    sb.AppendLine("-- 	stop jobs:");
                    sb.AppendLine("--  ********************************************************** --");
                    sb.AppendLine(string.Format(@"exec dbms_scheduler.DISABLE('{0}',TRUE);", job));
                    sb.AppendLine("-- ********************************************************** --");
                }
                sb.AppendLine(string.Empty);
            }

            //sb.AppendLine("-- ********************************************************** --");
            //sb.AppendLine("-- Script");
            //sb.AppendLine("-- ********************************************************** --");
            //sb.AppendLine("PROMPT --------------------------------");
            //sb.AppendLine("PROMPT  DATA STRUCTURE & DATA MANAGEMENT");
            //sb.AppendLine("PROMPT --------------------------------");
            //sb.AppendLine(@"@"".\source\ManualSending.sql"";");
            //sb.AppendLine(@"@"".\source\MissingContactData.sql"";");

        }

        private void Close(StringBuilder sb, string[] jobs)
        {

            sb.AppendLine("--");
            sb.AppendLine("-- ********************************************************** --");
            sb.AppendLine("-- -- get the invalid object after execute the script");
            sb.AppendLine("-- ********************************************************** --");
            sb.AppendLine("--SELECT substr(c.OWNER,1,20),substr(c.OBJECT_NAME,1,30),substr(c.OBJECT_TYPE,1,25)  FROM dba_objects c WHERE c.status != 'VALID' ORDER BY c.owner,c.object_type;");

            if (jobs.Length > 0)
            {
                foreach (var job in jobs)
                {
                    sb.AppendLine("--  ********************************************************** --");
                    sb.AppendLine("-- 	Restart jobs:");
                    sb.AppendLine("--  ********************************************************** --");
                    sb.AppendLine(string.Format(@"exec dbms_scheduler.ENABLE('{0}');", job));
                    sb.AppendLine("-- ********************************************************** --");
                }
            }

            sb.AppendLine("--  ********************************************************** --");
            sb.AppendLine("-- 	Stop Spool of log:");
            sb.AppendLine("--  ********************************************************** --  ");
            sb.AppendLine("spool OFF");
            sb.AppendLine("/");

        }

        private void AppendDuplicateObject(List<DifferenceModel> lst, StringBuilder sb, string cmd, List<DifferenceModel> items, HashSet<string> _doublons)
        {
            foreach (var item in items)
            {

                lst.Remove(item);

            }
        }

        private static void Append(List<DifferenceModel> lst, StringBuilder sb, string cmd, IEnumerable<DifferenceModel> items, HashSet<string> _doublons, bool isRollback)
        {

            sb.AppendLine("PROMPT --------------------------------");
            Func<FileElement, bool> f;
            if (isRollback)
                f = c => c.Path.ToUpper().EndsWith(".BACK.SQL");
            else
                f = c => !c.Path.ToUpper().EndsWith(".BACK.SQL");

            foreach (var item in items)
            {
                foreach (var file in item.Files.Where(f))
                    if (_doublons.Add(file.Path))
                        sb.AppendLine(string.Format(cmd, file));
                lst.Remove(item);
            }

        }

        private static void AppendChangedTable(List<DifferenceModel> lst, StringBuilder sb, string cmd, IEnumerable<DifferenceModel> items, HashSet<string> _doublons, bool isRollback)
        {

            Func<FileElement, bool> f;
            if (isRollback)
                f = c => c.Path.ToUpper().EndsWith(".BACK.SQL");
            else
                f = c => !c.Path.ToUpper().EndsWith(".BACK.SQL");

            foreach (var item in items)
            {
                foreach (var file in item.Files.Where(f))
                    if (_doublons.Add(file.Path))
                        sb.AppendLine("REM " + string.Format(cmd, file));
                lst.Remove(item);

            }
        }

    }

}
