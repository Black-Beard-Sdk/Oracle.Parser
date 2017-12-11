
using Pssa.Sdk.DataAccess.Dao.Contracts.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pssa.Sdk.DataAccess.Dao;
using System.Data.Common;
using Oracle.ManagedDataAccess.Client;
using Pssa.Tools.Databases.Generators.Queries;

namespace Pssa.Sdk.DataAccess.Dao.Oracle
{

    /// <summary>
    /// Définit une couche d'accès aux données vers une source de données Oracle 10i.
    /// Offre des fonctions supplémentaires d'accès rapides à des procédures stockées, des tables ou des vues.
    /// </summary>
    [Serializable()]
    public class OracleManager : IDisposable
    {

        private OracleConnection _sqlConn;
        private string _connString;
        private DbTransaction _transaction;

        #region Constructeurs

        /// <summary>
        /// Constructeur : initialise une nouvelle connexion à une source de données Oracle
        /// </summary>
        /// <param name="connString">Chaine de connexion</param>
        public OracleManager(string connString)
        {
            FetchSize = 16 * 1024;
            _connString = connString;
            _transaction = null;
            Open();
        }

        /// <summary>
        /// Constructeur : initialise une nouvelle connexion à une source de données Oracle
        /// </summary>
        /// <param name="connString">Chaine de connexion</param>
        /// <param name="level">Comportement de verrouillage de la transaction</param>
        public OracleManager(string connString, IsolationLevel level)
        {

            _connString = connString;
            _transaction = null;
            Open(level);
        }


        #endregion

        public static string GetConnectionString(string connectionStringName)
        {

            var cnx = System.Configuration.ConfigurationManager.ConnectionStrings[connectionStringName];

            if (cnx == null)
                throw new Exception(string.Format("Invalid connection string name '{0}'", connectionStringName));
            
            return cnx.ConnectionString;

        }

        #region Propriétés

        /// <summary>
        /// Connexion ouverte vers la source de données
        /// </summary>
        public DbConnection Connection
        {
            get { return _sqlConn; }
        }

        /// <summary>
        /// Chaine de connexion
        /// </summary>
        public string ConnectionString
        {
            get { return _connString; }
        }

        public int FetchSize { get; set; }


        #endregion

        #region Core

        /// <summary>
        /// Ouvre la connexion vers la base de données
        /// </summary>
        private void Open()
        {
            try
            {
                if (_sqlConn == null)
                    _sqlConn = new OracleConnection(_connString);

                if (_sqlConn.State != ConnectionState.Open)
                    _sqlConn.Open();
                
            }
            catch (OracleException ex)
            {
                throw new DBConnectionException("Impossible de se connecter au serveur de données Oracle (connectionString : " + (_connString == null ? "null" : _connString) + " ).", ex);
            }
            catch (Exception ex)
            {
                throw new DBDataException(ex);
            }
        }

        /// <summary>
        /// CloseOpen
        /// </summary>
        public void CloseOpen()
        {
            Close();
            Open();
        }

        /// <summary>
        /// Ouvre la connexion vers la base de données
        /// </summary>
        /// <param name="level">Comportement de verrouillage de la transaction</param>
        private void Open(IsolationLevel level)
        {
            try
            {

                if (_sqlConn == null)
                    _sqlConn = new OracleConnection(_connString);

                if (_sqlConn.State != ConnectionState.Open)
                    _sqlConn.Open();

                if (_transaction == null)
                    _transaction = _sqlConn.BeginTransaction(level);

            }
            catch (OracleException ex)
            {
                throw new DBConnectionException("Impossible de se connecter au serveur de données Oracle (connectionString : " + (_connString == null ? "null" : _connString) + " ).", ex);
            }
            catch (Exception ex)
            {
                throw new DBDataException(ex);
            }
        }


        /// <summary>
        /// Met fin à la connexion vers la base de données
        /// </summary>
        public void Close()
        {
            if (this._transaction != null)
                _transaction.Dispose();
            if (this._sqlConn.State != ConnectionState.Closed)
                _sqlConn.Close();
            if (_sqlConn != null)
                _sqlConn.Dispose();
        }


        /// <summary>
        /// Retrouve une clé dans le app.config / web.config
        /// </summary>
        /// <param name="key">Nom de clé identifiant la chaine de connexion dans le fichier de configuration de l'application</param>
        /// <returns>ConnectionString</returns>
        public static string GetConfConnectionStrings(string key)
        {
            System.Configuration.ConnectionStringSettings chaine = System.Configuration.ConfigurationManager.ConnectionStrings[key];
            if (chaine != null)
                return chaine.ConnectionString;
            else
                return string.Empty;
        }

        /// <summary>
        /// Ouvre une transaction sur la source de données Oracle
        /// </summary>
        /// <remarks>IsolationLevel : ReadCommitted</remarks>
        public void BeginTransaction()
        {
            this.Open(IsolationLevel.ReadCommitted);
        }

        /// <summary>
        /// Impose l'utilisation d'une transaction existante
        /// </summary>
        /// <param name="transaction">Transaction ne pouvant être nulle</param>
        public void UseTransaction(DbTransaction transaction)
        {

            if (transaction != null)
            {
                this.CommitTransaction();

                _transaction = transaction;
                _sqlConn = transaction.Connection as OracleConnection;
                _connString = _sqlConn.ConnectionString;

                try
                {
                    if (_sqlConn.State != ConnectionState.Open)
                        _sqlConn.Open();
                }
                catch (OracleException ex)
                {
                    throw new DBConnectionException("Impossible de se connecter au serveur de données Oracle Server (connectionString : " + (_connString == null ? "null" : _connString) + " ).", ex);
                }
            }
        }


        /// <summary>
        /// Ouvre une transaction sur la source de données Oracle Server
        /// </summary>
        /// <param name="level">Comportement de verrouillage de la transaction</param>
        public void BeginTransaction(IsolationLevel level)
        {
            this.Open(level);
        }


        /// <summary>
        /// Valide les dernières modifications apportées lors des précédents traitements et met fin à la connexion ouverte vers la source de données
        /// </summary>
        public void CommitTransaction()
        {
            if (_transaction != null)
                _transaction.Commit();
            _transaction = null;

        }


        /// <summary>
        /// Annule les dernières modifications apportées lors des précédents traitements et met fin à la connexion ouverte vers la source de données
        /// </summary>
        public void RollbackTransaction()
        {
            if (_transaction != null)
                _transaction.Rollback();
            _transaction = null;

        }


        /// <summary>
        /// Construction de la commande d'appel d'une procédure stockée
        /// </summary>
        /// <param name="cmdType">Type de la commande</param>
        /// <param name="commandText">Texte de la commande</param>
        /// <param name="parameters">Paramètres</param>
        /// <returns>Commande Oracle</returns>
        private OracleCommand BuildCommand(CommandType cmdType, string commandText, params DbParameter[] parameters)
        {

            OracleCommand cmd = _sqlConn.CreateCommand() as OracleCommand;
            cmd.BindByName = true;
            cmd.CommandText = commandText;
            cmd.CommandType = cmdType;

            cmd.InitialLONGFetchSize = FetchSize;
            cmd.InitialLOBFetchSize = FetchSize;

            for (int i = 0; i < parameters.Length; i++)
                cmd.Parameters.Add(parameters[i]);

            return cmd;
        }

        #endregion

        #region ExecuteDataSet

        /// <summary>
        /// ExecuteDataSet
        /// </summary>
        /// <param name="cmdType">Type de la commande</param>
        /// <param name="requete">Requête</param>
        /// <param name="parameters">Paramètres</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteDataSet(CommandType cmdType, string requete, params DbParameter[] parameters)
        {
            DataSet ds = null;
            OracleTransaction trans = null;
            try
            {
                this.Open();
                using (OracleCommand cmd = BuildCommand(cmdType, requete, parameters))
                {
                    // Recupération de la transaction 
                    trans = cmd.Transaction;

                    using (OracleDataAdapter adapt = new OracleDataAdapter(cmd))
                    {
                        ds = new DataSet();
                        adapt.Fill(ds);
                    }

                }
            }
            catch (Exception ex)
            {
                throw new DBExecProcedureException("Erreur lors de l'exécution de la commande de type " + cmdType.ToString() + " sur la base Oracle : " + ex.Message, ex); ;
            }
            finally
            {
                if (_sqlConn != null)
                {
                    _sqlConn.Close();
                    _sqlConn.Dispose();
                }
            }


            return ds;
        }

        #endregion

        #region ExecuteDataTable

        /// <summary>
        /// ExecuteDataTable
        /// </summary>
        /// <param name="cmdType">Type de la commande</param>
        /// <param name="requete">Requête</param>
        /// <param name="parameters">Paramètres</param>
        /// <returns>DataTable</returns>
        public DataTable ExecuteDataTable(CommandType cmdType, string requete, params DbParameter[] parameters)
        {
            DataTable dt = null;
            OracleTransaction trans = null;
            try
            {
                this.Open();
                using (OracleCommand cmd = BuildCommand(cmdType, requete, parameters))
                {
                    trans = cmd.Transaction;

                    using (OracleDataAdapter adapt = new OracleDataAdapter(cmd))
                    {
                        dt = new DataTable();
                        adapt.Fill(dt);
                    }

                }
            }
            catch (Exception ex)
            {
                throw new DBExecProcedureException("Erreur lors de l'exécution de la commande de type " + cmdType.ToString() + " sur la base Oracle : " + ex.Message, ex); ;
            }
            finally
            {
                if (_sqlConn != null)
                {
                    _sqlConn.Close();
                    _sqlConn.Dispose();
                }
            }
            return dt;
        }

        #endregion

        #region ExecuteNonQuery

        /// <summary>
        /// ExecuteNonQuery
        /// </summary>
        /// <param name="cmdType">Type de la commande</param>
        /// <param name="requete">Requête</param>
        /// <param name="parameters">Paramètres</param>
        /// <returns>Entier</returns>
        public int ExecuteNonQuery(CommandType cmdType, string requete, params DbParameter[] parameters)
        {
            int val = -1;
            OracleTransaction trans = null;
            try
            {
                this.Open();
                using (OracleCommand cmd = BuildCommand(cmdType, requete, parameters))
                {

                    // Recupération de la transaction 
                    trans = cmd.Transaction;

                    // Execution de la requete / procédure stockée
                    val = cmd.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                throw new DBExecProcedureException("Erreur lors de l'exécution de la commande de type " + cmdType.ToString() + " sur la base Oracle : " + ex.Message, ex); ;
            }
            finally
            {
                if (_sqlConn != null)
                    _sqlConn.Close();
            }

            return val;

        }

        #endregion


        /// <summary>
        /// Creates the parameter.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="parameterDirection">The parameter direction.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public DbParameter CreateParameter(string parameterName, ParameterDirection parameterDirection, object value)
        {
            OracleParameter p = new OracleParameter();
            p.ParameterName = parameterName;
            p.Direction = parameterDirection;
            p.Value = value;
            return p;
        }

        /// <summary>
        /// Creates the parameter.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="parameterDirection">The parameter direction.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public DbParameter CreateParameter(string parameterName, ParameterDirection parameterDirection, DbType type)
        {
            OracleParameter p = new OracleParameter();
            p.ParameterName = parameterName;
            p.DbType = type;
            p.Direction = parameterDirection;
            return p;
        }

        #region ExecuteReader

        /// <summary>
        /// ExecuteReader
        /// </summary>
        /// <param name="cmdType">Type de la commande</param>
        /// <param name="requete">Requête</param>
        /// <param name="parameters">Paramètres</param>
        /// <returns>IDataReader</returns>
        public IDataReader ExecuteReader(CommandType cmdType, string requete, params DbParameter[] parameters)
        {

            IDataReader read = null;
            OracleTransaction trans = null;

            this.Open();

            try
            {

                using (OracleCommand cmd = BuildCommand(cmdType, requete, parameters))
                {

                    trans = cmd.Transaction;

                    read = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    //for (int i = 0; i < parameters.Count; i++)
                    //    for (int j = 0; j < cmd.Parameters.Count; j++)
                    //        if (cmd.Parameters[j].Direction != ParameterDirection.Input && cmd.Parameters[j].ParameterName == parameters[i].ParameterName)
                    //        {
                    //            parameters[i].Value = cmd.Parameters[j].Value;
                    //            break;
                    //        }

                }

            }
            catch (Exception ex)
            {
                throw new DBExecProcedureException("Erreur lors de l'exécution de la commande de type " + cmdType.ToString() + " sur la base Oracle : " + ex.Message, ex); ;
            }

            return read;

        }

        #endregion

        #region ExecuteScalar

        /// <summary>
        /// ExecuteScalar
        /// </summary>
        /// <param name="cmdType">Type de la commande</param>
        /// <param name="requete">Requête</param>
        /// <param name="parameters">Paramètres</param>
        /// <returns>Objet</returns>
        public object ExecuteScalar(CommandType cmdType, string requete, params DbParameter[] parameters)
        {
            object read = null;
            OracleTransaction trans = null;

            try
            {
                this.Open();
                using (OracleCommand cmd = BuildCommand(cmdType, requete, parameters))
                {
                    trans = cmd.Transaction;
                    read = cmd.ExecuteScalar();

                    //for (int i = 0; i < parameters.Length; i++)
                    //    for (int j = 0; j < cmd.Parameters.Count; j++)
                    //        if (cmd.Parameters[j].Direction != ParameterDirection.Input && cmd.Parameters[j].ParameterName == parameters[i].ParameterName)
                    //        {
                    //            parameters[i].Value = cmd.Parameters[j].Value;
                    //            break;
                    //        }
                }

            }
            catch (Exception ex)
            {
                throw new DBExecProcedureException("Erreur lors de l'exécution de la commande de type " + cmdType.ToString() + " sur la base Oracle : " + ex.Message, ex); ;
            }
            finally
            {
                if (_sqlConn != null)
                {
                    _sqlConn.Close();
                    _sqlConn.Dispose();
                }
            }

            return read;

        }

        #endregion

        /// <summary>
        /// Insert in bulking mode: here array binding. 
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="mappingTypes">The mapping types.</param>
        /// <param name="extraColumnsDefinition">The extra columns definition: Key used as column name, Value used as value to insert. Value is the same for all lines inserted.</param>
        /// <exception cref="System.ArgumentNullException">
        /// Tablename cannot be empty. It corresponds to sql table target.
        /// </exception>
        /// <exception cref="Pssa.Sdk.DataAccess.Dao.Contracts.Exceptions.DBExecProcedureException">Erreur lors de l'exécution de la commande de type  + CommandType.Text.ToString() +  sur la base Oracle :  + ex.Message</exception>
        public void InsertInBulk(DataTable table, Dictionary<string, OracleDbType> mappingTypes = null, Dictionary<string,string> extraColumnsDefinition = null)
        {
            if (table == null) throw new ArgumentNullException();
            if (String.IsNullOrWhiteSpace(table.TableName)) throw new ArgumentNullException("Tablename cannot be empty. It corresponds to sql table target.");

            try
            {
                var columnNames = new List<String>();
                for (var i = 0; i < table.Columns.Count; i++)
                {
                    columnNames.Add(table.Columns[i].ColumnName);
                }

                var query = "insert into " + table.TableName + " (" + String.Join(",", columnNames) + ") VALUES(:" + String.Join(",:", columnNames) + ")";

                this.Open();
                using (OracleCommand cmd = BuildCommand(CommandType.Text, query))
                {
                    cmd.BindByName = true;
                    cmd.ArrayBindCount = table.Rows.Count;
                    for (var i = 0; i < columnNames.Count; i++)
                    {
                        var l = new List<object>();
                        for (var j = 0; j < table.Rows.Count; j++)
                        {
                            l.Add(table.Rows[j][i]);
                        }
                        cmd.Parameters.Add(":" + columnNames[i], (mappingTypes == null ? TypeMatchExtension.ConvertToOracleDbType(table.Columns[i].DataType.ToString()) : mappingTypes[columnNames[i]]), l.ToArray(), ParameterDirection.Input);
                    }
                    int result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new DBExecProcedureException("Erreur lors de l'exécution de la commande de type " + CommandType.Text.ToString() + " sur la base Oracle : " + ex.Message, ex);
            }
            finally
            {
                if (_sqlConn != null)
                {
                    _sqlConn.Close();
                    _sqlConn.Dispose();
                }
            }
        }

        #region IDisposable Membres

        /// <summary>
        /// Dispose
        /// </summary>
        void IDisposable.Dispose()
        {
            _transaction = null;
            if (_sqlConn.State != ConnectionState.Closed)
                _sqlConn.Close();
            _sqlConn.Dispose();
        }

        #endregion


    }


}
