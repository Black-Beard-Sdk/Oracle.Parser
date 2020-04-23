using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Bb.Oracle.Models;
using Bb.Oracle.Contracts;
using Bb.Oracle.Structures.Models;
using System.Diagnostics;

namespace Bb.Oracle.Models.Comparer
{

    public class DifferenceModels
    {

        protected const string CreateOrReplace = "CREATE OR REPLACE ";

        protected bool generateSource = false;
        protected bool generateTarget = false;
        private Action<string> log;

        private List<DifferenceModel> _lst = new List<DifferenceModel>();
        protected string folderForSource;
        protected string folderForTarget;
        private HashSet<string> _grants = new HashSet<string>();

        public IEnumerable<DifferenceModel> Changes { get { return this._lst; } }

        public DifferenceModels(string folderForSource, string folderForTarget, Action<string> log)
        {

            this.log = log;
            this.folderForSource = folderForSource;
            this.folderForTarget = folderForTarget;

            if (!string.IsNullOrEmpty(this.folderForSource))
            {

                DirectoryInfo dirSource = new DirectoryInfo(this.folderForSource);

                if (!dirSource.Parent.Exists)
                    dirSource.Parent.Create();

                if (dirSource.Exists)
                    Delete(dirSource);

                dirSource.Refresh();

                if (!dirSource.Exists)
                    dirSource.Create();

                generateSource = true;

            }

            if (!string.IsNullOrEmpty(this.folderForTarget))
            {
                DirectoryInfo dirtarget = new DirectoryInfo(this.folderForTarget);

                if (dirtarget.Exists)
                    Delete(dirtarget);

                dirtarget.Refresh();

                if (!dirtarget.Exists)
                    dirtarget.Create();

                generateTarget = true;
            }

        }

        public IEnumerable<DifferenceModel> Items { get { return this._lst; } }

        private void Delete(DirectoryInfo dirSource)
        {

            dirSource.Refresh();

            foreach (var file in dirSource.GetFiles())
                file.Delete();

            foreach (var dir in dirSource.GetDirectories())
                Delete(dir);

            dirSource.Delete();

        }

        public virtual void AppendMissing(TypeItem source)
        {

            AppendDifference(source, false);

            if (generateSource)
            {
                if (string.IsNullOrEmpty(source.PackageName))
                {
                    int length
                        = source.Code.Length
                        + source.CodeBody.Length
                        + 100;

                    StringBuilder sb = new StringBuilder(length);
                    if (!source.Code.IsEmpty)
                    {
                        sb.AppendLine(CreateOrReplace + source.Code.GetSource());
                        sb.AppendLine(string.Empty);
                        sb.AppendLine("/");
                    }
                    if (!source.CodeBody.IsEmpty)
                        sb.AppendLine(CreateOrReplace + source.CodeBody.GetSource());

                    string p = BuildPath(Path.Combine(this.folderForSource, source.Owner), "Types", source.Name);
                    WriteFile(p, sb.ToString());
                }
            }

        }

        public virtual void AppendMissing(SynonymModel source)
        {

            AppendDifference(source, false);

            if (generateSource)
            {

                string p = BuildPath(Path.Combine(this.folderForSource, source.ObjectTargetOwner), "Synonyms", source.Name);

                if (!File.Exists(p))
                {

                    string sql = string.Empty;
                    if (source.Owner == "PUBLIC")
                        sql = string.Format("CREATE OR REPLACE PUBLIC SYNONYM {0} FOR {1};", source.Name, source.ObjectTargetName);
                    else
                        sql = string.Format("CREATE OR REPLACE SYNONYM {1}.{2} FOR {3};", p, source.Owner, source.Name, source.ObjectTargetName);

                    WriteFile(p, sql);
                }

            }

        }

        public virtual void AppendMissing(ProcedureModel source)
        {

            AppendDifference(source, false);

            if (generateSource)
            {
                string p = BuildPath(Path.Combine(this.folderForSource, source.Owner), "Procedures", source.Name);
                WriteFile(p, CreateOrReplace + source.Code.GetSource());
            }

        }

        public virtual void AppendMissing(SequenceModel source)
        {
            AppendDifference(source, false);
        }

        public virtual void AppendMissing(PackageModel source)
        {

            AppendDifference(source, false);

            if (generateSource)
            {
                string p = BuildPath(Path.Combine(this.folderForSource, source.Owner), "PackageBodies", source.Name);
                WriteFile(p, CreateOrReplace + source.CodeBody.GetSource());

                p = BuildPath(Path.Combine(this.folderForSource, source.Owner), "Packages", source.Name);
                WriteFile(p, CreateOrReplace + source.Code.GetSource());

            }
        }

        public virtual void AppendMissing(GrantModel source, OracleDatabase targetModel)
        {

            AppendDifference(source, false);

            string schema = source.ObjectSchema;
            string role = source.Role;

            string _key = source.ObjectSchema.Replace(@"""", "") + ", " + source.Role;
            if (_grants.Add(_key))
            {
                HashSet<string> _doublons = new HashSet<string>();

                if (generateSource)
                {
                    string p = BuildPath(Path.Combine(this.folderForSource, source.ObjectSchema), "UserObjectPrivileges", source.Role.Replace(@"""", ""));
                    string p2 = BuildPath(Path.Combine(this.folderForSource, source.ObjectSchema), @"UserObjectPrivileges\AdvancedQueue", source.Role.Replace(@"""", ""));
                    var lst = source.Root.Grants.OfType<GrantModel>().Where(c => c.ObjectSchema == schema && c.Role == role).OrderBy(c => c.ObjectSchema + c.ObjectName).ToList();
                    StringBuilder sbGrant = new StringBuilder();
                    StringBuilder sbGrant2 = new StringBuilder();
                    _doublons.Clear();
                    foreach (var t in lst)
                    {
                        string sql = BuildGrant(t);

                        if (_doublons.Add(sql))
                        {
                            if (sql.StartsWith("EXECUTE"))
                                sbGrant2.AppendLine(sql);
                            else
                                sbGrant.AppendLine(sql);
                        }

                    }

                    if (sbGrant.Length > 0)
                        WriteFile(p, sbGrant.ToString());

                    if (sbGrant2.Length > 0)
                        WriteFile(p2, sbGrant2.ToString());

                }

                if (generateTarget)
                {
                    string p = BuildPath(Path.Combine(this.folderForTarget, source.ObjectSchema), "UserObjectPrivileges", source.Role.Replace(@"""", ""));
                    string p2 = BuildPath(Path.Combine(this.folderForTarget, source.ObjectSchema), @"UserObjectPrivileges\AdvancedQueue", source.Role.Replace(@"""", ""));
                    var lst = targetModel.Grants.OfType<GrantModel>().Where(c => c.ObjectSchema == schema && c.Role == role).OrderBy(c => c.ObjectSchema + c.ObjectName).ToList();
                    StringBuilder sbGrant = new StringBuilder();
                    StringBuilder sbGrant2 = new StringBuilder();
                    _doublons.Clear();
                    foreach (var t in lst)
                    {
                        string sql = BuildGrant(t);

                        if (_doublons.Add(sql))
                        {
                            if (sql.StartsWith("EXECUTE"))
                                sbGrant2.AppendLine(sql);
                            else
                                sbGrant.AppendLine(sql);
                        }

                    }

                    if (sbGrant.Length > 0)
                        WriteFile(p, sbGrant.ToString());

                    if (sbGrant2.Length > 0)
                        WriteFile(p2, sbGrant2.ToString());

                }

            }

        }

        private static string BuildGrant(GrantModel t)
        {
            var u = t.Privileges.Select(c => c.Name.ToUpper()).OrderBy(c => c).ToArray();
            string privilegs = string.Join(", ", u);
            string sql = null;

            if (u.Contains("DEQUEUE") || u.Contains("ENQUEUE"))
            {
                sql = string.Format(@"EXECUTE DBMS_AQADM.GRANT_QUEUE_PRIVILEGE (privilege => '{0}', queue_name => '{1}', grantee => '{2}', grant_option => {3});",
                        privilegs,
                        t.FullObjectName.Replace(@"""", ""),
                        t.Role,
                        t.Grantable ? "TRUE" : "FALSE");

            }
            else
            {
                sql = string.Format(@"GRANT {0} ON {1} TO {2}{3};",
                    privilegs,
                    t.FullObjectName.Replace(@"""", ""),
                    t.Role,
                    t.Grantable ? " WITH GRANT OPTION" : string.Empty);

            }
            return sql;
        }

        public virtual void AppendMissing(ConstraintModel source)
        {
            AppendDifference(source, false);
        }

        public virtual void AppendMissing(PropertyModel source)
        {
            AppendDifference(source, false);
        }

        public virtual void AppendMissing(ColumnModel source)
        {
            AppendDifference(source, false);
        }

        public virtual void AppendMissing(TableModel source)
        {

            AppendDifference(source, false);

            string typeObject = "Tables";

            if (source.IsMaterializedView)
                typeObject = "MaterializedViews";

            else if (source.IsView)
                typeObject = "Views";

            if (source.IsView)
            {
                string p = BuildPath(Path.Combine(this.folderForSource, source.Owner), typeObject, source.Name);
                WriteFile(p, Utils.Unserialize(source.CodeView, true));
            }
            else
            {
                // Tables
                // non pris en charge dans ce generateur
            }

        }

        public virtual void AppendMissing(ArgumentModel argSource, ProcedureModel source, ProcedureModel target)
        {

            AppendDifference(source, false);

            if (generateSource)
            {
                string p = BuildPath(Path.Combine(this.folderForSource, source.Owner), "Procedures", source.Name);
                WriteFile(p, CreateOrReplace + source.Code.GetSource());
            }

            if (generateTarget)
            {
                string p = BuildPath(Path.Combine(this.folderForTarget, target.Owner), "Procedures", target.Name);
                WriteFile(p, CreateOrReplace + target.Code.GetSource());
            }

        }

        public virtual void AppendMissing(IndexModel source)
        {
            AppendDifference(source, false);
        }

        public virtual void AppendMissing(TriggerModel source)
        {

            AppendDifference(source, false);

            if (generateSource)
            {
                string p = BuildPath(Path.Combine(this.folderForSource, source.TableReference.Owner), "Triggers", source.Name);
                WriteFile(p, CreateOrReplace + Utils.Unserialize(source.Code, true));
            }

        }


        public virtual void AppendToRemove(TypeItem target)
        {
            AppendDifference(target, true);
        }

        public virtual void AppendToRemove(SynonymModel target)
        {
            AppendDifference(target, true);
        }

        public virtual void AppendToRemove(ProcedureModel target)
        {
            AppendDifference(target, true);
        }

        public virtual void AppendToRemove(SequenceModel target)
        {
            AppendDifference(target, true);
        }

        public virtual void AppendToRemove(PackageModel target)
        {
            AppendDifference(target, true);
        }

        public virtual void AppendToRemove(TableModel target)
        {
            AppendDifference(target, true);
        }

        public virtual void AppendToRemove(IndexModel target)
        {
            AppendDifference(target, true);
        }

        public virtual void AppendToRemove(TriggerModel target)
        {
            AppendDifference(target, true);
        }

        public virtual void AppendToRemove(ConstraintModel target)
        {
            AppendDifference(target, true);
        }

        public virtual void AppendToRemove(GrantModel target)
        {
            AppendDifference(target, true);
        }


        public virtual void AppendChange(ConstraintModel source, ConstraintModel target, string propertyName)
        {
            AppendDifference(source, target, propertyName);
        }

        public virtual void AppendChange(TypeItem source, TypeItem target, string propertyName)
        {

            AppendDifference(source, target, propertyName);

            if (string.IsNullOrEmpty(source.PackageName))
            {
                if (generateSource)
                {
                    StringBuilder sb = new StringBuilder(source.Code.Length + source.CodeBody.Length + 100);
                    if (!source.Code.IsEmpty)
                    {
                        sb.AppendLine(CreateOrReplace + (source.Code?.GetSource() ?? string.Empty));
                        sb.AppendLine(string.Empty);
                        sb.AppendLine("/");
                    }
                    if (!source.CodeBody.IsEmpty)
                        sb.AppendLine(CreateOrReplace + (source.CodeBody?.GetSource() ?? string.Empty));
                    string p = BuildPath(Path.Combine(this.folderForSource, source.Owner), "Types", source.Name);
                    WriteFile(p, sb.ToString());
                }

                if (generateTarget)
                {
                    StringBuilder sb = new StringBuilder(target.Code.Length + target.CodeBody.Length + 100);
                    if (!target.Code.IsEmpty)
                    {
                        sb.AppendLine(CreateOrReplace + (target.Code?.GetSource() ?? string.Empty));
                        sb.AppendLine(string.Empty);
                        sb.AppendLine("/");
                    }
                    if (!target.CodeBody.IsEmpty)
                        sb.AppendLine(CreateOrReplace + (target.CodeBody?.GetSource() ?? string.Empty));

                    string p = BuildPath(Path.Combine(this.folderForTarget, target.Owner), "Types", target.Name);
                    WriteFile(p, sb.ToString());
                }
            }


        }

        public virtual void AppendChange(PropertyModel source, PropertyModel target, string propertyName)
        {
            AppendDifference(source, target, propertyName);
        }

        public virtual void AppendChange(SynonymModel source, SynonymModel target, string propertyName)
        {

            AppendDifference(source, target, propertyName);

            if (generateSource)
            {
                string p = BuildPath(Path.Combine(this.folderForSource, source.ObjectTargetOwner), "Synonyms", source.Name);
                if (!File.Exists(p))
                {
                    string sql = string.Empty;
                    if (source.Owner == "PUBLIC")
                        sql = string.Format("CREATE OR REPLACE PUBLIC SYNONYM {0} FOR {1};", source.Name, source.ObjectTargetName);
                    else
                        sql = string.Format("CREATE OR REPLACE SYNONYM {1}.{2} FOR {3};", p, source.Owner, source.Name, source.ObjectTargetName);
                    WriteFile(p, sql);
                }
            }

            if (generateTarget)
            {
                string p = BuildPath(Path.Combine(this.folderForTarget, target.ObjectTargetOwner), "Synonyms", target.Name);
                if (!File.Exists(p))
                {
                    string sql = string.Empty;
                    if (source.Owner == "PUBLIC")
                        sql = string.Format("CREATE OR REPLACE PUBLIC SYNONYM {0} FOR {1};", source.Name, source.ObjectTargetName);
                    else
                        sql = string.Format("CREATE OR REPLACE SYNONYM {1}.{2} FOR {3};", p, source.Owner, source.Name, source.ObjectTargetName);
                    WriteFile(p, sql);
                }
            }

        }

        public virtual void AppendChange(ProcedureModel source, ProcedureModel target, string propertyName)
        {

            AppendDifference(source, target, propertyName);

            if (generateSource)
            {
                string p = BuildPath(Path.Combine(this.folderForSource, source.Owner), "Procedures", source.Name);
                WriteFile(p, CreateOrReplace + source.Code.GetSource());
            }

            if (generateTarget)
            {
                string p = BuildPath(Path.Combine(this.folderForTarget, target.Owner), "Procedures", target.Name);
                WriteFile(p, CreateOrReplace + target.Code.GetSource());
            }

        }

        public virtual void AppendChange(ArgumentModel argSource, ArgumentModel argTarget, string propertyName, ProcedureModel source, ProcedureModel target)
        {

            AppendDifference(source, target, propertyName);

            if (generateSource)
            {
                string p = BuildPath(Path.Combine(this.folderForSource, source.Owner), "Procedures", source.Name);
                WriteFile(p, CreateOrReplace + source.Code.GetSource());
            }

            if (generateTarget)
            {
                string p = BuildPath(Path.Combine(this.folderForTarget, target.Owner), "Procedures", target.Name);
                WriteFile(p, CreateOrReplace + target.Code.GetSource());
            }

        }

        public virtual void AppendChange(SequenceModel source, SequenceModel target, string propertyName)
        {
            AppendDifference(source, target, propertyName);
        }

        public virtual void AppendChange(PackageModel source, PackageModel target, string propertyName)
        {

            AppendDifference(source, target, propertyName);

            if (propertyName == "CodeBody")
            {
                if (generateSource)
                {
                    string p = BuildPath(Path.Combine(this.folderForSource, source.Owner), "PackageBodies", source.Name);
                    WriteFile(p, CreateOrReplace + source.CodeBody.GetSource());
                }

                if (generateTarget)
                {
                    string p = BuildPath(Path.Combine(this.folderForTarget, target.Owner), "PackageBodies", target.Name);
                    WriteFile(p, CreateOrReplace + target.CodeBody.GetSource());
                }
            }
            else
            {
                if (generateSource)
                {
                    string p = BuildPath(Path.Combine(this.folderForSource, source.Owner), "Packages", source.Name);
                    WriteFile(p, CreateOrReplace + source.Code.GetSource());
                }

                if (generateTarget)
                {
                    string p = BuildPath(Path.Combine(this.folderForTarget, target.Owner), "Packages", target.Name);
                    WriteFile(p, CreateOrReplace + target.Code.GetSource());
                }
            }

        }

        public virtual void AppendChange(GrantModel source, GrantModel target, string propertyName)
        {

            AppendDifference(source, target, propertyName);

            string schema = source.ObjectSchema;
            string role = source.Role;

            string _key = source.ObjectSchema.Replace(@"""", "") + ", " + source.Role;
            if (_grants.Add(_key))
            {
                HashSet<string> _doublons = new HashSet<string>();

                if (generateSource)
                {
                    string p = BuildPath(Path.Combine(this.folderForSource, source.ObjectSchema), "UserObjectPrivileges", source.Role);
                    string p2 = BuildPath(Path.Combine(this.folderForSource, source.ObjectSchema), @"UserObjectPrivileges\AdvancedQueue", source.Role);
                    var lst = source.Root.Grants.OfType<GrantModel>().Where(c => c.ObjectSchema == schema && c.Role == role).OrderBy(c => c.ObjectSchema + c.ObjectName).ToList();
                    StringBuilder sbGrant = new StringBuilder();
                    StringBuilder sbGrant2 = new StringBuilder();
                    _doublons.Clear();
                    foreach (var t in lst)
                    {
                        string sql = BuildGrant(t);
                        if (_doublons.Add(sql))
                        {
                            if (sql.StartsWith("EXECUTE"))
                                sbGrant2.AppendLine(sql);
                            else
                                sbGrant.AppendLine(sql);
                        }
                    }

                    if (sbGrant.Length > 0)
                        WriteFile(p, sbGrant.ToString());

                    if (sbGrant2.Length > 0)
                        WriteFile(p2, sbGrant2.ToString());

                }

                if (generateTarget)
                {
                    string p = BuildPath(Path.Combine(this.folderForTarget, target.ObjectSchema), "UserObjectPrivileges", target.Role);
                    string p2 = BuildPath(Path.Combine(this.folderForTarget, target.ObjectSchema), @"UserObjectPrivileges\AdvancedQueue", target.Role);
                    var lst = target.Root.Grants.OfType<GrantModel>().Where(c => c.ObjectSchema == schema && c.Role == role).OrderBy(c => c.ObjectSchema + c.ObjectName).ToList();
                    StringBuilder sbGrant = new StringBuilder();
                    StringBuilder sbGrant2 = new StringBuilder();
                    _doublons.Clear();
                    foreach (var t in lst)
                    {
                        string sql = BuildGrant(t);
                        if (_doublons.Add(sql))
                        {
                            if (sql.StartsWith("EXECUTE"))
                                sbGrant2.AppendLine(sql);
                            else
                                sbGrant.AppendLine(sql);
                        }
                    }
                    if (sbGrant.Length > 0)
                        WriteFile(p, sbGrant.ToString());

                    if (sbGrant2.Length > 0)
                        WriteFile(p2, sbGrant2.ToString());
                }

            }


        }

        public virtual void AppendChange(ColumnModel source, ColumnModel target, string propertyName)
        {
            AppendDifference(source, target, propertyName);
        }

        public virtual void AppendChange(IndexModel source, IndexModel target, string propertyName, string columnsources = "", string columntarget = "")
        {
            var d = new DifferenceModel()
            {
                Source = source,
                Target = target,
                Kind = TypeDifferenceEnum.Change,
                PropertyName = propertyName,
                ColumnSource = columnsources,
                ColumnTarget = columntarget
            };
            this._lst.Add(d);
        }

        public virtual void AppendChange(TableModel source, TableModel target, string propertyName)
        {

            AppendDifference(source, target, propertyName);

            string typeObject = "Tables";

            if (source.IsMaterializedView)
                typeObject = "MaterializedViews";

            else if (source.IsView)
                typeObject = "Views";

            if (source.IsView)
            {
                if (generateSource)
                {
                    string p = BuildPath(Path.Combine(this.folderForSource, source.Owner), typeObject, source.Name);
                    WriteFile(p, Utils.Unserialize(source.CodeView, true).Trim('\n', ' '));
                }

                if (generateTarget)
                {
                    string p = BuildPath(Path.Combine(this.folderForTarget, target.Owner), typeObject, target.Name);
                    WriteFile(p, Utils.Unserialize(target.CodeView, true).Trim('\n', ' '));
                }
            }
            else
            {
                // Tables
                // non pris en charge dans ce generateur
            }

        }


        public virtual void AppendChange(TriggerModel source, TriggerModel target, string propertyName)
        {

            AppendDifference(source, target, propertyName);

            if (generateSource)
            {
                string p = BuildPath(Path.Combine(this.folderForSource, source.TableReference.Owner), "Triggers", source.Name);
                WriteFile(p, CreateOrReplace + Utils.Unserialize(source.Code, true));
            }

            if (generateTarget)
            {
                string p = BuildPath(Path.Combine(this.folderForTarget, target.TableReference.Owner), "Triggers", target.Name);
                WriteFile(p, CreateOrReplace + Utils.Unserialize(target.Code, true));
            }

        }


        public virtual void AppendDoublons(string type, ItemBase item, string name)
        {
            var d = new DoublonModel() { Kind = TypeDifferenceEnum.Doublon, Source = item, Type = type, Files = item.Files.OfType<FileElement>().ToList(), PropertyName = name };
            AppendDifference(d, false);
        }

        public virtual void AppendDoublons(string type, ItemBase[] item)
        {
            var d = new DoublonModel() { Kind = TypeDifferenceEnum.DuplicatedIndex, Source = item, Type = type, Files = item.SelectMany(c => c.Files.OfType<FileElement>()).ToList() };
            AppendDifference(d, false);
        }

        protected DifferenceModel AppendDifference(object source, bool ToRemove)
        {

            var type = source.GetType().Name;

            var d = new DifferenceModel()
            {
                Source = source,
                Kind = ToRemove
                    ? TypeDifferenceEnum.MissingInSource
                    : TypeDifferenceEnum.MissingInTarget,
            };
            this._lst.Add(d);

            if (source is DoublonModel)
                log(string.Format("{2} {0} is duplicated in the source. Files : {1}", GetName(source), GetFilename(source), type));

            else
            {
                if (ToRemove)
                    log(string.Format("{2} {0} must be removed in the target. Files : {1}", GetName(source), GetFilename(source), type));
                else
                    log(string.Format("{2} {0} is missing in the target. Files : {1}", GetName(source), GetFilename(source), type));
            }

            return d;

        }

        protected DifferenceModel AppendDifference(object source, object target, string propertyName)
        {
            var type = source.GetType().Name;

            if (propertyName == "Name")
                log(string.Format("{2} {0} is renamed in {1} in the target", GetName(source), GetName(target), type));
            else
                log(string.Format("{2} property '{1}' in '{0}' is changed in the target", GetName(source), propertyName, type));

            var d = new DifferenceModel()
            {
                Source = source,
                Target = target,
                Kind = TypeDifferenceEnum.Change,
                PropertyName = propertyName
            };
            this._lst.Add(d);
            return d;
        }

        internal static string GetFilename(object item)
        {

            if (item is ItemBase i)
                return string.Join(", ", i.Files.Select(c => c.ToString()));

            return string.Empty;

        }


        internal static string GetName(object item)
        {
            string result = string.Empty;

            if (item is ColumnModel)
            {
                var c = item as ColumnModel;
                result = string.Format("{0}.{1}", GetName(c.Parent), c.Name);
            }
            else if (item is GrantModel)
            {
                var g = item as GrantModel;
                result = string.Format("{0} {1} to {2}", string.Join(", ", g.PrivilegesToText), g.FullObjectName, g.Role);
            }
            else if (item is Ichangable)
            {
                var i = item as Ichangable;
                return string.Format("{0}.{1}", i.GetOwner(), i.GetName());
            }

            //else if (item is IndexModel)
            //    result = (item as IndexModel).GetName();

            //else if (item is TypeItem)
            //    result = (item as TypeItem).Key;


            else if (item is SynonymModel)
                result = (item as SynonymModel).Key;

            else if (item is DoublonModel)
                result = GetName((item as DoublonModel).Source);

            //else if (item is ConstraintModel)
            //    result = GetFile((item as ConstraintModel).Parent);

            //else if (item is SequenceModel)
            //    result = (item as SequenceModel).Name;

            //else if (item is PackageModel)
            //    result = (item as PackageModel).Name;

            //else if (item is ProcedureModel)
            //    result = (item as ProcedureModel).Name;

            //else if (item is TriggerModel)
            //    result = GetFile((item as TriggerModel).Parent);

            else
            {

            }

            return result;

        }

        public void Run(ProcessorBase processor)
        {
            processor.Run(this._lst);
        }

        protected void WriteFile(string p, string code)
        {
            try
            {

                FileInfo f = new FileInfo(p);
                if (!f.Directory.Exists)
                    f.Directory.Create();

                using (var stream = File.AppendText(p))
                {
                    stream.Write(code);
                }

            }
            catch (Exception e)
            {
                if (System.Diagnostics.Debugger.IsAttached)
                    System.Diagnostics.Debugger.Break();

                Trace.WriteLine("Exception -> " + e.Message);
            }

        }

        protected string BuildPath(string root, string objectType, string filename, bool isRollback = false)
        {
            try
            {
                if (isRollback)
                    return Path.Combine(root, objectType, filename + ".bak.sql");
                else
                    return Path.Combine(root, objectType, filename + ".sql");

            }
            catch (Exception)
            {
                string msg = $"invalid datas for build path root={root} objectType={objectType} filename={filename}";
                Trace.WriteLine(msg);
                throw;
            }
        }

        internal void AppendChange(PhysicalAttributesModel source, PhysicalAttributesModel target, string v)
        {
            throw new NotImplementedException();
        }
    }
}