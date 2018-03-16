using System.Collections.Generic;
using System.Text;

namespace Bb.Oracle.Models
{

    public class EventParser
    {


        public EventParser()
        {

        }

        public EventParser(ItemBase item)
        {
            this.OjectName = $"{item.GetOwner()}.{item.GetName()}";
            this.Kind = item.KindModel;
        }

        public List<FileElement> Files { get; set; } = new List<FileElement>();

        public string Message { get; set; }

        public string OjectName { get; set; }
        public KindModelEnum Kind { get; private set; }
        public string Type { get; set; }

        public string ValidatorName { get; set; }

    }

}
