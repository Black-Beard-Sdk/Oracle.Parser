namespace Bb.Oracle.Models.Codes
{




    public abstract class OCodeObject : OracleObject
    {

        public OCodeObject()
        {
            this.Kind = GetType().Name;
        }


        public string Kind { get; }

    }
    
}