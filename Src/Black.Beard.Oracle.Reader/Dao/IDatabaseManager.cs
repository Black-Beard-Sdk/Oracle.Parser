using System;
namespace Pssa.Tools.Databases.Generators.Oracle
{
    public interface IDatabaseManager
    {
        void BeginTransaction();
        void BeginTransaction(global::System.Data.IsolationLevel level);
        void Close();
        void CloseOpen();
        void CommitTransaction();
        global::System.Data.Common.DbConnection Connection { get; }
        string ConnectionString { get; }
        global::System.Data.Common.DbParameter CreateParameter(string parameterName, global::System.Data.ParameterDirection parameterDirection, global::System.Data.DbType type);
        global::System.Data.Common.DbParameter CreateParameter(string parameterName, global::System.Data.ParameterDirection parameterDirection, object value);
        global::System.Data.DataSet ExecuteDataSet(global::System.Data.CommandType cmdType, string requete, params global::System.Data.Common.DbParameter[] parameters);
        global::System.Data.DataTable ExecuteDataTable(global::System.Data.CommandType cmdType, string requete, params global::System.Data.Common.DbParameter[] parameters);
        int ExecuteNonQuery(global::System.Data.CommandType cmdType, string requete, params global::System.Data.Common.DbParameter[] parameters);
        global::System.Data.IDataReader ExecuteReader(global::System.Data.CommandType cmdType, string requete, params global::System.Data.Common.DbParameter[] parameters);
        object ExecuteScalar(global::System.Data.CommandType cmdType, string requete, params global::System.Data.Common.DbParameter[] parameters);
        //void InsertInBulk(global::System.Data.DataTable table, global::System.Collections.Generic.Dictionary<string, global::Oracle.ManagedDataAccess.Client.OracleDbType> mappingTypes = null, global::System.Collections.Generic.Dictionary<string, string> extraColumnsDefinition = null);
        void RollbackTransaction();
        void UseTransaction(global::System.Data.Common.DbTransaction transaction);
    }
}
