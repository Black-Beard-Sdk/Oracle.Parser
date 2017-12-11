using System.Collections.Generic;

namespace Bb.Oracle.Models
{
    public interface IEvaluateManager
    {

        IEnumerable<Anomaly> Evaluate(TableModel m);


        IEnumerable<Anomaly> Evaluate(ColumnModel c);


        IEnumerable<Anomaly> Evaluate(ConstraintModel c);


        IEnumerable<Anomaly> Evaluate(TriggerModel t);


        IEnumerable<Anomaly> Evaluate(IndexModel i);


        IEnumerable<Anomaly> Evaluate(SequenceModel s);


        IEnumerable<Anomaly> Evaluate(GrantModel g);


        IEnumerable<Anomaly> Evaluate(PackageModel p);


        IEnumerable<Anomaly> Evaluate(TypeItem t);


        IEnumerable<Anomaly> Evaluate(ProcedureModel p);

        IEnumerable<Anomaly> Evaluate(PropertyModel p);

    }
}