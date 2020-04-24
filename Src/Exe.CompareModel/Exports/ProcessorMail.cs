//using Bb.Oracle;
//using Bb.Oracle.Models;
//using Bb.Oracle.Models.Comparer;
//using Bb.Oracle.Models.Configurations;
//using Bb.Oracle.Structures.Models;
//using CompareModel.Exports.Excels;
//using EvaluateRules;
//using EvaluateRules.Mails;
//using Pssa.Sdk.Net.Mails;
//using Pssa.Sdk.Net.Mails.Configurations;
//using Pssa.Sdk.Net.Mails.Models;
//using Pssa.Sdk.Net.Mails.Renderer;
//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading;

//namespace CompareModel
//{

//    public class ProcessorMail : ProcessorCollectorBase
//    {

//        private bool sourceIs;
//        private bool targetIs;
//        private readonly string projectName;
//        private readonly ResponsabilitiesSection section;
//        private ArgumentContext ctx;
//        public ProcessorMail(bool sourceIs, bool targetIs, string projectName, ResponsabilitiesSection section, ArgumentContext ctx)
//        {
//            this.sourceIs = sourceIs;
//            this.targetIs = targetIs;
//            this.projectName = projectName;
//            this.section = section;
//            this.ctx = ctx;
//        }

//        internal void Save(string fullName)
//        {

//        }

//        public override void Run(DifferenceModels diffs, string rootSource, string rootTarget)
//        {

//            SheetBase.WithFile = false;
//            base.Run(diffs, rootSource, rootTarget);
//            GetResponsabilities(diffs);

//            ProcessGrants();
//            ProcessProcedures();
//            ProcesSequences();
//            ProcessSynonyms();
//            ProcessType();
//            ProcessTable();
//            ProcessPackages();
//            //ProcessDuplicateObjects();

//            var rules = RuleSection.Configuration;

//            var dirTemplate = new DirectoryInfo(MailConfiguration.Configuration.TemplateFolder);
//            var renderer = new RazorMailRenderer(dirTemplate);
//            var mailService = new MailNotificationService(rules.MailServiceName, renderer);
//            mailService.MailEvents += MailService_MailEvents;
//            var @from = new MailReceiverModel(rules.Sender);
//            var culture = CultureInfo.GetCultureInfo(rules.Culture);
//            string subject = "Rapport pour '{0}' de la comparaison d'environement entre {1} et {2}";

//            List<datas> _datas = GetDatas(this.list);
//            GenerateMails(_datas, mailService, @from, subject, ctx);

//        }

//        private static void MailService_MailEvents(object sender, MailEventArgs e)
//        {
//            if (e.Exception != null)
//                Console.WriteLine(e.Exception.ToString());

//            else
//            {
//                string msg = e.Status.ToString() + " mail : " + e.Mail.Subject + ". " + e.Mail.To.First().User;
//                Console.WriteLine(msg);
//            }

//        }

//        private void GenerateMails(List<datas> datas, MailNotificationService mailService, MailReceiverModel from, string subject, ArgumentContext ctx)
//        {

//            foreach (datas data in datas)
//            {

//                Console.WriteLine("Envoie des mails pour " + data.Team.Name);

//                var users = data.Team.Users.OfType<Bb.Oracle.Models.Configurations.UserElement>().ToList();
//                var u = new Bb.Oracle.Models.Configurations.UserElement() { Name = "Cedric Jeandin", Email = "c.jeandin@pickup-services.com" };
//                users.Add(u);

//                if (System.Diagnostics.Debugger.IsAttached)
//                    users.Clear();

//                u = new Bb.Oracle.Models.Configurations.UserElement() { Name = "Gael beard", Email = "g.beard@pickup-services.com" };
//                users.Add(u);

//                foreach (var user in users)
//                {

//                    var to = new MailReceiverModel(user.Email) { Civilite = "Monsieur", Libelle = user.Name };

//                    string _subject = string.Format(subject, data.Team.Name, EnvironmentSource, EnvironmentTarget); // rules.Subject;

//                    RapportMailCompareModel model = new RapportMailCompareModel(@from, to)
//                    {
//                        Subject = _subject,
//                        HtmlTemplateName = "Rapport",
//                        IsBodyHtml = true,
//                        Datas = data,
//                        EnvironmentSource = this.EnvironmentSource,
//                        EnvironmentTarget = this.EnvironmentTarget,
//                        //PathSource = ctx.PathSource.Trim('/'),
//                    };

//                    mailService.Send(model);

//                    while (mailService.InTreatment > 0)
//                        Thread.Sleep(10);

//                    Console.WriteLine("Mail envoyé à " + user.Name);

//                }

//            }

//        }

//        private List<datas> GetDatas(List<chargeUser> datas)
//        {

//            var lst = datas.ToLookup(c => c.Team.Name);  // Organize by team
//            Dictionary<string, datas> _dic = new Dictionary<string, datas>();

//            foreach (IEnumerable<chargeUser> items in lst)
//            {

//                datas _datas;
//                var itemsByType = items.ToLookup(c => c.Row.GetType().Name);
//                var firstItemUser = itemsByType.First().First();

//                if (!_dic.TryGetValue(firstItemUser.Team.Name, out _datas))
//                    _dic.Add(firstItemUser.Team.Name, (_datas = new datas() { Team = firstItemUser.Team }));

//                foreach (var item2 in itemsByType)
//                {
//                    var lst1 = item2.Cast<object>().ToList();
//                    AppendTitle(item2, lst1);
//                    var i = new changeType() { Name = item2.Key, Items = lst1 };
//                    _datas.Changes.Add(i);
//                }

//            }

//            return _dic.Select(c => c.Value).ToList();

//        }

//        private static void AppendTitle(IGrouping<string, chargeUser> item2, List<object> lst1)
//        {

//            var type = item2.First().Row.GetType().Name;

//            switch (type)
//            {

//                case "GrantLine":
//                    lst1.Insert(0, new chargeUser() { Row = new GrantLine("kind", "Full object name", "Role / Grantee", "Privilege", "Grantable", "sql") });
//                    break;

//                case "ProcedureLine":
//                    lst1.Insert(0, new chargeUser() { Row = new ProcedureLine("kind", "Schema name", "Package name", "Name", "args") });
//                    break;

//                case "SequenceLine":
//                    lst1.Insert(0, new chargeUser() { Row = new SequenceLine("kind", "Schema name", "Package name", "Name", "changes") });
//                    break;

//                case "SynonymLine":
//                    lst1.Insert(0, new chargeUser() { Row = new SynonymLine("kind", "Schema name", "Name", "Object target", "Object type") });
//                    break;

//                case "TypeLine":
//                    lst1.Insert(0, new chargeUser() { Row = new TypeLine("kind", "Schema name", "Package name", "Name", "Collection schema name", "Collection type name") });
//                    break;

//                case "ConstraintLine":
//                    lst1.Insert(0, new chargeUser() { Row = new ConstraintLine("kind", "schema name", "table name", "column", "property changed", "target name", "Type", "Rel owner", "Rel name", "sql1", "sql2") });
//                    break;

//                case "TableLine":
//                    lst1.Insert(0, new chargeUser() { Row = new TableLine("Kind", "Schema name", "Table name", "Property Changed") });
//                    break;

//                case "ColumnLine":
//                    lst1.Insert(0, new chargeUser() { Row = new ColumnLine("kind", "schema name", "table name", "column", "property changed") });
//                    break;

//                case "IndexLine":
//                    lst1.Insert(0, new chargeUser() { Row = new IndexLine("kind", "schema name", "table name", "indexName", "property changed") });
//                    break;

//                case "TriggerLine":
//                    lst1.Insert(0, new chargeUser() { Row = new TriggerLine("kind", "schema name", "table name", "TriggerName", "property changed") });
//                    break;

//                case "ViewLine":
//                    lst1.Insert(0, new chargeUser() { Row = new ViewLine("kind", "schema name", "table name", "schema name", "Table name", "property changed") });
//                    break;

//                case "PackageLine":
//                    lst1.Insert(0, new chargeUser() { Row = new PackageLine("kind", "schema name", "Package name") });
//                    break;

//                case "PackageBodyLine":
//                    lst1.Insert(0, new chargeUser() { Row = new PackageBodyLine("kind", "schema name", "Package body name") });
//                    break;

//                case "DuplicateObjectsyLine":
//                    lst1.Insert(0, new chargeUser() { Row = new DuplicateObjectsyLine("Name", "type", "file 1", "file 2") });
//                    break;

//                default:
//                    break;

//            }
//        }

//        private void GetResponsabilities(DifferenceModels diffs)
//        {
//            if (section != null)
//            {
//                foreach (DifferenceModel dif in diffs.Items)
//                {
//                    var i = dif.Source as ItemBase;
//                    if (i != null)
//                    {
//                        var type = i.GetType().Name.ToUpper();
//                        if (i != null)
//                        {
//                            var schema = i.GetOwner();
//                            if (!string.IsNullOrEmpty(schema))
//                            {
//                                schema = schema.ToUpper();
//                                var _schema = section.Schemas.OfType<SchemaElement>().Where(c => c.Name.ToUpper() == schema).FirstOrDefault();
//                                if (_schema != null)
//                                {

//                                    if (!string.IsNullOrEmpty(_schema.Team))
//                                        dif.Team = _schema.Team;

//                                    string _name = string.Format("{0}.{1}", schema, i.GetName());
//                                    var obj = _schema.Objects.OfType<ObjectElement>().Where(c => c.Type.ToUpper() == type && c.Fullname == _name).FirstOrDefault();
//                                    if (obj == null)
//                                    {


//                                    }

//                                    if (obj != null)
//                                    {
//                                        dif.Team = obj.Team;
//                                    }


//                                    if (dif.Team == null)
//                                    {

//                                    }

//                                }
//                            }
//                        }
//                    }
//                }
//            }

//        }


//        private static FileElement GetFile(ItemBase item)
//        {
//            FileElement result = null;
//            if (item != null)
//            {

//                result = item.Files.OfType<FileElement>().FirstOrDefault();

//                if (result == null)
//                {
//                    if (item is IndexModel)
//                        result = GetFile((item as IndexModel).Parent as ItemBase);

//                    //else if (item is TypeItem)
//                    //    result = (item as TypeItem).Key;

//                    //else if (item is GrantModel)
//                    //    result = (item as GrantModel).Key;

//                    //else if (item is SynonymModel)
//                    //    result = (item as SynonymModel).Key;

//                    else if (item is ConstraintModel)
//                        result = GetFile((item as ConstraintModel).Parent as ItemBase);

//                    //else if (item is SequenceModel)
//                    //    result = (item as SequenceModel).Name;

//                    //else if (item is PackageModel)
//                    //    result = (item as PackageModel).Name;

//                    //else if (item is ProcedureModel)
//                    //    result = (item as ProcedureModel).Name;

//                    else if (item is ColumnModel)
//                        result = GetFile((item as ColumnModel).Parent as ItemBase);

//                    else if (item is TriggerModel)
//                        result = GetFile((item as TriggerModel).Parent as ItemBase);

//                    else
//                    {

//                    }

//                    //else if (item is TableModel)
//                    //    result = (item as TableModel).Key;

//                }

//            }

//            return result;

//        }

//        private void ProcessDuplicateObjects()
//        {
//            foreach (DoublonModel item in this._doublons)
//            {
//                Append(new DuplicateObjectsyLine(item.Source as ItemBase, null, item.PropertyName, item.Type, item.Files.ToArray()), "DATATEAM");
//            }
//        }

//        private void ProcessTable()
//        {

//            foreach (var item in base._tables)
//            {

//                string team = string.Empty;
//                if (item.Value.DifferenceModel != null && !string.IsNullOrEmpty(item.Value.DifferenceModel.Team))
//                    team = item.Value.DifferenceModel.Team;

//                table t = item.Value;
//                if (item.Value.Kind == TypeDifferenceEnum.MissingInTarget || item.Value.Kind == TypeDifferenceEnum.MissingInSource)
//                {
//                    if (t.Source.IsView)
//                        Append(new ViewLine(item.Value.Source, item.Value.Target, ConvertToText(t.Kind), t.Source.Owner, t.Source.Name, string.Empty), item.Value.DifferenceModel.Team);
//                    else
//                        Append(new TableLine(item.Value.Source, item.Value.Target, ConvertToText(t.Kind), t.Source.Owner, t.Source.Name, string.Empty), item.Value.DifferenceModel.Team);
//                }
//                else
//                {

//                    #region tables changes

//                    foreach (var ch1 in item.Value.Changes)
//                    {
//                        switch (ch1.ToLower())
//                        {

//                            //case "blocpartition":                           
//                            //case "code":
//                            //case "InitialExtent":
//                            //case "comment":
//                            //case "tablespacename":
//                            //case "initialextent":
//                            //case "segmentcreated":
//                            //case "nextextent":
//                            //case "minextents":
//                            //case "logging":
//                            //case "rowmovement":
//                            //case "pctfree":
//                            //case "partitioned":
//                            //case "maxtrans":
//                            //case "initrans":
//                            //case "description":
//                            //case "bufferpool":
//                            //case "triggerstatus":
//                            //    break;

//                            case "codeview":
//                                Append(new ViewLine(item.Value.Source, item.Value.Target, ConvertToText(t.Kind), t.Source.Owner, t.Source.Name, ch1), item.Value.DifferenceModel.Team);
//                                break;

//                            default:
//                                if (t.Source.IsView)
//                                    Append(new ViewLine(item.Value.Source, item.Value.Target, ConvertToText(t.Kind), t.Source.Owner, t.Source.Name, ch1), item.Value.DifferenceModel.Team);
//                                else
//                                    Append(new TableLine(item.Value.Source, item.Value.Target, ConvertToText(t.Kind), t.Source.Owner, t.Source.Name, ch1), item.Value.DifferenceModel.Team);
//                                break;
//                        }


//                    }

//                    #endregion tables changes

//                    if (item.Value.Source.IsView)
//                    {

//                    }
//                    else
//                    {
//                        foreach (var c in item.Value.Columns)
//                        {

//                            string _team = team;
//                            if (c.Value.DifferenceModel != null && !string.IsNullOrEmpty(c.Value.DifferenceModel.Team))
//                                _team = c.Value.DifferenceModel.Team;

//                            string sql = string.Empty;      // string.Format("ALTER TABLE {0}.{1} MODIFY {2} ", t.Source.GetOwner(), t.Source.Name, c.Value.Source.ColumnName);

//                            if (c.Value.Kind == TypeDifferenceEnum.MissingInTarget || c.Value.Kind == TypeDifferenceEnum.MissingInSource)
//                                Append(new ColumnLine(c.Value.Source, c.Value.Target, ConvertToText(c.Value.Kind), t.Source.Owner, t.Source.Name, c.Value.Source.Name, sql), _team);

//                            else
//                            {
//                                foreach (var ch in c.Value.Changes)
//                                {
//                                    Append(new ColumnLine(c.Value.Source, c.Value.Target, ConvertToText(c.Value.Kind), t.Source.Owner, t.Source.Name, c.Value.Source.Name, ch), _team);
//                                }
//                            }
//                        }
//                    }

//                    #region constraints

//                    ProcessConstraints(item, t);

//                    #endregion constraints

//                    foreach (var c in item.Value.Indexes)
//                    {

//                        string _team = team;
//                        if (c.Value.DifferenceModel != null && !string.IsNullOrEmpty(c.Value.DifferenceModel.Team))
//                            _team = c.Value.DifferenceModel.Team;

//                        if (c.Value.Kind == TypeDifferenceEnum.MissingInTarget || c.Value.Kind == TypeDifferenceEnum.MissingInSource)
//                            Append(new IndexLine(c.Value.Source, c.Value.Target, ConvertToText(c.Value.Kind), t.Source.Owner, t.Source.Name, c.Value.Source.Name, string.Empty), _team);
//                        else
//                            foreach (var ch in c.Value.Changes)
//                                Append(new IndexLine(c.Value.Source, c.Value.Target, ConvertToText(t.Kind), t.Source.Owner, t.Source.Name, c.Value.Source.Name, ch), _team);
//                    }

//                    foreach (var c in item.Value.Triggers)
//                    {

//                        string _team = team;
//                        if (c.Value.DifferenceModel != null && !string.IsNullOrEmpty(c.Value.DifferenceModel.Team))
//                            _team = c.Value.DifferenceModel.Team;

//                        if (c.Value.Kind == TypeDifferenceEnum.MissingInTarget || c.Value.Kind == TypeDifferenceEnum.MissingInSource)
//                            Append(new TriggerLine(c.Value.Source, c.Value.Target, ConvertToText(t.Kind), t.Source.Owner, t.Source.Name, c.Value.Source.Name, string.Empty), _team);
//                        else
//                            foreach (var ch in c.Value.Changes)
//                                Append(new TriggerLine(c.Value.Source, c.Value.Target, ConvertToText(t.Kind), t.Source.Owner, t.Source.Name, c.Value.Source.Name, ch), _team);
//                    }
//                }
//            }
//        }

//        private void ProcessConstraints(KeyValuePair<string, table> item, table t)
//        {
//            foreach (var c in item.Value.Constraints)
//            {

//                string team = string.Empty;
//                if (item.Value.DifferenceModel != null && !string.IsNullOrEmpty(item.Value.DifferenceModel.Team))
//                    team = item.Value.DifferenceModel.Team;

//                string sqltarget = string.Empty;
//                string sqlsource = string.Empty;

//                if (c.Value.Kind == TypeDifferenceEnum.MissingInTarget)
//                {
//                    Append(new ConstraintLine(c.Value.Source, c.Value.Target, ConvertToText(c.Value.Kind), t.Source.Owner, t.Source.Name, c.Value.Source.Name, string.Empty, string.Empty, string.Empty, c.Value.Source.Reference.Owner, c.Value.Source.Reference.Name, sqltarget, sqlsource), item.Value.DifferenceModel.Team);
//                }
//                else
//                {
//                    //if (c.Ch)
//                    foreach (var ch in c.Value.Changes)
//                    {

//                        string _team = team;
//                        if (c.Value.DifferenceModel != null && !string.IsNullOrEmpty(c.Value.DifferenceModel.Team))
//                            _team = c.Value.DifferenceModel.Team;

//                        string newConstraintName = null;

//                        if (ch == "Name")
//                        {

//                            if (!c.Value.Source.Name.StartsWith("SYS_C"))
//                                newConstraintName = c.Value.Source.Name;

//                            else if (!c.Value.Target.Name.StartsWith("SYS_C"))
//                                newConstraintName = c.Value.Target.Name;

//                            else
//                            {
//                                newConstraintName = string.Format("{0}.{1} {2} {3}",
//                                  t.Source.Owner,
//                                  t.Source.Name,
//                                  c.Value.Source.Type,
//                                  string.Join(", ", c.Value.Source.Columns.OfType<ConstraintColumnModel>().Select(d => d.ColumnName))
//                                );

//                                newConstraintName = "RENAMED_" + Math.Abs(Pssa.Sdk.Crc32.Calculate(newConstraintName)).ToString();

//                            }

//                            if (c.Value.Source.Generated == "GENERATED NAME")
//                            {
//                                sqlsource = string.Format("ALTER TABLE {0}.{1} RENAME CONSTRAINT {2} TO RENAMED_{3}"
//                                    , t.Source.Owner
//                                    , t.Source.Name
//                                    , c.Value.Source.Name
//                                    , newConstraintName
//                                    );

//                            }

//                            if (c.Value.Kind == TypeDifferenceEnum.Change)
//                            {
//                                if (c.Value.Target.Generated == "GENERATED NAME")
//                                {
//                                    sqltarget = string.Format("ALTER TABLE {0}.{1} RENAME CONSTRAINT {2} TO RENAMED_{3}"
//                                       , t.Target.Owner
//                                       , t.Target.Name
//                                       , c.Value.Target.Name
//                                       , newConstraintName
//                                       );
//                                }
//                            }


//                            newConstraintName = c.Value.Source.Name + " renamed to " + c.Value.Target.Name;

//                        }
//                        else
//                            newConstraintName = c.Value.Source.Key;

//                        string ctType = string.Empty;
//                        switch (c.Value.Source.Type.ToUpper())
//                        {
//                            case "C":
//                                ctType = "Check constraint on a table";
//                                var _c = getConstraintText(c);
//                                if (_c.Trim().EndsWith("IS NOT NULL"))
//                                {
//                                    // + string.Join(", ", c.Value.Source.Columns.OfType<ConstraintColumnModel>().Select(d => d.ColumnName).ToList()).Trim(',', ' ')
//                                    ctType = "not null constraint on column";
//                                }
//                                else
//                                {

//                                }
//                                break;
//                            case "P":
//                                ctType = "Primary key";
//                                break;
//                            case "U":
//                                ctType = "Unique key";
//                                break;
//                            case "R":
//                                ctType = "Foreign integrity";
//                                break;
//                            case "V":
//                                ctType = "With check option, on a view";
//                                break;
//                            case "O":
//                                ctType = "With read only, on a view";
//                                break;
//                            case "H":
//                                ctType = "Hash expression";
//                                break;
//                            case "F":
//                                ctType = "Constraint that involves a REF column";
//                                break;
//                            case "S":
//                                ctType = "Supplemental logging";
//                                break;

//                            default:
//                                break;
//                        }

//                        StringBuilder sb = new StringBuilder();
//                        foreach (ConstraintColumnModel col in c.Value.Source.Columns)
//                        {
//                            sb.Append(col.ColumnName);
//                            sb.Append(", ");
//                        }

//                        Append(new ConstraintLine(c.Value.Source, c.Value.Target, ConvertToText(t.Kind), t.Source.Owner, t.Source.Name, sb.ToString().Trim(',', ' '), ch, newConstraintName, ctType, c.Value.Source.Reference.Owner, c.Value.Source.Reference.Name, sqltarget, sqlsource), _team);

//                    }
//                }
//            }

//        }

//        private static string getConstraintText(KeyValuePair<string, constraint> c)
//        {
//            var s = Utils.Unserialize(c.Value.Source.Search_Condition, false).ToUpper();
//            var v = s;
//            while (v != (s = s.Replace("\t", " ").Replace("  ", " ")))
//                v = s;
//            return s;
//        }

//        private void ProcessType()
//        {
//            foreach (var item in base._types)
//            {
//                var t = item.Value;
//                Append(new TypeLine(item.Value.Source, item.Value.Target, ConvertToText(t.Kind), t.Source.Owner, t.Source.PackageName, t.Source.Name, t.Source.CollectionSchemaName, t.Source.CollectionTypeName), item.Value.DifferenceModel.Team);
//            }
//        }

//        private void ProcessSynonyms()
//        {
//            foreach (var item in base._synonyms)
//            {
//                var t = item.Value;
//                Append(new SynonymLine(item.Value.Source, item.Value.Target, ConvertToText(t.Kind), t.Source.Owner, t.Source.Name, $"{t.Source.ObjectTargetOwner}.{t.Source.ObjectTargetName}"), item.Value.DifferenceModel.Team);
//            }
//        }

//        private void ProcesSequences()
//        {

//            foreach (var item in base._sequences)
//            {

//                var t = item.Value;
//                string sql = string.Empty;

//                if (t.Kind == TypeDifferenceEnum.Change)
//                {
//                    sql = string.Format(@"ALTER SEQUENCE {0}.{1} ", t.Source.Owner, t.Source.Name);
//                    foreach (var c1 in t.Changes)
//                    {

//                        switch (c1.ToLower())
//                        {
//                            case "minvalue":
//                                sql += "MINVALUE " + t.Source.MinValue.ToString() + " ";
//                                break;
//                            case "maxvalue":
//                                sql += "MAXVALUE " + t.Source.MaxValue.ToString() + " ";
//                                break;
//                            case "cachesize":
//                                sql += "CACHE " + t.Source.CacheSize.ToString() + " ";
//                                break;
//                            case "cycleflag":
//                                if (t.Target.CycleFlag != t.Source.CycleFlag)
//                                    sql += t.Source.CycleFlag ? "CYCLE " : "NO CYCLE ";
//                                break;
//                            default:
//                                break;
//                        }
//                    }
//                }
//                else if (t.Kind == TypeDifferenceEnum.MissingInSource)
//                {
//                    sql = string.Format(@"DROP SEQUENCE {0}.{1} ", t.Source.Owner, t.Source.Name);
//                }

//                Append(new SequenceLine(item.Value.Source, item.Value.Target, ConvertToText(t.Kind), t.Source.Owner, t.Source.Name, string.Join(", ", t.Changes).Trim(',', ' '), sql), item.Value.DifferenceModel.Team);

//            }
//        }

//        private void ProcessProcedures()
//        {
//            foreach (var item in base._procedures)
//            {
//                var t = item.Value;
//                var args = t.Source.Arguments.OfType<ArgumentModel>().Select(c => string.Format("{0}{1}{2} {3}", c.Out ? "out " : string.Empty, string.IsNullOrEmpty(c.Type.DataType.Owner) ? "" : "." + c.Type.DataType.Owner, c.Type.DataType, c.Name)).ToArray();
//                Append(new ProcedureLine(item.Value.Source, item.Value.Target, ConvertToText(t.Kind), t.Source.GetOwner(), t.Source.PackageName, t.Source.Name, string.Join(", ", args)), item.Value.DifferenceModel.Team);
//            }
//        }

//        private void ProcessPackages()
//        {
//            foreach (var item in base._packages)
//            {
//                var t = item.Value;

//                if (item.Value.Package)
//                    Append(new PackageLine(item.Value.Source, item.Value.Target, ConvertToText(t.Kind), t.Source.Owner, t.Source.Name), item.Value.DifferenceModel.Team);

//                if (item.Value.PackageBody)
//                    Append(new PackageBodyLine(item.Value.Source, item.Value.Target, ConvertToText(t.Kind), t.Source.Owner, t.Source.Name), item.Value.DifferenceModel.Team);

//            }
//        }

//        private void ProcessGrants()
//        {
//            foreach (var item in base._grants)
//            {
//                var t = item.Value;

//                if (!t.Source.ObjectName.StartsWith("BIN$"))
//                {

//                    var teamName = item.Value.DifferenceModel.Team;

//                    string privileges = string.Join(", ", t.Source.Privileges);
//                    string sql;
//                    if (t.Kind == TypeDifferenceEnum.MissingInTarget || t.Kind == TypeDifferenceEnum.Change)
//                        sql = string.Format(@"GRANT {0} ON {1} TO {2}{3};", privileges, t.Source.FullObjectName, t.Source.Role, t.Source.Grantable ? " WITH GRANT OPTION" : string.Empty);
//                    else
//                        sql = string.Format(@"REVOKE {0} FROM {1} TO {2};", privileges, t.Source.FullObjectName, t.Source.Role);

//                    SheetBase row = new GrantLine(item.Value.Source, item.Value.Target, ConvertToText(t.Kind), t.Source.FullObjectName, t.Source.Role, t.Source.Privileges.ToArray(), t.Source.Grantable ? "Grantable" : string.Empty, sql);

//                    Append(row, item.Value.DifferenceModel.Team);

//                }
//            }
//        }


//        List<chargeUser> list = new List<chargeUser>();

//        private void Append(SheetBase row, string teamName)
//        {

//            if (string.IsNullOrEmpty(teamName))
//                teamName = "DATATEAM";
//            TeamElement team = section.Teams[teamName];
//            chargeUser c = new chargeUser() { Team = team, Row = row };
//            list.Add(c);
//        }

//    }

//    public class chargeUser
//    {

//        public SheetBase Row { get; internal set; }
//        public TeamElement Team { get; internal set; }

//    }

//}

