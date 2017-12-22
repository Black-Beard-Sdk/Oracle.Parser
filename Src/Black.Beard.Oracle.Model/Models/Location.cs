using System.Text;

namespace Bb.Oracle.Models
{

    public class Location
    {

        public int Line { get; set; }

        public int Column { get; set; }

        public int Offset { get; set; }

        public int Length { get; set; }

        public override string ToString()
        {

            StringBuilder sb = new StringBuilder(100);

            sb.Append("Line : ");
            sb.Append(Line);

            sb.Append(", Column : ");
            sb.Append(Column);

            sb.Append(", Offset : ");
            sb.Append(Offset);

            return sb.ToString();

        }

    }

}