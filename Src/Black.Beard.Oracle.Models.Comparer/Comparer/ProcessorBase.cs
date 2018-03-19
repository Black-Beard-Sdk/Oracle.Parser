using Bb.Oracle.Models;
using Bb.Oracle.Structures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Oracle.Models.Comparer
{

    public class ProcessorBase
    {


        public virtual void Run(DifferenceModels diffs, string rootSource, string rootTarget)
        {
            diffs.Run(this);
        }

        internal void Run(List<DifferenceModel> _lst)
        {
            foreach (DifferenceModel item in _lst)
            {
                if (item is DoublonModel)
                {
                    VisitDuplicateFile(item as DoublonModel);
                }
                else
                    VisitBase(item.Source, item.Target, item.Kind, item.PropertyName, item);
            }
        }

        private void VisitBase(object source, object target, TypeDifferenceEnum kind, string propertyName, DifferenceModel item)
        {
            if (source is TypeItem)
                Visit(source as TypeItem, target as TypeItem, kind, propertyName, item);

            else if (source is SynonymModel)
                Visit(source as SynonymModel, target as SynonymModel, kind, propertyName, item);

            else if (source is ProcedureModel)
                Visit(source as ProcedureModel, target as ProcedureModel, kind, propertyName, item);

            else if (source is SequenceModel)
                Visit(source as SequenceModel, target as SequenceModel, kind, propertyName, item);

            else if (source is PackageModel)
                Visit(source as PackageModel, target as PackageModel, kind, propertyName, item);

            else if (source is GrantModel)
                Visit(source as GrantModel, target as GrantModel, kind, propertyName, item);

            else if (source is ConstraintModel)
                Visit(source as ConstraintModel, target as ConstraintModel, kind, propertyName, item);

            else if (source is PropertyModel)
                Visit(source as PropertyModel, target as PropertyModel, kind, propertyName, item);

            else if (source is ColumnModel)
                Visit(source as ColumnModel, target as ColumnModel, kind, propertyName, item);

            else if ( source is TableModel)
                Visit(source as TableModel, target as TableModel, kind, propertyName, item);

            else if ( source is ArgumentModel)
                Visit(source as ArgumentModel, target as ArgumentModel, kind, propertyName, item);

            else if (source is IndexModel)
                Visit(source as IndexModel, target as IndexModel, kind, propertyName, item);

            else if (source is TriggerModel)
                Visit(source as TriggerModel, target as TriggerModel, kind, propertyName, item);


        }


        protected virtual void VisitDuplicateFile(DoublonModel doublonModel)
        {
            throw new NotImplementedException();
        }

        protected virtual void Visit(TriggerModel source, TriggerModel target, TypeDifferenceEnum kind, string propertyName, DifferenceModel item)
        {

        }

        protected virtual void Visit(IndexModel source, IndexModel target, TypeDifferenceEnum kind, string propertyName, DifferenceModel item)
        {
            
        }

        protected virtual void Visit(ArgumentModel source, ArgumentModel target, TypeDifferenceEnum kind, string propertyName, DifferenceModel item)
        {

        }

        protected virtual void Visit(TableModel source, TableModel target, TypeDifferenceEnum kind, string propertyName, DifferenceModel item)
        {

        }

        protected virtual void Visit(ColumnModel source, ColumnModel target, TypeDifferenceEnum kind, string propertyName, DifferenceModel item)
        {

        }

        protected virtual void Visit(PropertyModel source, PropertyModel target, TypeDifferenceEnum kind, string propertyName, DifferenceModel item)
        {

        }

        protected virtual void Visit(ConstraintModel source, ConstraintModel target, TypeDifferenceEnum kind, string propertyName, DifferenceModel item)
        {

        }

        protected virtual void Visit(GrantModel source, GrantModel target, TypeDifferenceEnum kind, string propertyName, DifferenceModel item)
        {

        }

        protected virtual void Visit(PackageModel source, PackageModel target, TypeDifferenceEnum kind, string propertyName, DifferenceModel item)
        {

        }

        protected virtual void Visit(SequenceModel source, SequenceModel target, TypeDifferenceEnum kind, string propertyName, DifferenceModel item)
        {

        }

        protected virtual void Visit(ProcedureModel source, ProcedureModel target, TypeDifferenceEnum kind, string propertyName, DifferenceModel item)
        {

        }

        protected virtual void Visit(SynonymModel source, SynonymModel target, TypeDifferenceEnum kind, string propertyName, DifferenceModel item)
        {

        }

        protected virtual void Visit(TypeItem source, TypeItem target, TypeDifferenceEnum kind, string propertyName, DifferenceModel item)
        {

        }
      
    }

}
