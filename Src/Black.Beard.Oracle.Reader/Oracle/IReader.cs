using System;
using System.Data.Common;
namespace Pssa.Sdk.DataAccess.Dao.Contracts
{
    
    public interface IReader<T>
    {
        System.Data.DataTable GetDataTable(string tableName, System.Collections.Generic.IEnumerable<T> entities);
        //void LoadTable(System.Configuration.ConnectionStringSettings cnx, string sql, Action<T> act);
        void Read(System.Data.IDataReader r, T item);
        T Read(System.Data.IDataReader reader);
        System.Collections.Generic.IEnumerable<T> ReadAll(System.Data.IDataReader reader);

        void Load(System.Data.IDataReader reader, Action<T> act);

        void LoadDb(DbProviderFactory factory, string cnxString, string sql, Action<T> act);

    }
}
