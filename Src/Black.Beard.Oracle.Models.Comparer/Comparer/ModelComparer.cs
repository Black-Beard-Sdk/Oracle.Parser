using Bb.Oracle.Helpers;
using Bb.Oracle.Models;
using Bb.Oracle.Structures.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Bb.Oracle.Models.Comparer
{

    public class ModelComparer
    {

        private CompareContext _context;
        private DifferenceModels _changes;
        private bool SourceScript;

        public ModelComparer()
        {

        }

        public void CompareModels(OracleDatabase source, OracleDatabase target, DifferenceModels _changes, CompareContext context)
        {

            this._context = context ?? new CompareContext();
            this._changes = _changes;
            this.SourceScript = source.SourceScript || target.SourceScript;

            if (!_context.IgnoreTables)
                CompareTables(source.Tables, target.Tables);

            if (!_context.IgnoreGrants)
                CompareGrants(source.Grants, target.Grants, target);

            if (!_context.IgnorePackages)
                ComparePackages(source.Packages, target.Packages);

            //if (!_context.IgnorePartitions)
            //    ComparePartitions(source.Partitions, target.Partitions);

            if (!_context.IgnoreProcedures)
                CompareProcedures(source.Procedures, target.Procedures);

            if (!_context.IgnoreSequences)
                CompareSequences(source.Sequences, target.Sequences);

            if (!_context.IgnoreSynonyms)
                CompareSynonyms(source.Synonyms, target.Synonyms);

            if (!_context.IgnoreTypes)
                CompareTypes(source.Types, target.Types);

        }

        private void CompareTypes(TypeCollection sources, TypeCollection targets)
        {

            var src = sources.OfType<TypeItem>().OrderBy(c => (c.Owner + c.PackageName)).ToList();
            foreach (TypeItem source in src)
            {

                TypeItem target;
                if (!targets.TryGet(source.Key, out target))
                    _changes.AppendMissing(source);
                else
                    CompareType(source, target);
            }

            if (!this._context.IgnoreRevert)
            {
                src = targets.OfType<TypeItem>().OrderBy(c => (c.Owner + c.PackageName)).ToList();
                foreach (TypeItem source in src)
                {
                    TypeItem target = sources[source.Key];
                    if (target == null)
                        _changes.AppendToRemove(source);
                }
            }

        }

        private void CompareType(TypeItem source, TypeItem target)
        {

            EvaluateFiles(source);
            EvaluateFiles(target);

            if (source.SuperType == null)
                source.SuperType = string.Empty;

            if (target.SuperType == null)
                target.SuperType = string.Empty;

            if (source.SuperType.ToUpper().Trim() != target.SuperType.ToUpper().Trim())
                this._changes.AppendChange(source, target, "SuperType");

            if (source.Code != target.Code)
            {

                var s = source.Code.GetSource().Replace(@"""", "");
                s = CleanForCompare(s);

                var t = target.Code.GetSource().Replace(@"""", "");
                t = CleanForCompare(t);

                int index = s.ToUpper().IndexOf("TYPE ");
                if (index > 0)
                    s = s.Substring(index);

                index = t.ToUpper().IndexOf("TYPE ");
                if (index > 0)
                    t = t.Substring(index);

                if (ContentHelper.CompareCodeSources(s, t))
                    this._changes.AppendChange(source, target, "Code");

            }

            if (source.CodeBody != target.CodeBody)
            {

                var s = source.CodeBody.GetSource().Replace(@"""", "");
                var t = target.CodeBody.GetSource().Replace(@"""", "");

                s = CleanForCompare(s);
                t = CleanForCompare(t);

                int index = s.ToUpper().IndexOf("TYPE BODY ");
                if (index > 0)
                    s = s.Substring(index);

                index = t.ToUpper().IndexOf("TYPE BODY ");
                if (index > 0)
                    t = t.Substring(index);

                if (ContentHelper.CompareCodeSources(s, t))
                    this._changes.AppendChange(source, target, "Code body");

            }

            foreach (PropertyModel sourceProp in target.Properties)
            {
                PropertyModel targetProp = target.Properties[sourceProp.Name];
                if (targetProp == null)
                    _changes.AppendMissing(sourceProp);
                else
                    CompareProperty(sourceProp, targetProp);
            }

        }

        private void CompareProperty(PropertyModel source, PropertyModel target)
        {

            if (source.Type.ArrayOfType != target.Type.ArrayOfType)
                this._changes.AppendChange(source, target, "ArrayOfType");

            if (source.Type.DataType != target.Type.DataType)
                this._changes.AppendChange(source, target, "DataType");

            if (source.Type.DataDefault != target.Type.DataDefault)
                this._changes.AppendChange(source, target, "DataDefault");

            if (source.Type.DataLength != target.Type.DataLength)
                this._changes.AppendChange(source, target, "DataLength");

            if (source.Type.DataLevel != target.Type.DataLevel)
                this._changes.AppendChange(source, target, "DataLevel");

            if (source.Type.DataPrecision != target.Type.DataPrecision)
                this._changes.AppendChange(source, target, "DataPrecision");

            if (source.Type.defaultLength != target.Type.defaultLength)
                this._changes.AppendChange(source, target, "defaultLength");

            if (source.Type.IsArray != target.Type.IsArray)
                this._changes.AppendChange(source, target, "IsArray");

            if (source.Type.IsRecord != target.Type.IsRecord)
                this._changes.AppendChange(source, target, "IsRecord");

            if (source.Type.Name != target.Type.Name)
                this._changes.AppendChange(source, target, "TypeName");

        }

        private void CompareSynonyms(SynonymCollection sources, SynonymCollection targets)
        {

            var src = sources.OfType<SynonymModel>().OrderBy(c => c.Name).ToList();
            CompareSynonyms(targets, src, false);

            if (!this._context.IgnoreRevert)
            {
                src = targets.OfType<SynonymModel>().OrderBy(c => c.Name).ToList();
                CompareSynonyms(sources, src, true);
            }

        }

        private void CompareSynonyms(SynonymCollection targets, List<SynonymModel> src, bool toRemove)
        {
            foreach (SynonymModel source in src)
            {

                var trg = targets.OfType<SynonymModel>().Where(c => c.ObjectTargetOwner == source.ObjectTargetOwner).ToList();
                trg = trg.OfType<SynonymModel>().Where(c => c.Name == source.Name).ToList();
                trg = trg.OfType<SynonymModel>().Where(c => c.ObjectTargetName == source.ObjectTargetName).ToList();

                SynonymModel target = null;
                target = trg.FirstOrDefault();

                if (toRemove)
                {
                    if (target == null)
                        _changes.AppendToRemove(source);
                }
                else
                {
                    if (target == null)
                        _changes.AppendMissing(source);
                    else
                        CompareSynonym(source, target);
                }

            }
        }

        private void CompareSynonym(SynonymModel source, SynonymModel target)
        {

            EvaluateFiles(source);
            EvaluateFiles(target);

            if (source.ObjectTargetOwner != target.ObjectTargetOwner)
                this._changes.AppendChange(source, target, "ObjectTargetOwner");

            else if (source.ObjectTargetName != target.ObjectTargetName)
                this._changes.AppendChange(source, target, "ObjectTargetName");

            if (source.IsPublic != target.IsPublic)
                this._changes.AppendChange(source, target, "IsPublic");

        }

        private void CompareProcedures(ProcedureCollection sources, ProcedureCollection targets)
        {

            var src = sources.OfType<ProcedureModel>().Where(c => string.IsNullOrEmpty(c.PackageName)).OrderBy(c => (c.Owner)).ToList();
            CompareProcedures(targets, src, false);

            if (!this._context.IgnoreRevert)
            {
                src = targets.OfType<ProcedureModel>().Where(c => string.IsNullOrEmpty(c.PackageName)).OrderBy(c => (c.Owner)).ToList();
                CompareProcedures(sources, src, true);
            }

        }

        private void CompareProcedures(ProcedureCollection targets, List<ProcedureModel> src, bool toRemove)
        {

            foreach (var item in targets)
                if (item.PackageName == null)
                    item.PackageName = string.Empty;

            foreach (var item in src)
                if (item.PackageName == null)
                    item.PackageName = string.Empty;

            foreach (ProcedureModel source in src)
            {

                List<ProcedureModel> lst = null;
                ProcedureModel target = targets[source.Key];

                if (target == null)
                {

                    // On requete sans s occuper des parametres
                    lst = targets.OfType<ProcedureModel>().Where(c => c.Owner == source.Owner).ToList();
                    lst = lst.OfType<ProcedureModel>().Where(c => source.PackageName == c.PackageName).ToList();
                    lst = lst.OfType<ProcedureModel>().Where(c => c.Name == source.Name).ToList();

                    if (lst.Count == 0)
                    {

                    }
                    else if (lst.Count == 1) // Y en a qu un donc il a changé
                        target = lst.FirstOrDefault();

                    else
                    {

                        if (!this.SourceScript)
                        {

                            var lst2 = lst.OfType<ProcedureModel>().Where(c => CompareArguments(source.Arguments, c.Arguments)).ToList();

                            if (lst2.Count == 1)    // En fait non c est pas compliqué
                                target = lst2.FirstOrDefault();

                        }
                        else
                        {

                            var lst2 = lst.OfType<ProcedureModel>().Where(c => CompareSources(c, source)).ToList();

                            if (lst2.Count == 1)    // En fait non c est pas compliqué
                                target = lst2.FirstOrDefault();

                        }


                    }

                }

                if (toRemove)
                {
                    if (target == null)
                        _changes.AppendToRemove(source);
                }
                else
                {
                    if (target == null)
                        _changes.AppendMissing(source);

                    else
                        CompareProcedure(source, target);
                }

            }
        }

        private bool CompareArguments(ArgumentCollection arguments1, ArgumentCollection arguments2)
        {
            if (arguments1.Count != arguments2.Count)
                return false;

            var a1 = arguments1.ToList();
            var a2 = arguments2.ToList();

            for (int i = 0; i < arguments1.Count; i++)
            {

                var a = a1[i];
                var b = a2[i];

                if (a.Name != b.Name)
                    return false;

                if (a.In != b.In)
                    return false;

                if (a.Out != b.Out)
                    return false;

                if (a.Position != b.Position)
                    return false;

                if (!CompareType(a.Type, b.Type))
                    return false;

            }

            return true;

        }

        private bool CompareType(OracleType source, OracleType target)
        {

            EvaluateFiles(source);
            EvaluateFiles(target);

            if (source.ArrayOfType != target.ArrayOfType)
                return false;

            if (source.DataType != target.DataType)
                return false;

            if (source.DataDefault != target.DataDefault)
                return false;

            if (source.DataLength != target.DataLength)
                return false;

            if (source.DataLevel != target.DataLevel)
                return false;

            if (source.DataPrecision != target.DataPrecision)
                return false;

            if (source.defaultLength != target.defaultLength)
                return false;

            if (source.IsArray != target.IsArray)
                return false;

            if (source.IsRecord != target.IsRecord)
                return false;

            if (source.Name != target.Name)
                return false;

            return true;

        }

        private void CompareProcedure(ProcedureModel source, ProcedureModel target)
        {

            EvaluateFiles(source);
            EvaluateFiles(target);

            if (source.Code != target.Code)
            {
                if (CompareSources(source, target))
                    this._changes.AppendChange(source, target, "Code");
            }
            else
            {

                //foreach (ArgumentModel argSource in source.Arguments)
                //{
                //    ArgumentModel ArgTarget = target.Arguments.OfType<ArgumentModel>().FirstOrDefault(c => c.ArgumentName == argSource.ArgumentName);
                //    if (ArgTarget == null)
                //        _changes.AppendMissing(argSource, source, target);
                //    else
                //        CompareArgument(argSource, ArgTarget, source, target);
                //}

            }

            if (source.Description != target.Description)
                this._changes.AppendChange(source, target, "Description");

        }

        private bool CompareSources(ProcedureModel source, ProcedureModel target)
        {

            string name = source.Name;

            var s = Utils.Unserialize(source.Code, true).Replace(@"""", "");
            s = CleanForCompare(s);

            var t = Utils.Unserialize(target.Code, true).Replace(@"""", "");
            t = CleanForCompare(t);

            // On ne commence a valider qu a partir du nom sinon on va alerter sur tout les boulets qui n'ont pas specifier le nom de schema
            var i = s.ToUpper().IndexOf(name.ToUpper());
            var j = t.ToUpper().IndexOf(name.ToUpper());
            if (i > 0 && j > 0)
            {
                s = s.Substring(i);
                t = t.Substring(j);
            }

            return ContentHelper.CompareCodeSources(s, t);

        }

        private static string CleanForCompare(string s)
        {

            int len = 0;
            while (len != s.Length)
            {
                len = s.Length;
                s = s.Trim().Trim(' ', '\t', '\r', '\n');
                if (s.EndsWith("/"))
                {
                    if (!s.EndsWith("*/"))
                        s = s.Trim('/');
                }
            }

            return s;

        }

        private void CompareArgument(ArgumentModel argSource, ArgumentModel argTarget, ProcedureModel source, ProcedureModel target)
        {

            if (argSource.In != argTarget.In)
                this._changes.AppendChange(argSource, argTarget, "In", source, target);

            if (argSource.Out != argTarget.Out)
                this._changes.AppendChange(argSource, argTarget, "Out", source, target);

            if (argSource.Position != argTarget.Position)
                this._changes.AppendChange(argSource, argTarget, "Position", source, target);

            if (argSource.Type.DataType != argTarget.Type.DataType)
                this._changes.AppendChange(argSource, argTarget, "DataType", source, target);

            if (argSource.Type.DataDefault != argTarget.Type.DataDefault)
                this._changes.AppendChange(argSource, argTarget, "DataDefault", source, target);

            if (argSource.Type.DataLength != argTarget.Type.DataLength)
                this._changes.AppendChange(argSource, argTarget, "DataLength", source, target);

            if (argSource.Type.DataLevel != argTarget.Type.DataLevel)
                this._changes.AppendChange(argSource, argTarget, "DataLevel", source, target);

            if (argSource.Type.DataPrecision != argTarget.Type.DataPrecision)
                this._changes.AppendChange(argSource, argTarget, "DataPrecision", source, target);

            if (argSource.Type.defaultLength != argTarget.Type.defaultLength)
                this._changes.AppendChange(argSource, argTarget, "defaultLength", source, target);

            if (argSource.Type.IsArray != argTarget.Type.IsArray)
                this._changes.AppendChange(argSource, argTarget, "IsArray", source, target);

            if (argSource.Type.IsRecord != argTarget.Type.IsRecord)
                this._changes.AppendChange(argSource, argTarget, "IsRecord", source, target);

            if (argSource.Type.Name != argTarget.Type.Name)
                this._changes.AppendChange(argSource, argTarget, "TypeName", source, target);


        }

        private void CompareSequences(SequenceCollection sources, SequenceCollection targets)
        {

            var src = sources.OfType<SequenceModel>().OrderBy(c => (c.Owner + c.Key)).ToList();

            foreach (SequenceModel source in src)
            {
                SequenceModel target = targets[source.Key];
                if (target == null)
                    _changes.AppendMissing(source);
                else
                    CompareSequence(source, target);

            }

            if (!this._context.IgnoreRevert)
            {
                src = targets.OfType<SequenceModel>().OrderBy(c => (c.Owner + c.Key)).ToList();

                foreach (SequenceModel source in src)
                {
                    SequenceModel target = sources[source.Key];
                    if (target == null)
                        _changes.AppendToRemove(source);

                }
            }

        }

        private void CompareSequence(SequenceModel source, SequenceModel target)
        {

            EvaluateFiles(source);
            EvaluateFiles(target);

            if (source.CacheSize != target.CacheSize)
                this._changes.AppendChange(source, target, "CacheSize");

            if (source.CycleFlag != target.CycleFlag)
                this._changes.AppendChange(source, target, "CycleFlag");

            //if (source.Generated != target.Generated)
            //    this._changes.AppendChange(source, target, "Generated");

            if (source.IncrementBy != target.IncrementBy)
                this._changes.AppendChange(source, target, "IncrementBy");

            if (source.MinValue != target.MinValue)
                this._changes.AppendChange(source, target, "MinValue");

            if (source.OrderFlag != target.OrderFlag)
                this._changes.AppendChange(source, target, "OrderFlag");

            if (source.Status != target.Status)
                this._changes.AppendChange(source, target, "Status");

            if (source.Session != target.Session)
                this._changes.AppendChange(source, target, "Session");

            if (source.MaxValue != target.MaxValue)
                this._changes.AppendChange(source, target, "MaxValue");

        }

        private void ComparePackages(PackageCollection sources, PackageCollection targets)
        {

            var src = sources.OfType<PackageModel>().OrderBy(c => (c.Owner + c.Key)).ToList();

            foreach (PackageModel source in src)
            {
                PackageModel target = targets[source.Key];
                if (target == null)
                    _changes.AppendMissing(source);
                else
                    ComparePackage(source, target);
            }

            if (!this._context.IgnoreRevert)
            {
                src = targets.OfType<PackageModel>().OrderBy(c => (c.Owner + c.Key)).ToList();

                foreach (PackageModel source in src)
                {
                    PackageModel target = sources[source.Key];
                    if (target == null)
                        _changes.AppendToRemove(source);
                }
            }

        }

        private void ComparePackage(PackageModel source, PackageModel target)
        {

            EvaluateFiles(source);
            EvaluateFiles(target);

            if (source.CodeBody != target.CodeBody)
            {

                var s = source.CodeBody.GetSource().Replace(@"""", "");
                s = CleanForCompare(s);

                int index = s.ToUpper().IndexOf("PACKAGE BODY " + source.Owner.ToUpper() + "." + source.Name.ToUpper());
                if (index > 0)
                    s = s.Substring(index);

                var t = target.CodeBody.GetSource().Replace(@"""", "");
                t = CleanForCompare(t);

                index = t.ToUpper().IndexOf("PACKAGE BODY " + source.Owner.ToUpper() + "." + source.Name.ToUpper());
                if (index > 0)
                    t = t.Substring(index);

                if (ContentHelper.CompareCodeSources(s, t))
                    this._changes.AppendChange(source, target, "CodeBody");

                else
                {


                }

            }
            if (source.Code != target.Code)
            {

                var s = source.Code.GetSource().Replace(@"""", "");
                s = CleanForCompare(s);

                int index = s.ToUpper().IndexOf("PACKAGE " + source.Owner.ToUpper() + "." + source.Name.ToUpper());
                if (index > 0)
                    s = s.Substring(index);

                var t = target.Code.GetSource().Replace(@"""", "");
                t = CleanForCompare(t);

                index = t.ToUpper().IndexOf("PACKAGE " + source.Owner.ToUpper() + "." + source.Name.ToUpper());
                if (index > 0)
                    t = t.Substring(index);

                if (ContentHelper.CompareCodeSources(s, t))
                    this._changes.AppendChange(source, target, "Code");

            }
        }

        private void CompareGrants(GrantCollection sources, GrantCollection targets, OracleDatabase _target)
        {

            var src = sources.OfType<GrantModel>().OrderBy(c => c.Role + c.FullObjectName).ToList();
            CompareGrants(targets, _target, src, false);

            if (!this._context.IgnoreRevert)
            {
                src = targets.OfType<GrantModel>().OrderBy(c => c.Role + c.FullObjectName).ToList();
                CompareGrants(sources, _target, src, true);
            }

        }

        private void CompareGrants(GrantCollection targets, OracleDatabase _target, List<GrantModel> src, bool toRemove)
        {
            foreach (GrantModel source in src)
            {

                // default object
                if (source.ObjectName.Contains("$"))
                    continue;

                var s = source.FullObjectName.Replace("\"", "");
                string role = source.Role.ToUpper();
                string schema = source.ObjectSchema.ToUpper();
                string name = source.ObjectName?.ToUpper();

                var trg = targets.OfType<GrantModel>().Where(c => c.Role.ToUpper() == role).ToList();
                trg = trg.OfType<GrantModel>().Where(c => c.ObjectSchema.ToUpper() == schema).ToList();
                trg = trg.OfType<GrantModel>().Where(c => c.ObjectName?.ToUpper() == name).ToList();

                GrantModel target = null;

                if (trg.Count == 1)
                    target = trg.FirstOrDefault();

                else if (trg.Count > 1)
                {
                    if (System.Diagnostics.Debugger.IsAttached)
                        System.Diagnostics.Debugger.Break();
                }

                if (toRemove)
                {
                    if (target == null)
                        _changes.AppendToRemove(source);
                }
                else
                {
                    if (target == null)
                        _changes.AppendMissing(source, _target);
                    else
                        CompareGrant(source, target);
                }
            }
        }

        private void CompareGrant(GrantModel source, GrantModel target)
        {

            EvaluateFiles(source);
            EvaluateFiles(target);

            if (source.Grantable != target.Grantable)
                this._changes.AppendChange(source, target, "Grantable");

            else if (source.Role != target.Role)
                this._changes.AppendChange(source, target, "Role");

            else if (ComparePrivilegesAreChanges(source, target))
                this._changes.AppendChange(source, target, "Privileges");

        }

        private static bool ComparePrivilegesAreChanges(GrantModel source, GrantModel target)
        {

            if (source.Privileges.Count != source.Privileges.Count)
                return true;

            var p1 = new HashSet<string>(source.Privileges.Select(c => c.Name.ToUpper().Replace(" ", "")));
            var p2 = new HashSet<string>(target.Privileges.Select(c => c.Name.ToUpper().Replace(" ", "")));

            foreach (var item in p1)
                if (!p2.Contains(item))
                    return true;

            return false;

        }

        private void CompareTables(TableCollection sources, TableCollection targets)
        {

            var src = sources.OfType<TableModel>().OrderBy(c => (c.Owner + c.Name)).ToList();
            CompareTables(targets, src);

            if (!this._context.IgnoreRevert)
            {
                src = targets.OfType<TableModel>().OrderBy(c => (c.Owner + c.Name)).ToList();
                CompareTables(sources, src, true);
            }

        }

        private void CompareTables(TableCollection targets, List<TableModel> src, bool findToRemove = false)
        {
            foreach (TableModel tableSource in src)
            {

                // default object
                if (tableSource.Name.Contains("$"))
                    continue;

                if (findToRemove)
                {
                    if (!targets.Contains(tableSource.Key))
                        _changes.AppendToRemove(tableSource);
                }
                else
                {
                    TableModel tableTarget;

                    if (targets.TryGet(tableSource.Key, out tableTarget))
                        CompareTable(tableSource, tableTarget, findToRemove);
                    else
                        _changes.AppendMissing(tableSource);
                }
            }
        }

        private void CompareTable(TableModel tableSource, TableModel tableTarget, bool toRemove)
        {

            if (!tableSource.IsView && tableSource.Valid && tableTarget.Valid)
            {
                CompareConstraints(tableSource, tableTarget);
                CompareColumns(tableSource, tableTarget);
            }

            CompareIndexes(tableSource, tableTarget, toRemove);

            CompareTriggers(tableSource, tableTarget);

            EvaluateFiles(tableSource);
            EvaluateFiles(tableTarget);

            if (tableSource.IsView)
                if (tableSource.codeView != tableTarget.codeView)
                {

                    var s = Utils.Unserialize(tableSource.codeView, true).Replace(@"""", "");
                    s = CleanForCompare(s);

                    int index = s.ToUpper().IndexOf("VIEW " + tableSource.Owner.ToUpper() + "." + tableSource.Name.ToUpper());
                    if (index > 0)
                        s = s.Substring(index);

                    var t = Utils.Unserialize(tableTarget.codeView, true).Replace(@"""", "");
                    t = CleanForCompare(t);

                    index = t.ToUpper().IndexOf("VIEW " + tableSource.Owner.ToUpper() + "." + tableSource.Name.ToUpper());
                    if (index > 0)
                        t = t.Substring(index);

                    if (ContentHelper.CompareCodeSources(s, t))
                        this._changes.AppendChange(tableSource, tableTarget, "CodeView");

                    else
                    {


                    }

                }

            //if (!tableSource.IsView && !tableTarget.IsView)
            //{

            //    //if (tableSource.BlocPartition != tableTarget.BlocPartition)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "BlocPartition");

            //    //if (tableTarget.BufferPool == null)
            //    //    tableTarget.BufferPool = "DEFAULT";

            //    //if (tableSource.BufferPool != tableTarget.BufferPool)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "BufferPool");

            //    //if (tableSource.Cache != tableTarget.Cache)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "Cache");

            //    //if (tableSource.CellFlashCache != tableTarget.CellFlashCache)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "CellFlashCache");

            //    //if (tableSource.ClusterName != tableTarget.ClusterName)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "ClusterName");

            //    //if (tableSource.ClusterOwner != tableTarget.ClusterOwner)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "ClusterOwner");

            //    //if (tableSource.Comment != tableTarget.Comment)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "Comment");

            //    //if (tableSource.CompressFor != tableTarget.CompressFor)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "CompressFor");

            //    //if (tableSource.Compression != tableTarget.Compression)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "Compression");

            //    //if (tableSource.Dependencies != tableTarget.Dependencies)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "Dependencies");

            //    //if (tableSource.Description != tableTarget.Description)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "Description");

            //    //if (tableSource.Dropped != tableTarget.Dropped)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "Dropped");

            //    //if (tableSource.Duration != tableTarget.Duration)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "Duration");

            //    //if (tableSource.FlashCache != tableTarget.FlashCache)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "FlashCache");

            //    //if (tableSource.Generated != tableTarget.Generated)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "Generated");

            //    //if (tableSource.GlobalStats != tableTarget.GlobalStats)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "GlobalStats");

            //    //if (tableSource.InitialExtent != tableTarget.InitialExtent)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "InitialExtent");

            //    //if (tableSource.IniTrans != tableTarget.IniTrans)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "IniTrans");

            //    //if (tableSource.Logging != tableTarget.Logging)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "Logging");

            //    //if (tableSource.MaxExtents != tableTarget.MaxExtents)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "MaxExtents");

            //    //if (tableSource.MaxTrans != tableTarget.MaxTrans)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "MaxTrans");

            //    //if (tableSource.MinExtents != tableTarget.MinExtents)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "MinExtents");

            //    //if (tableSource.Monitoring != tableTarget.Monitoring)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "Monitoring");

            //    //if (tableSource.Nested != tableTarget.Nested)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "Nested");

            //    //if (tableSource.NextExtent != tableTarget.NextExtent)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "NextExtent");

            //    //if (tableSource.Partitioned != tableTarget.Partitioned)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "Partitioned");

            //    ////if (tableSource.Partitions != tableTarget.Partitions)
            //    ////    this._changes.AppendChange(tableSource, tableTarget, "Partitions");

            //    //if (tableSource.PctFree != tableTarget.PctFree)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "PctFree");

            //    //if (tableSource.PctUsed != tableTarget.PctUsed)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "PctUsed");

            //    //if (tableSource.ReadOnly != tableTarget.ReadOnly)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "ReadOnly");

            //    //if (tableSource.ResultCache != tableTarget.ResultCache)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "ResultCache");

            //    //if (tableSource.RowMovement != tableTarget.RowMovement)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "RowMovement");

            //    //if (tableSource.Secondary != tableTarget.Secondary)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "Secondary");

            //    //if (tableSource.SegmentCreated != tableTarget.SegmentCreated)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "SegmentCreated");

            //    //if (tableSource.SkipCorrupt != tableTarget.SkipCorrupt)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "SkipCorrupt");

            //    //if (tableSource.Status != tableTarget.Status)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "Status");

            //    //if (tableSource.TableLock != tableTarget.TableLock)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "TableLock");

            //    //if (tableSource.TablespaceName != tableTarget.TablespaceName)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "TablespaceName");

            //    //if (tableSource.Temporary != tableTarget.Temporary)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "Temporary");

            //    //if (tableSource.UserStats != tableTarget.UserStats)
            //    //    this._changes.AppendChange(tableSource, tableTarget, "UserStats");

            //}

            if (tableSource.IsView != tableTarget.IsView)
                this._changes.AppendChange(tableSource, tableTarget, "IsView");
        }

        private void EvaluateFiles(ItemBase item)
        {
            if (item is PackageModel)
            {
                if (item.Files.Count > 2)
                    this._changes.AppendDoublons(typeof(ItemBase).Name.Replace("Model", ""), item, item.GetItemName());
            }
            else if (item.Files.Count > 1)
                this._changes.AppendDoublons(typeof(ItemBase).Name.Replace("Model", ""), item, item.GetItemName());
        }

        private void CompareTriggers(TableModel tableSource, TableModel tableTarget)
        {

            foreach (TriggerModel trigger in tableSource.Triggers)
            {

                if (tableTarget.Triggers.TryGet(trigger.Key, out TriggerModel tTarget))
                    CompareTrigger(trigger, tTarget);
                else
                    _changes.AppendMissing(trigger);
            }

            foreach (TriggerModel trigger in tableTarget.Triggers)
                if (!tableSource.Triggers.Contains(trigger.Key))
                    _changes.AppendToRemove(trigger);

        }

        private void CompareTrigger(TriggerModel source, TriggerModel target)
        {

            EvaluateFiles(source);
            EvaluateFiles(target);

            if (source.ActionType != target.ActionType)
                this._changes.AppendChange(source, target, "ActionType");

            if (source.Code != target.Code)
            {
                var s = Utils.Unserialize(source.Code, true).Trim().Replace(@"""", "");
                s = CleanForCompare(s);

                var t = Utils.Unserialize(target.Code, true).Trim().Replace(@"""", "");
                t = CleanForCompare(t);

                if (ContentHelper.CompareCodeSources(s, t))
                    this._changes.AppendChange(source, target, "Code");

            }

            if (!this.SourceScript)
            {

                if (source.BaseObjectType != target.BaseObjectType)
                    this._changes.AppendChange(source, target, "BaseObjectType");

                if (source.TriggerStatus != target.TriggerStatus)
                    this._changes.AppendChange(source, target, "TriggerStatus");

                if (source.TriggerType != target.TriggerType)
                    this._changes.AppendChange(source, target, "TriggerType");
            }

        }

        private void CompareIndexes(TableModel tableSource, TableModel tableTarget, bool toRemove)
        {

            foreach (IndexModel index in tableSource.Indexes)
            {

                if (!tableTarget.Indexes.TryGet(index.Name, out IndexModel tTarget))
                {

                    string id1 = GetIdentiferColumns(index);
                    var cl = GetColumnListFromIndex(tableTarget, id1).ToList();

                    // Trouve dans la table target, les indexes qui pointent les meme colonnes
                    List<IndexModel> _candidates = new List<IndexModel>();
                    foreach (IndexModel item in cl)
                        if (index.IndexOwner == item.IndexOwner)
                            if (index.Bitmap == item.Bitmap)
                                _candidates.Add(item);

                    if (_candidates.Count == 0)
                    {
                        if (toRemove)
                            _changes.AppendToRemove(index);
                        else
                            _changes.AppendMissing(index);
                    }

                    else if (_candidates.Count == 1)
                    {
                        if (!toRemove)
                            _changes.AppendChange(index, _candidates.First(), "Name");
                    }
                    else
                        _changes.AppendDoublons(nameof(IndexModel), _candidates.ToArray());

                }
                else if (!toRemove)
                    CompareIndex(index, tTarget);
            }
        }

        private void CompareIndex(IndexModel source, IndexModel target)
        {

            EvaluateFiles(source);
            EvaluateFiles(target);

            if (!SourceScript)
            {

                if (source.BlocPartition != target.BlocPartition)
                    this._changes.AppendChange(source, target, "BlocPartition");

                if (source.Logging != target.Logging)
                    this._changes.AppendChange(source, target, "Logging");

                if (source.MaxExtents != target.MaxExtents)
                    this._changes.AppendChange(source, target, "MaxExtents");

                if (source.MinExtents != target.MinExtents)
                    this._changes.AppendChange(source, target, "MinExtents");

                if (source.NextExtents != target.NextExtents)
                    this._changes.AppendChange(source, target, "NextExtents");

                if (source.InitialExtent != target.InitialExtent)
                    this._changes.AppendChange(source, target, "InitialExtent");

                if (source.Tablespace != target.Tablespace)
                    this._changes.AppendChange(source, target, "Tablespace");

                if (source.FreeLists != target.FreeLists)
                    this._changes.AppendChange(source, target, "FreeLists");

            }

            if (source.Bitmap != target.Bitmap)
                this._changes.AppendChange(source, target, "Bitmap");

            if (source.BufferPool != target.BufferPool)
                this._changes.AppendChange(source, target, "BufferPool");

            if (source.Cache != target.Cache)
                this._changes.AppendChange(source, target, "Bitmap");

            if (source.Chunk != target.Chunk)
                this._changes.AppendChange(source, target, "Chunk");


            var c1 = GetIdentiferColumns(source.Columns);
            var c2 = GetIdentiferColumns(target.Columns);
            if (c1 != c2)
            {
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    Debug.WriteLine(c1);
                    Debug.WriteLine(c2);
                }

                c1 = GetIdentiferColumns(source.Columns, true);
                c2 = GetIdentiferColumns(target.Columns, true);

                if (c1 != c2)
                    this._changes.AppendChange(source, target, "Columns", c1, c2);

            }

            if ((source.Compress ?? string.Empty) != (target.Compress ?? string.Empty))
                this._changes.AppendChange(source, target, "Compress");

            if ((source.Compression_Prefix ?? string.Empty) != (target.Compression_Prefix ?? string.Empty))
                this._changes.AppendChange(source, target, "Compression_Prefix");

            if ((source.Deduplication ?? string.Empty) != (target.Deduplication ?? string.Empty))
                this._changes.AppendChange(source, target, "Deduplication");

            if (source.FreeListGroups != target.FreeListGroups)
                this._changes.AppendChange(source, target, "FreeListGroups");

            if ((source.FreePools ?? string.Empty) != (target.FreePools ?? string.Empty))
                this._changes.AppendChange(source, target, "FreePools");

            if ((source.IndexType ?? string.Empty) != (target.IndexType ?? string.Empty))
                this._changes.AppendChange(source, target, "IndexType");

            if (source.In_Row != target.In_Row)
                this._changes.AppendChange(source, target, "In_Row");

            if (source.KindModel != target.KindModel)
                this._changes.AppendChange(source, target, "KindModel");

            if ((source.PctIncrease ?? string.Empty) != (target.PctIncrease ?? string.Empty))
                this._changes.AppendChange(source, target, "PctIncrease");

            if (source.PctVersion != target.PctVersion)
                this._changes.AppendChange(source, target, "PctVersion");

            if (source.SecureFile != target.SecureFile)
                this._changes.AppendChange(source, target, "SecureFile");

            if ((source.SegmentName ?? string.Empty) != (target.SegmentName ?? string.Empty))
                this._changes.AppendChange(source, target, "SegmentName");

            if ((source.TablespaceName ?? string.Empty) != (target.TablespaceName ?? string.Empty))
                this._changes.AppendChange(source, target, "TablespaceName");

            if (source.Unique != target.Unique)
                this._changes.AppendChange(source, target, "Unique");

        }

        private void CompareColumns(TableModel tableSource, TableModel tableTarget)
        {
            foreach (ColumnModel col in tableSource.Columns)
            {

                var colTarget = tableTarget.Columns.OfType<ColumnModel>().Where(c => c.Name == col.Name).FirstOrDefault();

                if (colTarget == null)
                    _changes.AppendMissing(col);

                else if (!tableSource.IsView)
                    CompareColumn(col, colTarget, tableTarget);

            }

        }

        private void CompareColumn(ColumnModel source, ColumnModel target, TableModel tableTarget)
        {

            EvaluateFiles(source);
            EvaluateFiles(target);

            if (!SourceScript)
            {

                if (source.CharactereSetName != target.CharactereSetName)
                    this._changes.AppendChange(source, target, "CharactereSetName");

                if (source.CharUsed != target.CharUsed)
                    this._changes.AppendChange(source, target, "CharUsed");

                if (source.ColumnId != target.ColumnId)
                    this._changes.AppendChange(source, target, "ColumnId");

                if (source.DataUpgrated != target.DataUpgrated)
                    this._changes.AppendChange(source, target, "DataUpgrated");

                if (!tableTarget.IsView)
                    if (source.Description != target.Description)
                        this._changes.AppendChange(source, target, "Description");

                if (source.EncryptionAlg != target.EncryptionAlg)
                    this._changes.AppendChange(source, target, "EncryptionAlg");

                if (source.IntegrityAlg != target.IntegrityAlg)
                    this._changes.AppendChange(source, target, "IntegrityAlg");

                if (source.Salt != target.Salt)
                    this._changes.AppendChange(source, target, "Salt");

            }

            if (!tableTarget.IsView)
            {

                if (source.IsComputed != target.IsComputed)
                    this._changes.AppendChange(source, target, "IsComputed");

                if (source.IsPrimaryKey != target.IsPrimaryKey)
                    this._changes.AppendChange(source, target, "IsPrimaryKey");


                if (EvaluateIfNullable(source) != EvaluateIfNullable(target))
                {

                    this._changes.AppendChange(source, target, "Nullable");

                }

                CompareColumnType(source, target, tableTarget);

                if (!string.IsNullOrEmpty(source.ForeignKey.ConstraintName))
                    this._changes.AppendChange(source, target, "ConstraintName");
            }

        }

        private static bool EvaluateIfNullable(ColumnModel source)
        {

            bool result = source.Nullable;
            if (!result)
            {
                foreach (ConstraintModel item in source.Constraints)
                {
                    if (!string.IsNullOrEmpty(item.Search_Condition))
                    {

                        var i = Utils.Unserialize(item.Search_Condition, false);

                        if (i.EndsWith("NOT NULL"))
                        {
                            if (item.Status == "DISABLED")
                                result = true;
                        }

                    }
                }
            }

            return result;

        }

        private void CompareColumnType(ColumnModel source, ColumnModel target, TableModel tableTarget)
        {

            CompareType(source, target);

            CompareDataDefault(source, target);

            if (source.Type.DataLength != target.Type.DataLength)
                this._changes.AppendChange(source, target, "DataLength");

            if (source.Type.DataLevel != target.Type.DataLevel)
                this._changes.AppendChange(source, target, "DataLevel");

            if (source.Type.DataPrecision != target.Type.DataPrecision)
                this._changes.AppendChange(source, target, "DataPrecision");

            if (source.Type.IsArray != target.Type.IsArray)
                this._changes.AppendChange(source, target, "IsArray");

            if (source.Type.IsRecord != target.Type.IsRecord)
                this._changes.AppendChange(source, target, "IsRecord");

            if (source.Type.Name != target.Type.Name)
                this._changes.AppendChange(source, target, "TypeName");

        }

        private void CompareType(ColumnModel source, ColumnModel target)
        {
            if (source.Type.DataType != target.Type.DataType)
                this._changes.AppendChange(source, target, "DataType");
            else
            {
                switch (source.Type.DataType)
                {

                    case "VARCHAR":
                    case "NVARCHAR":
                    case "VARCHAR2":
                    case "NVARCHAR2":

                        if (source.CharUsed != target.CharUsed)
                            this._changes.AppendChange(source, target, "UnitInChar");

                        break;

                    default:
                        break;
                }
            }
        }

        private void CompareDataDefault(ColumnModel source, ColumnModel target)
        {

            source.Type.DataDefault = source.Type.DataDefault.Trim().ToUpper();
            target.Type.DataDefault = target.Type.DataDefault.Trim().ToUpper();

            if (source.Type.DataDefault != target.Type.DataDefault)
            {

                int r;
                if (int.TryParse(source.Type.DataDefault, out r))
                    source.Type.DataDefault = r.ToString();

                if (int.TryParse(target.Type.DataDefault, out r))
                    target.Type.DataDefault = r.ToString();

                if (!source.Root.SourceScript)
                {
                    if (!source.Nullable && source.Type.DataDefault == "NULL")
                    {
                        source.Type.DataDefault = string.Empty;
                        source.Type.defaultLength = 0;
                    }
                }

                if (!target.Root.SourceScript)
                {
                    if (!target.Nullable && target.Type.DataDefault == "NULL")
                    {
                        target.Type.DataDefault = string.Empty;
                        target.Type.defaultLength = 0;
                    }
                }

                //if (string.IsNullOrEmpty(source.Type.DataDefault) && source.Nullable)
                //    source.Type.DataDefault = "NULL";

                //else if (!string.IsNullOrEmpty(target.Type.DataDefault) && !target.Nullable)
                //    target.Type.DataDefault = string.Empty;

                //if (!string.IsNullOrEmpty(source.Type.DataDefault) && !source.Nullable)
                //    source.Type.DataDefault = string.Empty;

                //else if (string.IsNullOrEmpty(target.Type.DataDefault) && target.Nullable)
                //    target.Type.DataDefault = "NULL";

                if (source.Type.DataDefault != target.Type.DataDefault)
                {
                    this._changes.AppendChange(source, target, "DataDefault");
                }

            }
        }

        private void CompareConstraints(TableModel tableSource, TableModel tableTarget)
        {

            EvaluateFiles(tableSource);
            EvaluateFiles(tableTarget);

            foreach (ConstraintModel constrSource in tableSource.Constraints)
            {

                if (tableTarget.Constraints.TryGet(constrSource.Key, out ConstraintModel constrTarget))
                    CompareConstraint(constrSource, constrTarget);

                else
                {
                    bool t = false;
                    string id1 = GetIdentiferColumns(constrSource);

                    var cl = GetColumnListFromConstraint(tableTarget, id1).ToList();

                    foreach (ConstraintModel item in cl)
                        if (constrSource.Owner == item.Owner)
                            if (constrSource.Type == item.Type)

                                if (constrSource.Rel_Constraint_Owner == item.Rel_Constraint_Owner)
                                    if (constrSource.Rel_Constraint_Name == item.Rel_Constraint_Name)
                                    {
                                        _changes.AppendChange(constrSource, item, "Name");
                                        t = true;
                                    }

                    if (!t)
                        _changes.AppendMissing(constrSource);

                }

            }
        }

        private void CompareConstraint(ConstraintModel source, ConstraintModel target)
        {

            EvaluateFiles(source);
            EvaluateFiles(target);

            
            if (GetIdentiferColumns(source) != GetIdentiferColumns(target))
                this._changes.AppendChange(source, target, "Columns");

            if (source.Deferred != source.Deferred)
                this._changes.AppendChange(source, target, "Deferred");

            if (source.Deferrable != source.Deferrable)
                this._changes.AppendChange(source, target, "Deferrable");

            if (source.DeleteRule != source.DeleteRule)
                this._changes.AppendChange(source, target, "DeleteRule");

            if (source.Generated != source.Generated)
                this._changes.AppendChange(source, target, "Generated");

            if (source.IndexName != source.IndexName)
                this._changes.AppendChange(source, target, "IndexName");

            if (source.Reference != source.Reference)
                this._changes.AppendChange(source, target, "Reference");

            if (source.Rely != source.Rely)
                this._changes.AppendChange(source, target, "Rely");

            if (source.Rel_Constraint_Name != source.Rel_Constraint_Name)
                this._changes.AppendChange(source, target, "Rel_Constraint_Name");

            if (source.Rel_Constraint_Owner != source.Rel_Constraint_Owner)
                this._changes.AppendChange(source, target, "Rel_Constraint_Owner");

            if (source.Search_Condition != source.Search_Condition)
                this._changes.AppendChange(source, target, "Search_Condition");

            if (source.Status != source.Status)
                this._changes.AppendChange(source, target, "Status");

            if (source.Type != source.Type)
                this._changes.AppendChange(source, target, "Type");

            if (source.Validated != source.Validated)
                this._changes.AppendChange(source, target, "Validated");

            if (source.ViewRelated != source.ViewRelated)
                this._changes.AppendChange(source, target, "ViewRelated");

        }

        //private void RenameConstraint(TableModel tableSource, TableModel tableTarget, ConstraintModel constrSource, string id1, ConstraintModel item)
        //{
        //    string newName = "TO_RENAME_" + (string.Format("{0}_{1}_", constrSource.Type, tableTarget.Key.Replace(".", "_"), id1)).GetHashCode();

        //    if (constrSource.Generated != "GENERATED NAME")
        //        newName = constrSource.Name;
        //    else if (item.Generated != "GENERATED NAME")
        //        newName = item.Name;

        //    if (constrSource.Generated == "GENERATED NAME")
        //    {
        //        //OutputDbSource.AppendFormat("ALTER TABLE {0} RENAME CONSTRAINT {1} TO {2}", tableSource.Key, constrSource.Name, newName.Trim('_'));
        //        //OutputDbSource.AppendLine(string.Empty);
        //    }

        //    if (item.Generated == "GENERATED NAME")
        //    {
        //        //OutputDbTarget.AppendFormat("ALTER TABLE {0} RENAME CONSTRAINT {1} TO {2}", tableSource.Key, item.Name, newName.Trim('_'));
        //        //OutputDbTarget.AppendLine(string.Empty);
        //    }
        //}

        private static IEnumerable<IndexModel> GetColumnListFromIndex(TableModel tableTarget, string id1)
        {
            foreach (IndexModel indexTarget in tableTarget.Indexes)
            {
                string sr2 = GetIdentiferColumns(indexTarget);
                if (id1 == sr2)
                    yield return indexTarget;
            }

        }

        private static IEnumerable<ConstraintModel> GetColumnListFromConstraint(TableModel tableTarget, string id1)
        {
            foreach (ConstraintModel constrTarget1 in tableTarget.Constraints)
            {
                string sr2 = GetIdentiferColumns(constrTarget1);
                if (id1 == sr2)
                    yield return constrTarget1;
            }
        }

        private static string GetIdentiferColumns(ConstraintModel constraint)
        {

            var cl = constraint.Columns.OfType<ConstraintColumnModel>().ToList();

            StringBuilder s1 = new StringBuilder();
            foreach (string item in cl.Select(c => c.ColumnName).OrderBy(c => c))
            {
                s1.Append(item);
                s1.Append("_");
            }

            string sr1 = s1.ToString().Trim('_');

            Trace.WriteLine(constraint.Key + " => " + sr1);

            return sr1;
        }

        private static string GetIdentiferColumns(IndexModel index)
        {

            var cl = index.Columns.OfType<IndexColumnModel>().ToList();

            StringBuilder s1 = new StringBuilder();
            foreach (string item in cl.Select(c => c.Name).OrderBy(c => c))
            {
                s1.Append(item);
                s1.Append("_");
            }

            string sr1 = s1.ToString().Trim('_');

            //Debug.WriteLine(index.Name + " => " + sr1);

            return sr1;
        }

        private static string GetIdentiferColumns(IndexColumnCollection columns, bool compareName = false)
        {

            var cl = columns.OfType<IndexColumnModel>().OrderBy(c => c.Rule).ToList();

            StringBuilder s1 = new StringBuilder();
            foreach (IndexColumnModel item in cl)
            {
                if (compareName)
                    s1.Append(item.Name.Replace(@"""", ""));
                else
                    s1.Append(item.Rule.Replace(@"""", ""));
                s1.Append(item.Asc ? " ASC" : " DESC");
                s1.Append(", ");
            }

            string sr1 = s1.ToString().Trim(',', ' ');
            return sr1;

        }

    }

}
