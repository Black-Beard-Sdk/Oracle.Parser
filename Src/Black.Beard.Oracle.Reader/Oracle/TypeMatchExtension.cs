
using Oracle.ManagedDataAccess.Client;
using System;
using System.Linq;
using System.Text;

namespace Pssa.Tools.Databases.Generators.Queries
{

    public static class TypeMatchExtension
    {

        public static string Match(this string type, int lenght, int precision, int level)
        {

            if (string.IsNullOrEmpty(type))
                return string.Empty;

            char[] type2 = type.Where(c => char.IsLetter(c) || c == ' ').Select(c => char.ToUpper(c)).ToArray();

            StringBuilder sb = new StringBuilder(type2.Length);
            sb.Append(type2);
            string t = sb.ToString();

            if (t.StartsWith("PLSQL " ))
                t = t.Substring(6);

            //TIMESTAMP(6) WITH TIME ZONE
            //TIMESTAMP(6)

            Type result = typeof(object);

            switch (t)
            {

                case "INTERVAL YEAR TO MONTH":
                    result = typeof(Int32);
                    break;


                case "NUMBER":
                    //if (precision > 0)
                    //    result = typeof(Decimal);

                    //else
                    //{
                    //    switch (lenght)
                    //    {
                    //        case 1:
                    //            result = typeof(bool);
                    //            break;
                    //        case 2:
                    //        case 3:
                    //        case 4:
                    //        case 5:
                    //            result = typeof(Int16);
                    //            break;
                    //        case 6:
                    //        case 7:
                    //        case 8:
                    //            result = typeof(Int32);
                    //            break;
                    //        case 9:
                    //        default:
                    //            result = typeof(long);
                    //            break;
                    //    }
                    //}

                    // 1 000 000 000 000 000 000 000
                    // 9223372036854775807
                     // 2147483647

                case "BINARY FLOAT":
                case "UNSIGNED INTEGER":
                case "FLOAT":
                case "INTEGER":
                    // 19 decimal
                    // 10 int32
                    // 19, 0 Int 64
                    // 4, 0    int 16                    
                    result = typeof(Decimal);
                    
                    break;

                case "ROWID":
                case "NCLOB":
                case "LONG":
                case "CHAR":
                case "NVARCHAR":
                case "NVARCHAR2":
                case "NCHAR":
                case "NCHAR2":
                case "VARCHAR":
                case "VARCHAR2":
                    result = typeof(string);
                    break;


                case "TIMESTAMP WITH TZ":
                case "INTERVAL DAY TO SECOND":
                case "TIMESTAMP WITH LOCAL TIME ZONE":
                case "TIMESTAMP WITH TIME ZONE":
                case "TIMESTAMP":
                case "DATE":
                    result = typeof(DateTime);
                    break;

                case "BYTE":
                    result = typeof(byte);
                    break;


                //  Double	            8-byte FLOAT
                //  Single	            4-byte FLOAT                   
                //  Int16	            2-byte INTEGER
                //  Int32	            4-byte INTEGER
                //  Int64	            8-byte INTEGER

                //  BFile	            BFILE
                //  Blob	            BLOB
                //  Clob	            CLOB
                //  IntervalDS	        INTERVAL DAY TO SECOND
                //  IntervalYM	        INTERVAL YEAR TO MONTH
                //  LongRaw	            LONG RAW
                //  NClob	            NCLOB
                //  Raw	                RAW
                //  RefCursor	        REF CURSOR                     

                //  XmlType	            XMLType                        

                case "REF CURSOR":
                case "PLSQL RECORD":
                case "":

                case "RAW":
                case "LONG RAW":
                case "OBJECT":
                case "CLOB":
                case "BFILE":
                case "BLOB":
                    result = typeof(byte[]);
                    break;

                case "BOOLEAN":
                    result = typeof(bool);
                    break;

                case "TABLE":
                    result = typeof(object);
                    break;

                default:
                    result = typeof(object);
                    break;

            }

            return result.FullName + ", " + result.Assembly.GetName().Name;

        }



        public static System.Data.DbType ConvertToDbType(string type)
        {

            if (string.IsNullOrEmpty(type))
                return System.Data.DbType.Object;

            char[] type2 = type.Where(c => char.IsLetter(c) || c == ' ').Select(c => char.ToUpper(c)).ToArray();

            StringBuilder sb = new StringBuilder(type2.Length);
            sb.Append(type2);
            string t = sb.ToString();

            if (t.StartsWith("PLSQL "))
                t = t.Substring(6);

            switch (t)
            {

                case "INTERVAL YEAR TO MONTH":
                    return System.Data.DbType.Int32;

                case "BOOLEAN":
                    return System.Data.DbType.Boolean;

                case "FLOAT":
                    return System.Data.DbType.Double;

                case "INTEGER":
                    return System.Data.DbType.Int32;

                case "UNSIGNED INTEGER":
                    return System.Data.DbType.UInt32;

                case "NUMBER":
                    return System.Data.DbType.VarNumeric;

                case "LONG":
                    return System.Data.DbType.AnsiString;

                case "VARCHAR2":
                case "NVARCHAR2":
                    return System.Data.DbType.String;

                case "ROWID":
                    return System.Data.DbType.AnsiString;

                case "NCLOB":
                case "CHAR":
                case "NCHAR":
                case "NCHAR2":
                case "VARCHAR":
                    return System.Data.DbType.StringFixedLength;

                case "INTERVAL DAY TO SECOND":
                    return System.Data.DbType.Object;

                case "TIMESTAMP WITH LOCAL TIME ZONE":
                case "TIMESTAMP WITH TIME ZONE":
                case "TIMESTAMP":
                case "DATE":
                    return System.Data.DbType.DateTime;

                case "BYTE":
                    return System.Data.DbType.Byte;

                case "RAW":
                case "LONG RAW":
                    return System.Data.DbType.Binary;

                case "REF CURSOR":
                case "PLSQL RECORD":
                case "":

                case "OBJECT":
                case "CLOB":
                case "BFILE":
                case "BLOB":
                    return System.Data.DbType.Object; 

                default:
                    return System.Data.DbType.Object; 

            }

        }


        public static OracleDbType ConvertToOracleDbType(string type)
        {

            if (string.IsNullOrEmpty(type))
                return OracleDbType.NVarchar2;

            type = type.ToUpperInvariant().Replace("SYSTEM.", "");

            switch (type)
            {

                case "INT16":
                    return OracleDbType.Int16;
                case "INT32":
                    return OracleDbType.Int16;
                case "INT64":
                    return OracleDbType.Int64;
                case "STRING":
                    return OracleDbType.NVarchar2;
                case "BYTE":
                    return OracleDbType.Byte;
                case "CHAR":
                    return OracleDbType.Char;
                case "DECIMAL":
                    return OracleDbType.Decimal;
                case "DOUBLE":
                    return OracleDbType.Double;
                case "SINGLE":
                    return OracleDbType.Single;
                case "DATETIME":
                    return OracleDbType.Date;
                default:
                    return OracleDbType.NVarchar2;
            }

        }

    }
   
}
