using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Bb.Oracle.Reader.Dao
{

        [DebuggerDisplay("{ColumnName} : {Type.FullName}")]
        public class Field
        {

            public Field()
            {
                ForeignKey = new ForeignKey();
            }

            public string ColumnName { get; set; }
            public int ColumnId { get; set; }
            public string DataType { get; set; }
            public int DataLength { get; set; }
            public int DataPrecision { get; set; }
            public bool Nullable { get; set; }
            public int DefaultLength { get; set; }
            public string DataDefault { get; set; }
            public string CharactereSetName { get; set; }
            public bool DataUpgraded { get; set; }
            public Type Type { get; set; }
            public bool IsKey { get; set; }
            public bool IsSequence { get; set; }
            public bool IsComputed { get; set; }

            public ForeignKey ForeignKey { get; private set; }

            public Func<object, object> ReadItem { get; set; }

            public virtual DataColumn CreateColumn() { return new DataColumn(ColumnName, Type); }

        }

        /// <summary>
        /// Field descriptor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class Field<T> : Field
        {

            /// <summary>
            /// Gets or sets the read.
            /// </summary>
            /// <value>
            /// The read.
            /// </value>
            public Func<IDataReader, T> Read { get; set; }

            /// <summary>
            /// Creates the column.
            /// </summary>
            /// <returns></returns>
            public override DataColumn CreateColumn()
            {
                return new DataColumn(ColumnName, typeof(T));
            }

        }

        public class ForeignKey
        {

            public bool IsForeignKey { get; set; }

            public string ConstraintName { get; set; }

            public string Table { get; set; }

            public string Field { get; set; }

        }

}
