using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Bb.Oracle.Models.Names
{

    public class MethodName : ObjectName
    {

        public MethodName(params string[] items) 
            : this(items.ToList())
        {

        }

        public MethodName(List<string> items) 
            : base()
        {

            if (items == null)
                throw new System.ArgumentNullException(nameof(items));

            switch (items.Count)
            {

                case 1:
                    base.Schema = string.Empty;
                    base.Package = string.Empty;
                    base.Name = items[0];
                    break;

                case 2:
                    base.Schema = items[0];
                    base.Package = string.Empty;
                    base.Name = items[1];
                    break;

                case 3:
                    base.Schema = items[0];
                    base.Package = items[1];
                    base.Name = items[2];
                    break;

                default:
                    if (System.Diagnostics.Debugger.IsAttached)
                        System.Diagnostics.Debugger.Break();
                    break;
            }

        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Schema))
            {
                if (string.IsNullOrEmpty(Package))
                    return Name;
                else
                    return $"{Package}.{Name}";
            }
            else if (string.IsNullOrEmpty(Package))
                return $"{Schema}.{Name}";
            else
                return $"{Schema}.{Package}.{Name}";
        }

    }
}