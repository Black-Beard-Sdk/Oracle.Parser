using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Bb.Oracle.Models
{

    public abstract class ChangeVisitorBase : IchangeVisitor
    {


        #region tables

        public abstract void Create(TableModel m);

        // Evaluate(tableSource, tableTarget, t => t.Columns);
        // Evaluate(tableTarget.Constraints, null);


        public abstract void Drop(TableModel m);

        public abstract void Alter(TableModel m, TableModel fromSource, string propertyName);

        #endregion tables

        #region Columns

        public abstract void Create(ColumnModel c);
        public abstract void Drop(ColumnModel c);
        public abstract void Alter(ColumnModel c, ColumnModel fromSource, string propertyName);

        #endregion Columns

        #region Constraints

        public abstract void Create(ConstraintModel c);

        public abstract void Drop(ConstraintModel c);

        #endregion Constraints

        #region Triggers

        public abstract void Create(TriggerModel triggerModel);

        public abstract void Drop(TriggerModel triggerModel);

        public abstract void Alter(TriggerModel t, TriggerModel source, string propertyName);

        #endregion Triggers

        #region indexes

        public abstract void Create(IndexModel indexModel);

        public abstract void Drop(IndexModel indexModel);

        #endregion indexes

        #region sequences

        public abstract void Create(SequenceModel s);

        public abstract void Drop(SequenceModel s);

        public abstract void Alter(SequenceModel s, SequenceModel sequenceSource, string propertyName);

        #endregion sequences

        #region Grants

        public abstract void Create(GrantModel g);

        public abstract void Drop(GrantModel g);

        public abstract void Alter(GrantModel g, GrantModel grantSource, string propertyName);

        #endregion Grants

        #region Packages

        public abstract void Create(PackageModel p);

        public abstract void Drop(PackageModel p);

        public abstract void Alter(PackageModel p, PackageModel sourcePackage, string propertyName);

        #endregion Packages

        #region Types

        public abstract void Create(TypeItem t);

        public abstract void Drop(TypeItem t);

        public abstract void Alter(TypeItem t, TypeItem typeSource, string propertyName);

        #endregion Types

        #region Procedures

        public abstract void Create(ProcedureModel p);

        public abstract void Drop(ProcedureModel p);

        public abstract void Alter(ProcedureModel p, ProcedureModel procedureSource, string propertyName);

        #endregion Procedures

        #region Properties

        public abstract void Create(PropertyModel p);
        public abstract void Drop(PropertyModel p);
        public abstract void Alter(PropertyModel p, PropertyModel fromSource, string propertyName);

        #endregion Properties

        public IScriptChangeEvaluator Evaluator { get; set; }

        public abstract void StartBlock(string message);

        public abstract void EndBlock(string message);

        public abstract void ByPassedBlock(string message);

        [DebuggerStepThrough]
        [DebuggerHidden]
        protected static void Pause()
        {
            if (System.Diagnostics.Debugger.IsAttached)
                System.Diagnostics.Debugger.Break();
        }

        public abstract void Write(System.IO.FileInfo file, policyEnum policy);

        public abstract void Clear();

    }

}
