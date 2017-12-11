namespace Bb.Oracle.Models
{

    public enum policyEnum
    {
        DeleteBefore,
        Append,
    }

    public interface IchangeVisitor
    {

        void Write(System.IO.FileInfo file, policyEnum policy);

        void Create(TableModel m);

        void Drop(TableModel m);

        void Alter(TableModel m, TableModel fromSource, string propertyName);


        void Create(ColumnModel c);
        void Drop(ColumnModel c);

        void Alter(ColumnModel c, ColumnModel fromSource, string propertyName);



        void Create(ConstraintModel c);

        void Drop(ConstraintModel c);



        void Create(TriggerModel t);

        void Drop(TriggerModel t);

        void Alter(TriggerModel t, TriggerModel source, string propertyName);



        void Create(IndexModel i);

        void Drop(IndexModel i);



        void Create(SequenceModel s);

        void Drop(SequenceModel s);

        void Alter(SequenceModel sequenceModel, SequenceModel sequenceSource, string propertyName);




        void Create(GrantModel g);

        void Drop(GrantModel g);

        void Alter(GrantModel g, GrantModel grantSource, string propertyName);




        void Create(PackageModel p);

        void Drop(PackageModel p);

        void Alter(PackageModel p, PackageModel sourcePackage, string propertyName);




        void Create(TypeItem t);

        void Drop(TypeItem t);

        void Alter(TypeItem t, TypeItem typeSource, string propertyName);




        void Create(ProcedureModel p);

        void Drop(ProcedureModel p);

        void Alter(ProcedureModel p, ProcedureModel procedureSource, string propertyName);



        void Alter(PropertyModel p, PropertyModel fromSource, string propertyName);
        void Create(PropertyModel propertyModel);
        void Drop(PropertyModel propertyModel);

        IScriptChangeEvaluator Evaluator { get; set; }


        void StartBlock(string message);

        void EndBlock(string message);

        void ByPassedBlock(string message);

        void Clear();

    }


}