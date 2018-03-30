namespace Bb.Oracle.Visitors
{
    public class PolicyBehavior
    {


        public static readonly PolicyBehavior Default = new PolicyBehavior()
        {
            ParseCode = false,
        };

        public PolicyBehavior()
        {
            if (PolicyBehavior.Default != null)
            {
                ParseCode = PolicyBehavior.Default.ParseCode;
            }
        }

        public bool ParseCode { get; set; }

    }

}