using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Bb.Oracle.Reader.Dao
{


    public abstract class StructureDescriptorTable<T> : IReader<T>
    {

        private Func<T> creator;
        protected readonly string ConnectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="StructureDescriptorTable{T}"/> class.
        /// </summary>
        /// <param name="creator">The creator.</param>
        /// <param name="connectionString">The creator.</param>
        protected StructureDescriptorTable(Func<T> creator, string connectionString)
        {
            this.creator = creator;
            this.ConnectionString = connectionString;
        }

        #region Readers

        /// <summary>
        /// Read the next item.
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <returns></returns>
        public T Read(IDataReader reader)
        {
            var item = creator();
            Read(reader, item);
            return item;
        }

        /// <summary>
        /// Reads all items.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        public IEnumerable<T> ReadAll(IDataReader reader)
        {
            while (reader.Read())
            {
                var item = creator();
                Read(reader, item);
                yield return item;
            }
        }

        /// <summary>
        /// Reads all items.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        public IEnumerable<T> ReadAll(IDataReader reader, Action<T> act)
        {
            while (reader.Read())
            {
                var item = creator();
                Read(reader, item);
                act(item);
                yield return item;
            }
        }


        /// <summary>
        /// Readers the specified r.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <param name="item">The item.</param>
        public abstract void Read(IDataReader r, T item);

        /// <summary>
        /// Fieldses this instance.
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<Field> Fields();

        /// <summary>
        /// Gets the datatable.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        public abstract DataTable GetDataTable(string tableName, IEnumerable<T> entities);


        /// <summary>
        /// Loads the specified act.
        /// </summary>
        /// <param name="act">The act.</param>
        /// <param name="reader">The reader.</param>
        public void Load(System.Data.IDataReader reader, Action<T> act)
        {
            while (reader.Read())
            {
                T instance = creator();
                Read(reader, instance);
                act(instance);
            }
        }

        /// <summary>
        /// Loads the database.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="cnxString">The CNX string.</param>
        /// <param name="sql">The SQL.</param>
        /// <param name="act">The act.</param>
        public void LoadDb(DbProviderFactory factory, string cnxString, string sql, Action<T> act)
        {

            using (DbConnection cnx = factory.CreateConnection())
            {

                cnx.ConnectionString = cnxString;

                DbCommand cmd = factory.CreateCommand();
                cmd.CommandText = sql;
                cmd.Connection = cnx;
                cmd.CommandType = System.Data.CommandType.Text;

                cnx.Open();

                DbDataReader reader = cmd.ExecuteReader();
                Load(reader, act);

            }
        }


        #endregion Readers



        protected string GetTempName()
        {
            string name = "Temp" + Guid.NewGuid().ToString("N");
            name = name.Substring(0, 30);
            return name;
        }


        /// <summary>
        /// Creates the tempory table query and the datatable filled with the specified entities.
        /// </summary>
        /// <param name="fields">The fields.</param>
        /// <param name="entities">The entities.</param>
        /// <param name="dataTable">The data table.</param>
        /// <returns></returns>
        public string CreateTemporyTable(List<Field> fields, IEnumerable<T> entities, out DataTable dataTable)
        {

            #region Map table

            string tableName = GetTempName();
            dataTable = new DataTable(tableName);

            foreach (var item in fields)
                dataTable.Columns.Add(item.CreateColumn());

            foreach (var item in entities)
            {
                DataRow row = dataTable.NewRow();
                foreach (var field in fields)
                {
                    var value = field.ReadItem(item);
                    row[field.ColumnName] = value;
                }
                dataTable.Rows.Add(row);
            }

            #endregion Map table

            StringBuilder sb = new StringBuilder();

            bool t = false;
            foreach (var item in fields)
            {
                if (!t)
                    sb.Append(", ");
                sb.Append(item.ColumnName);
                t = true;
            }

            string query = sb.ToString();
            return query;

        }


        protected string InsertInto(List<Field> fields, DataTable dataTable, string tableName)
        {

            StringBuilder sb = new StringBuilder();

            bool t = false;
            foreach (var item in fields)
            {
                if (!t)
                    sb.Append(", ");
                sb.Append(item.ColumnName);
                t = true;
            }

            string query = sb.ToString();
            return query;

        }

    }

}
