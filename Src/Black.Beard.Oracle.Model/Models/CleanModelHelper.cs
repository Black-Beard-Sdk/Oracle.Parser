using System.Collections.Generic;
using System.Linq;

namespace Bb.Oracle.Models
{

    public class CleanModelHelper : IOracleModelVisitor
    {
        private readonly ModelParser parser;
        private readonly string path;
        private readonly OracleDatabase target;

        private CleanModelHelper(string path, OracleDatabase target, ModelParser parser)
        {
            this.path = path.ToUpper();
            this.target = target;
            this.parser = parser;
        }

        public static void Clean(string path, OracleDatabase target)
        {
            ModelParser parser = new ModelParser();
            CleanModelHelper c = new CleanModelHelper(path, target, parser);
            c.Visit();
        }

        public void VisitArgument(ArgumentModel item)
        {

        }

        public void VisitColumn(ColumnModel item)
        {
            // Action dans le parser ???
            if (InFile(item))
            {

            }
        }

        public void VisitConstraint(ConstraintModel item)
        {
            if (InFile(item))
            {

            }
        }

        public void VisitConstraintColumn(ConstraintColumnModel item)
        {

        }

        public void VisitGrant(GrantModel item)
        {
            if (InFile(item))
                this._sequenceToDelete.Add(item);
        }

        public void VisitIndex(IndexModel item)
        {
            if (InFile(item))
            {

            }
        }

        public void VisitIndexColumn(IndexColumnModel item)
        {

        }

        public void VisitPackage(PackageModel item)
        {
            if (InFile(item))
                this._sequenceToDelete.Add(item);
        }

        public void VisitPartition(PartitionModel item)
        {
            if (InFile(item))
            {

            }

        }

        public void VisitPartitionRef(PartitionRefModel item)
        {
            if (InFile(item))
            {

            }
        }

        public void VisitProcedure(ProcedureModel item)
        {
            if (InFile(item))
            {

            }
        }

        public void VisitSchema(SchemaModel item)
        {

        }

        public void VisitSequence(SequenceModel item)
        {
            if (InFile(item))
                _sequenceToDelete.Add(item);
        }

        public void VisitSynonym(SynonymModel item)
        {
            if (InFile(item))
                _sequenceToDelete.Add(item);
        }

        public void VisitTable(TableModel item)
        {
            if (InFile(item))
            {

                item.Columns.Clear();

            }
        }

        public void VisitTablespace(TablespaceModel item)
        {
            if (InFile(item))
            {

            }
        }

        public void VisitTrigger(TriggerModel item)
        {
            if (InFile(item))
            {

            }
        }

        public void VisitType(TypeItem item)
        {
            if (InFile(item))
                _sequenceToDelete.Add(item);
        }

        private bool InFile(ItemBase item)
        {
            return item.Files.OfType<FileElement>().Where(c => c.Path.ToUpper() == this.path).Any();
        }

        private void Visit()
        {

            this.parser.Visit(this.target, this, a => { });


            foreach (var item in _sequenceToDelete)
            {

                if (item is SequenceModel s)
                    target.Sequences.Remove(s);

                else if (item is PackageModel p)
                    target.Packages.Remove(p);

                else if (item is SynonymModel sy)
                    target.Synonymes.Remove(sy);

                else if (item is TypeItem t)
                    target.Types.Remove(t);

                else if (item is GrantModel g)
                    target.Grants.Remove(g);

                else
                {

                }

            }

        }

        private List<ItemBase> _sequenceToDelete = new List<ItemBase>();

    }


}
