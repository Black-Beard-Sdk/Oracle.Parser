using Bb.Oracle;
using Bb.Oracle.Models;
using Bb.Oracle.Models.Comparer;
using Bb.Oracle.Structures.Models;
using CompareModel.Exports.Excels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompareModel
{

    public class ProcessorExcel : ProcessorCollectorBase
    {

        private ExcelWriter Writer;
        private readonly string url;
        private bool sourceIs;
        private bool targetIs;
        private readonly string projectName;
        private bool withoutTfs;

        public ProcessorExcel(bool withoutTfs, string url, bool sourceIs, bool targetIs, string projectName)
        {
            this.withoutTfs = withoutTfs;
            this.url = url;
            this.sourceIs = sourceIs;
            this.targetIs = targetIs;
            this.projectName = projectName;
            this.Writer = new ExcelWriter();

        }

        internal void Save(string fullName)
        {
            this.Writer.Save(fullName);
        }

        public override void Run(DifferenceModels diffs, string rootSource, string rootTarget)
        {

            base.Run(diffs, rootSource, rootTarget);
            //if (!withoutTfs)
            //    ResolveNames(diffs, rootSource, rootTarget);

            Append(new GrantLine("kind", "Full object name", "Role / Grantee", "Privilege", "Grantable", "sql", ExcelConstants.Source, ExcelConstants.LastCommiter, "files sources", ExcelConstants.Target, ExcelConstants.LastCommiter, "files target"));
            Append(new ProcedureLine("kind", "Schema name", "Package name", "Name", "args", ExcelConstants.Source, ExcelConstants.LastCommiter, ExcelConstants.Target, ExcelConstants.LastCommiter));
            Append(new SequenceLine("kind", "Schema name", "Package name", "Name", "changes", ExcelConstants.Source, ExcelConstants.LastCommiter, ExcelConstants.Target, ExcelConstants.LastCommiter));
            Append(new SynonymLine("kind", "Schema name", "Name", "Object target", ExcelConstants.Source, ExcelConstants.LastCommiter, ExcelConstants.Target, ExcelConstants.LastCommiter));
            Append(new TypeLine("kind", "Schema name", "Package name", "Name", "Collection schema name", "Collection type name", ExcelConstants.Source, ExcelConstants.LastCommiter, ExcelConstants.Target, ExcelConstants.LastCommiter));
            Append(new ConstraintLine("kind", "schema name", "table name", "column", "property changed", "target name", "Type", "Rel owner", "Rel name", "sql1", "sql2", ExcelConstants.Source, ExcelConstants.LastCommiter, ExcelConstants.Target, ExcelConstants.LastCommiter));
            Append(new TableLine("Kind", "Schema name", "Table name", "Property Changed", ExcelConstants.Source, ExcelConstants.LastCommiter, ExcelConstants.Target, ExcelConstants.LastCommiter));
            Append(new ColumnLine("kind", "schema name", "table name", "column", "property changed", ExcelConstants.Source, ExcelConstants.LastCommiter, ExcelConstants.Target, ExcelConstants.LastCommiter));
            Append(new IndexLine("kind", "schema name", "table name", "Name", "property changed", ExcelConstants.Source, ExcelConstants.LastCommiter, ExcelConstants.Target, ExcelConstants.LastCommiter));
            Append(new TriggerLine("kind", "schema name", "table name", "TriggerName", "property changed", ExcelConstants.Source, ExcelConstants.LastCommiter, ExcelConstants.Target, ExcelConstants.LastCommiter));
            Append(new ViewLine("kind", "schema name", "table name", "schema name", "Table name", "property changed", ExcelConstants.Source, ExcelConstants.LastCommiter, ExcelConstants.Target, ExcelConstants.LastCommiter));
            Append(new PackageLine("kind", "schema name", "Package name", ExcelConstants.Source, ExcelConstants.LastCommiter, ExcelConstants.Target, ExcelConstants.LastCommiter));
            Append(new PackageBodyLine("kind", "schema name", "Package body name", ExcelConstants.Source, ExcelConstants.LastCommiter, ExcelConstants.Target, ExcelConstants.LastCommiter));
            Append(new DuplicateObjectsyLine("Name", "type", "file 1", "file 2", ExcelConstants.Source, ExcelConstants.LastCommiter, ExcelConstants.Target, ExcelConstants.LastCommiter));

            ProcessGrants();
            ProcessProcedures();
            ProcesSequences();
            ProcessSynonyms();
            ProcessType();
            ProcessTable();
            ProcessPackages();
            ProcessDuplicateObjects();

        }

        //private void ResolveNames(DifferenceModels diffs, string rootSource, string rootTarget)
        //{
        //    ChangesetRef blk;

        //    Dictionary<string, ChangesetRef> _items = new Dictionary<string, ChangesetRef>();

        //    var result = EvaluateChangesets(rootSource, diffs, rootSource, rootTarget, _items);
        //    result.Wait();

        //    foreach (var item in diffs.Items)
        //    {
        //        FileElement file = null;

        //        if (sourceIs)
        //        {
        //            var _i = item.Source as ItemBase;
        //            file = GetFile(_i);
        //            if (file != null)
        //            {
        //                string path = rootTarget + "/" + file.Path.Replace("\\", "/");
        //                if (_items.TryGetValue(path, out blk))
        //                {
        //                    _i.Tag = blk;
        //                }
        //                _i.File = file;
        //            }
        //        }

        //        file = null;

        //        if (targetIs)
        //        {
        //            var _i = item.Target as ItemBase;
        //            file = GetFile(_i);
        //            if (file != null)
        //            {
        //                string path = rootTarget + "/" + file.Path.Replace("\\", "/");
        //                if (_items.TryGetValue(path, out blk))
        //                {
        //                    _i.Tag = blk;
        //                }
        //                _i.File = file;
        //            }
        //        }

        //    }


        //}

        //private async Task EvaluateChangesets(string root, DifferenceModels diffs, string rootSource, string rootTarget, Dictionary<string, ChangesetRef> _items)
        //{

        //    string excluded = @"Lotfi RACHDI";

        //    int count = 0;
        //    ChangesetRef blk;
        //    HashSet<string> _paths = new HashSet<string>();
        //    using (TfsHelperClient client = new TfsHelperClient(this.url))
        //    {
        //        client.Authenticate();

        //        foreach (var item in diffs.Items.AsParallel())
        //        {
        //            FileElement file = null;
        //            if (sourceIs)
        //            {
        //                file = GetFile(item.Source as ItemBase);
        //                if (file != null)
        //                {
        //                    string path = root + "/" + file.Path.Replace("\\", "/");
        //                    if (_paths.Add(path) && !_items.TryGetValue(path, out blk))
        //                    {
        //                        count++;
        //                        Console.WriteLine(String.Format("Calling tfs -> {0}", path));
        //                        var task = await client.GetChangsetsFrom(projectName, path);
        //                        Console.WriteLine(String.Format("{0} result(s)", task.Count()));
        //                        _items.Add(path, task.FirstOrDefault(c => c != null && c.Author != null && c.Author.DisplayName != excluded));
        //                    }

        //                    item.sourceFile = file;
        //                }
        //            }

        //            file = null;
        //            if (targetIs)
        //            {

        //                var itembase = item.Target as ItemBase;
        //                if (item.Users == null)
        //                {
        //                    file = GetFile(itembase);
        //                    if (file != null)
        //                    {
        //                        string path = root + "/" + file.Path.Replace("\\", "/");
        //                        if (!path.EndsWith(@"/{Virtual}"))
        //                        {
        //                            if (_paths.Add(path) && !_items.TryGetValue(path, out blk))
        //                            {

        //                                count++;
        //                                Console.WriteLine(String.Format("Calling tfs -> {0}", path));
        //                                try
        //                                {
        //                                    var task = await client.GetChangsetsFrom(projectName, path);
        //                                    Console.WriteLine(String.Format("{0} result(s)", task.Count()));
        //                                    _items.Add(path, task.FirstOrDefault(c => c != null && c.Author != null && c.Author.DisplayName != excluded));
        //                                }
        //                                catch (Exception e)
        //                                {
        //                                    if (System.Diagnostics.Debugger.IsAttached)
        //                                        System.Diagnostics.Debugger.Break();
        //                                    Console.WriteLine(e.Message);
        //                                    //throw;
        //                                }

        //                            }

        //                            item.targetFile = file;
        //                        }
        //                        else
        //                        {

        //                        }
        //                    }
        //                }
        //                else
        //                {

        //                }

        //            }

        //        }
        //    }

        //}

        //private async Task Load(string path, TfsHelperClient client, Dictionary<string, ChangesetRef> _items)
        //{

        //    string[] _path = path.Split('/');
        //    var u = string.Join("/", _path.Take(_path.Length - 2)).Trim('/');

        //    Console.WriteLine(String.Format("Calling tfs -> {0}", path));
        //    var task = await client.GetChangsetsFrom(projectName, u);
        //    Console.WriteLine(String.Format("{0} result(s)", task.Count()));

        //    foreach (ChangesetRef item in task)
        //    {
        //        var lst2 = await client.GetChangset(item.ChangesetId);
        //    }


        //}

        private static FileElement GetFile(ItemBase item)
        {
            FileElement result = null;
            if (item != null)
            {

                result = item.Files.OfType<FileElement>().FirstOrDefault();

                if (result == null)
                {
                    if (item is IndexModel)
                        result = GetFile((item as IndexModel).Parent as ItemBase);

                    //else if (item is TypeItem)
                    //    result = (item as TypeItem).Key;

                    //else if (item is GrantModel)
                    //    result = (item as GrantModel).Key;

                    //else if (item is SynonymModel)
                    //    result = (item as SynonymModel).Key;

                    else if (item is ConstraintModel)
                        result = GetFile((item as ConstraintModel).Parent as ItemBase);

                    //else if (item is SequenceModel)
                    //    result = (item as SequenceModel).Name;

                    //else if (item is PackageModel)
                    //    result = (item as PackageModel).Name;

                    //else if (item is ProcedureModel)
                    //    result = (item as ProcedureModel).Name;

                    else if (item is ColumnModel)
                        result = GetFile((item as ColumnModel).Parent as ItemBase);

                    else if (item is TriggerModel)
                        result = GetFile((item as TriggerModel).Parent as ItemBase);

                    else
                    {

                    }

                    //else if (item is TableModel)
                    //    result = (item as TableModel).Key;

                }

            }

            return result;

        }

        private void ProcessDuplicateObjects()
        {
            foreach (DoublonModel item in this._doublons)
            {
                Append(new DuplicateObjectsyLine(item.Source as ItemBase, null, item.PropertyName, item.Type, item.Files.ToArray()));
            }
        }

        private void ProcessTable()
        {

            foreach (var item in base._tables)
            {

                table t = item.Value;
                if (item.Value.Kind == TypeDifferenceEnum.MissingInTarget || item.Value.Kind == TypeDifferenceEnum.MissingInSource)
                {
                    if (t.Source.IsView)
                        Append(new ViewLine(item.Value.Source, item.Value.Target, ConvertToText(t.Kind), t.Source.Owner, t.Source.Name, string.Empty));
                    else
                        Append(new TableLine(item.Value.Source, item.Value.Target, ConvertToText(t.Kind), t.Source.Owner, t.Source.Name, string.Empty));
                }
                else
                {

                    #region tables changes

                    foreach (var ch1 in item.Value.Changes)
                    {
                        switch (ch1.ToLower())
                        {

                            //case "blocpartition":                           
                            //case "code":
                            //case "InitialExtent":
                            //case "comment":
                            //case "tablespacename":
                            //case "initialextent":
                            //case "segmentcreated":
                            //case "nextextent":
                            //case "minextents":
                            //case "logging":
                            //case "rowmovement":
                            //case "pctfree":
                            //case "partitioned":
                            //case "maxtrans":
                            //case "initrans":
                            //case "description":
                            //case "bufferpool":
                            //case "triggerstatus":
                            //    break;

                            case "codeview":
                                Append(new ViewLine(item.Value.Source, item.Value.Target, ConvertToText(t.Kind), t.Source.Owner, t.Source.Name, ch1));
                                break;

                            default:
                                if (t.Source.IsView)
                                    Append(new ViewLine(item.Value.Source, item.Value.Target, ConvertToText(t.Kind), t.Source.Owner, t.Source.Name, ch1));
                                else
                                    Append(new TableLine(item.Value.Source, item.Value.Target, ConvertToText(t.Kind), t.Source.Owner, t.Source.Name, ch1));
                                break;
                        }


                    }

                    #endregion tables changes

                    if (item.Value.Source.IsView)
                    {

                    }
                    else
                    {
                        foreach (var c in item.Value.Columns)
                        {
                            string sql = string.Empty;      // string.Format("ALTER TABLE {0}.{1} MODIFY {2} ", t.Source.GetOwner(), t.Source.Name, c.Value.Source.ColumnName);

                            if (c.Value.Kind == TypeDifferenceEnum.MissingInTarget || c.Value.Kind == TypeDifferenceEnum.MissingInSource)
                                Append(new ColumnLine(c.Value.Source, c.Value.Target, c.Value.Kind.ToString(), t.Source.Owner, t.Source.Name, c.Value.Source.Name, sql));

                            else
                            {
                                foreach (var ch in c.Value.Changes)
                                {
                                    Append(new ColumnLine(c.Value.Source, c.Value.Target, c.Value.Kind.ToString(), t.Source.Owner, t.Source.Name, c.Value.Source.Name, ch));
                                }
                            }
                        }
                    }

                    #region constraints

                    ProcessConstraints(item, t);

                    #endregion constraints

                    foreach (var c in item.Value.Indexes)
                    {
                        if (c.Value.Kind == TypeDifferenceEnum.MissingInTarget || c.Value.Kind == TypeDifferenceEnum.MissingInSource)
                            Append(new IndexLine(c.Value.Source, c.Value.Target, c.Value.Kind.ToString(), t.Source.Owner, t.Source.Name, c.Value.Source.Name, string.Empty));
                        else
                            foreach (var ch in c.Value.Changes)
                                Append(new IndexLine(c.Value.Source, c.Value.Target, ConvertToText(t.Kind), t.Source.Owner, t.Source.Name, c.Value.Source.Name, ch));
                    }

                    foreach (var c in item.Value.Triggers)
                    {
                        if (c.Value.Kind == TypeDifferenceEnum.MissingInTarget || c.Value.Kind == TypeDifferenceEnum.MissingInSource)
                            Append(new TriggerLine(c.Value.Source, c.Value.Target, c.Value.Kind.ToString(), t.Source.Owner, t.Source.Name, c.Value.Source.Name, string.Empty));
                        else
                            foreach (var ch in c.Value.Changes)
                                Append(new TriggerLine(c.Value.Source, c.Value.Target, ConvertToText(t.Kind), t.Source.Owner, t.Source.Name, c.Value.Source.Name, ch));
                    }
                }
            }
        }

        private void ProcessConstraints(KeyValuePair<string, table> item, table t)
        {
            foreach (var c in item.Value.Constraints)
            {

                string sqltarget = string.Empty;
                string sqlsource = string.Empty;

                if (c.Value.Kind == TypeDifferenceEnum.MissingInTarget)
                {
                    Append(new ConstraintLine(c.Value.Source, c.Value.Target, c.Value.Kind.ToString(), t.Source.Owner, t.Source.Name, c.Value.Source.Name, string.Empty, string.Empty, string.Empty, c.Value.Source.Reference.Owner, c.Value.Source.Reference.Name, sqltarget, sqlsource));
                }
                else
                {
                    //if (c.Ch)
                    foreach (var ch in c.Value.Changes)
                    {

                        string newConstraintName = null;

                        if (ch == "Name")
                        {

                            if (!c.Value.Source.Name.StartsWith("SYS_C"))
                                newConstraintName = c.Value.Source.Name;

                            else if (!c.Value.Target.Name.StartsWith("SYS_C"))
                                newConstraintName = c.Value.Target.Name;

                            else
                            {
                                newConstraintName = string.Format("{0}.{1} {2} {3}",
                                  t.Source.Owner,
                                  t.Source.Name,
                                  c.Value.Source.Type,
                                  string.Join(", ", c.Value.Source.Columns.OfType<ConstraintColumnModel>().Select(d => d.ColumnName))
                                );

                                newConstraintName = "RENAMED_" + Math.Abs(Crc32.Calculate(newConstraintName)).ToString();

                            }

                            if (c.Value.Source.Generated == "GENERATED NAME")
                            {
                                sqlsource = string.Format("ALTER TABLE {0}.{1} RENAME CONSTRAINT {2} TO RENAMED_{3}"
                                    , t.Source.Owner
                                    , t.Source.Name
                                    , c.Value.Source.Name
                                    , newConstraintName
                                    );

                            }

                            if (c.Value.Kind == TypeDifferenceEnum.Change)
                            {
                                if (c.Value.Target.Generated == "GENERATED NAME")
                                {
                                    sqltarget = string.Format("ALTER TABLE {0}.{1} RENAME CONSTRAINT {2} TO RENAMED_{3}"
                                       , t.Target.Owner
                                       , t.Target.Name
                                       , c.Value.Target.Name
                                       , newConstraintName
                                       );
                                }
                            }


                            newConstraintName = c.Value.Source.Name + " renamed to " + c.Value.Target.Name;

                        }
                        else
                            newConstraintName = c.Value.Source.Key;

                        string ctType = string.Empty;
                        switch (c.Value.Source.Type.ToUpper())
                        {
                            case "C":
                                ctType = "Check constraint on a table";
                                var _c = getConstraintText(c);
                                if (_c.Trim().EndsWith("IS NOT NULL"))
                                {
                                    // + string.Join(", ", c.Value.Source.Columns.OfType<ConstraintColumnModel>().Select(d => d.ColumnName).ToList()).Trim(',', ' ')
                                    ctType = "not null constraint on column";
                                }
                                else
                                {

                                }
                                break;
                            case "P":
                                ctType = "Primary key";
                                break;
                            case "U":
                                ctType = "Unique key";
                                break;
                            case "R":
                                ctType = "Foreign integrity";
                                break;
                            case "V":
                                ctType = "With check option, on a view";
                                break;
                            case "O":
                                ctType = "With read only, on a view";
                                break;
                            case "H":
                                ctType = "Hash expression";
                                break;
                            case "F":
                                ctType = "Constraint that involves a REF column";
                                break;
                            case "S":
                                ctType = "Supplemental logging";
                                break;

                            default:
                                break;
                        }

                        StringBuilder sb = new StringBuilder();
                        foreach (ConstraintColumnModel col in c.Value.Source.Columns)
                        {
                            sb.Append(col.ColumnName);
                            sb.Append(", ");
                        }

                        Append(new ConstraintLine(c.Value.Source, c.Value.Target, ConvertToText(t.Kind), t.Source.Owner, t.Source.Name, sb.ToString().Trim(',', ' '), ch, newConstraintName, ctType, c.Value.Source.Reference.Owner, c.Value.Source.Reference.Name, sqltarget, sqlsource));

                    }
                }
            }

        }

        private static string getConstraintText(KeyValuePair<string, constraint> c)
        {
            var s = Utils.Unserialize(c.Value.Source.Search_Condition, false).ToUpper();
            var v = s;
            while (v != (s = s.Replace("\t", " ").Replace("  ", " ")))
                v = s;
            return s;
        }

        private void ProcessType()
        {
            foreach (var item in base._types)
            {
                var t = item.Value;
                Append(new TypeLine(item.Value.Source, item.Value.Target, ConvertToText(t.Kind), t.Source.Owner, t.Source.PackageName, t.Source.Name, t.Source.CollectionSchemaName, t.Source.CollectionTypeName));
            }
        }

        private void ProcessSynonyms()
        {
            foreach (var item in base._synonyms)
            {
                var t = item.Value;
                Append(new SynonymLine(item.Value.Source, item.Value.Target, ConvertToText(t.Kind), t.Source.Owner, t.Source.Name, $"{t.Source.ObjectTargetOwner}.{t.Source.ObjectTargetName}"));
            }
        }

        private void ProcesSequences()
        {

            foreach (var item in base._sequences)
            {

                var t = item.Value;
                string sql = string.Empty;

                if (t.Kind == TypeDifferenceEnum.Change)
                {
                    sql = string.Format(@"ALTER SEQUENCE {0}.{1} ", t.Source.Owner, t.Source.Name);
                    foreach (var c1 in t.Changes)
                    {

                        switch (c1.ToLower())
                        {
                            case "minvalue":
                                sql += "MINVALUE " + t.Source.MinValue.ToString() + " ";
                                break;
                            case "maxvalue":
                                sql += "MAXVALUE " + t.Source.MaxValue.ToString() + " ";
                                break;
                            case "cachesize":
                                sql += "CACHE " + t.Source.CacheSize.ToString() + " ";
                                break;
                            case "cycleflag":
                                if (t.Target.CycleFlag != t.Source.CycleFlag)
                                    sql += t.Source.CycleFlag ? "CYCLE " : "NO CYCLE ";
                                break;
                            default:
                                break;
                        }
                    }
                }
                else if (t.Kind == TypeDifferenceEnum.MissingInSource)
                {
                    sql = string.Format(@"DROP SEQUENCE {0}.{1} ", t.Source.Owner, t.Source.Name);
                }

                Append(new SequenceLine(item.Value.Source, item.Value.Target, ConvertToText(t.Kind), t.Source.Owner, t.Source.Name, string.Join(", ", t.Changes).Trim(',', ' '), sql));

            }
        }

        private void ProcessProcedures()
        {
            foreach (var item in base._procedures)
            {
                var t = item.Value;
                var args = t.Source.Arguments.OfType<ArgumentModel>().Select(c => string.Format("{0}{1}{2} {3}", c.Out ? "out " : string.Empty, string.IsNullOrEmpty(c.Type.DataType.Owner) ? "" : "." + c.Type.DataType.Owner, c.Type.DataType, c.Name)).ToArray();
                Append(new ProcedureLine(item.Value.Source, item.Value.Target, ConvertToText(t.Kind), t.Source.GetOwner(), t.Source.PackageName, t.Source.Name, string.Join(", ", args)));
            }
        }

        private void ProcessPackages()
        {
            foreach (var item in base._packages)
            {
                var t = item.Value;

                if (item.Value.Package)
                    Append(new PackageLine(item.Value.Source, item.Value.Target, ConvertToText(t.Kind), t.Source.GetOwner(), t.Source.Name));

                if (item.Value.PackageBody)
                    Append(new PackageBodyLine(item.Value.Source, item.Value.Target, ConvertToText(t.Kind), t.Source.GetOwner(), t.Source.Name));

            }
        }

        private void ProcessGrants()
        {

            foreach (var item in base._grants)
            {
                var t = item.Value;

                if (!t.Source.ObjectName.StartsWith("BIN$"))
                {

                    string sql = string.Empty;
                    if (t.Kind == TypeDifferenceEnum.MissingInTarget || t.Kind == TypeDifferenceEnum.Change)
                        sql = string.Format(@"GRANT {0} ON {1} TO {2}{3};", t.Source.PrivilegesToText, t.Source.FullObjectName, t.Source.Role, t.Source.Grantable ? " WITH GRANT OPTION" : string.Empty);
                    else
                        sql = string.Format(@"REVOKE {0} ON {1} TO {2};", t.Source.PrivilegesToText, t.Source.FullObjectName, t.Source.Role);

                    var sss = t.Source != null && t.Source.Files !=  null ? string.Join(", ", t.Source.Files.Select(c => c.ToString())) : string.Empty;
                    var ttt = t.Target != null && t.Target.Files != null ? string.Join(", ", t.Target.Files.Select(c => c.ToString())): string.Empty;

                    Append(new GrantLine(item.Value.Source, item.Value.Target, ConvertToText(t.Kind), t.Source.FullObjectName, t.Source.Role, t.Source.PrivilegesToText, t.Source.Grantable ? "Grantable" : string.Empty, sql, "", "", sss, "", "", ttt));

                }
            }
        }

        private void Append(SheetBase row)
        {
            this.Writer.Append(row);
        }

    }

}

