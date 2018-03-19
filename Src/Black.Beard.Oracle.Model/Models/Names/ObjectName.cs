using Bb.Oracle.Structures.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bb.Oracle.Models.Names
{

    public abstract class ObjectName
    {

        //public ObjectName(string schema, string objectName)
        //{
        //    this.Schema = schema;
        //    this.Name = objectName;
        //}

        public virtual KindModelEnum KindModel => throw new NotImplementedException();

        public string Schema { get; set; }

        public string Name { get; set; }
        public string Package { get; internal set; }

        public static MethodName ProcedureName(List<string> items)
        {

            if (items == null)
                throw new ArgumentNullException(nameof(items));

            return new MethodName(items);

        }

    }



}
