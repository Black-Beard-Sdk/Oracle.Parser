using System;
using System.Collections.Generic;
using System.Linq;

namespace Bb.Oracle.Structures.Models
{

    public class ModelParser
    {

        private Action<string> _log;

        public ModelParser()
        {
            
        }

        public void Visit(OracleDatabase model, IOracleModelVisitor visitor, Action<string> _log)
        {

            this._log = _log;
            HashSet<string> _schemas = new HashSet<string>();

            if (!string.IsNullOrEmpty(model.AvailableOwner))
                _schemas = new HashSet<string>(model.AvailableOwner.Split(';').ToList());

            else
            {

                Func<string, bool> fnc = c => !string.IsNullOrWhiteSpace(c) && c.Split('\t', ' ', '\r', '\n').Count() == 1;
                
                var t = model.Packages.OfType<PackageModel>().Select(c => c.GetOwner()).Where(c => fnc(c));
                foreach (string item in t)
                    _schemas.Add(item);

                t = model.Synonyms.OfType<SynonymModel>().Select(c => c.Name).Where(c => fnc(c));
                foreach (string item in t)
                    _schemas.Add(item);

                t = model.Synonyms.OfType<SynonymModel>().Select(c => c.ObjectTargetName.Split('.')[0]).Where(c => fnc(c));
                foreach (string item in t)
                    _schemas.Add(item);

                t = model.Grants.OfType<GrantModel>().Select(c => c.GetOwner()).Where(c => fnc(c));
                foreach (string item in t)
                    _schemas.Add(item);

                t = model.Procedures.OfType<ProcedureModel>().Select(c => c.GetOwner()).Where(c => fnc(c));
                foreach (string item in t)
                    _schemas.Add(item);

                t = model.Sequences.OfType<SequenceModel>().Select(c => c.GetOwner()).Where(c => fnc(c));
                foreach (string item in t)
                    _schemas.Add(item);

                t = model.Tables.OfType<TableModel>().Select(c => c.GetOwner()).Where(c => fnc(c));
                foreach (string item in t)
                    _schemas.Add(item);

                t = model.Types.OfType<TypeItem>().Select(c => c.GetOwner()).Where(c => fnc(c));
                foreach (string item in t)
                    _schemas.Add(item);
                
            }


            var schemas = _schemas.Select(c => new SchemaModel() { Name = c.ToUpper(), Parent = model });
            this._log(string.Format("--- analyzing {0} schema name(s)", schemas.Count()));
            foreach (var item in schemas)
                visitor.VisitSchema(item);

            this._log(string.Format("--- analyzing {0} grant(s)", model.Grants.OfType<GrantModel>().Count()));
            foreach (GrantModel item in model.Grants)
                visitor.VisitGrant(item);

            this._log(string.Format("--- analyzing {0} package(s)", model.Packages.OfType<PackageModel>().Count()));
            foreach (PackageModel item in model.Packages)
                visitor.VisitPackage(item);

            this._log(string.Format("--- analyzing {0} partition(s)", model.Partitions.OfType<PartitionModel>().Count()));
            foreach (PartitionModel item in model.Partitions)
                visitor.VisitPartition(item);


            this._log(string.Format("--- analyzing {0} procedure(s)", model.Procedures.OfType<ProcedureModel>().Count()));
            foreach (ProcedureModel item in model.Procedures)
            {
                visitor.VisitProcedure(item);
                foreach (ArgumentModel item2 in item.Arguments)
                    visitor.VisitArgument(item2);
            }


            this._log(string.Format("--- analyzing {0} sequences(s)", model.Sequences.OfType<SequenceModel>().Count()));
            foreach (SequenceModel item in model.Sequences)
                visitor.VisitSequence(item);


            this._log(string.Format("--- analyzing {0} synonym(s)", model.Synonyms.OfType<SynonymModel>().Count()));
            foreach (SynonymModel item in model.Synonyms)
                visitor.VisitSynonym(item);


            this._log(string.Format("--- analyzing {0} tables(s)", model.Tables.OfType<TableModel>().Count()));
            foreach (TableModel item in model.Tables)
            {

                visitor.VisitTable(item);
                foreach (ColumnModel item2 in item.Columns)
                    visitor.VisitColumn(item2);

                foreach (ConstraintModel item2 in item.Constraints)
                {
                    visitor.VisitConstraint(item2);
                    foreach (ConstraintColumnModel item3 in item2.Columns)
                        visitor.VisitConstraintColumn(item3);
                }

                foreach (IndexModel item2 in item.Indexes)
                {
                    visitor.VisitIndex(item2);
                    foreach (IndexColumnModel item3 in item2.Columns)
                        visitor.VisitIndexColumn(item3);
                }

                foreach (PartitionRefModel item2 in item.Partitions)
                    visitor.VisitPartitionRef(item2);

                foreach (TriggerModel item2 in item.Triggers)
                    visitor.VisitTrigger(item2);

            }

            this._log(string.Format("--- analyzing {0} tablespace(s)", model.Tablespaces.OfType<TablespaceModel>().Count()));
            foreach (TablespaceModel item in model.Tablespaces)
                visitor.VisitTablespace(item);


            this._log(string.Format("--- analyzing {0} type(s)", model.Packages.OfType<TypeItem>().Count()));
            foreach (TypeItem item in model.Types)
                visitor.VisitType(item);


        }


    }

}
