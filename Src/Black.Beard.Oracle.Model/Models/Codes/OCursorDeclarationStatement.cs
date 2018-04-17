using Bb.Oracle.Structures.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bb.Oracle.Models.Codes
{

    public class OCursorDeclarationStatement : OCodeStatement
    {

        public OCursorDeclarationStatement()
        {

            this.Arguments = new ArgumentCollection() { Parent = this };

            this.ResultType = new ProcedureResult() { Parent = this };

        }

        public override void Accept(Contracts.IOracleModelVisitor visitor)
        {
            visitor.VisitCursorDeclarationStatement(this);
        }

        public override KindModelEnum KindModel =>  KindModelEnum.CursorDeclaration;

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Arguments
        /// </summary>
        /// <returns>		
        /// Objet <see cref="ArgumentCollection" />.");
        /// </returns>
        public ArgumentCollection Arguments { get; set; }

        /// <summary>
        /// ResultType
        /// </summary>
        /// <returns>		
        /// Objet <see cref="ProcedureResult" />.");
        /// </returns>
        public ProcedureResult ResultType { get; set; }



    }
}
