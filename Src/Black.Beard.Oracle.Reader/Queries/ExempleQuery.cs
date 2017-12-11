//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Data.Common;
//using System.Data;
//using Pssa.Tools.Databases;
//using Pssa.Tools.Databases.Generators;
//using Pssa.Sdk.DataAccess.Dao.Contracts;
//using System.Diagnostics;
//using System.Text.RegularExpressions;
//using Pssa.Sdk.DataAccess.Dao;
//using Pssa.Tools.Databases.Models;

//namespace Pssa.Tools.Databases.Generators.Queries.Oracle
//{

//    public class OwnerNameQuery : DbQueryBase
//    {

//        string sql =
//@"
//";

//        public override void Resolve(DbContextOracle context)
//        {

//            var tables = context.database;

//            Action<OwnerNameQueryTable> act =
//                t =>
//                {

//                    string key = t.Table_owner + "." + t.Table_name;

//                };

//            OwnerNameQueryDescriptor OwnerName = new OwnerNameQueryDescriptor(context.Manager.ConnectionString);
//            sql = string.Format(sql, DbQueryBase.TableQueryWhereCondition("l"));

//            using (var reader = context.Manager.ExecuteReader(CommandType.Text, sql, DbQueryBase.DbParams.ToArray()))
//            {
//                var List = OwnerName.ReadAll(reader, act).ToList();
//            }
//        }

//    }

//    public class OwnerNameQueryDescriptor : StructureDescriptorTable<OwnerNameQueryTable>
//    {

//        public OwnerNameQueryDescriptor(string connectionString)
//            : base(() => new OwnerNameQueryTable(), connectionString)
//        {

//        }

//        public static class Columns
//        {

//            public static Field<string> OwnerName_OWNER = new Field<string>() { ColumnName = "OwnerName_OWNER", Read = reader => reader.Field<string>((int)OwnerNameQueryColumns.OwnerName_OWNER) };

//        }

//        #region Readers

//        public override void Read(IDataReader r, OwnerNameQueryTable item)
//        {
//            item.Owner = OwnerNameQueryDescriptor.Columns.OwnerName_OWNER.Read(r);
//        }

//        #endregion Readers

//        public override IEnumerable<Field> Fields()
//        {
//            yield return Columns.OwnerName_OWNER;
//        }

//        public override DataTable GetDataTable(string tableName, IEnumerable<OwnerNameQueryTable> entities)
//        {
//            throw new NotImplementedException();
//        }


//    }

//    public enum OwnerNameQueryColumns
//    {
//        OwnerName_OWNER,
//    }

//    public class OwnerNameQueryTable
//    {

//        public string OwnerName_OWNER { get; set; }

//    }

//}
