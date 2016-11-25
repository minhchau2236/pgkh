using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace PSCPortal.Framework
{
    [Serializable]
    public class Database
    {
        private string _connectionStringName = string.Empty;
        private string _providerInvariantName = string.Empty;
        private DbProviderFactory _factory = null;
        public DbProviderFactory Factory
        {
            get
            {
                return _factory;
            }
        }
        public Database(string connectionStringName)
        {
            _connectionStringName = connectionStringName;
            _providerInvariantName = System.Configuration.ConfigurationManager.ConnectionStrings[_connectionStringName].ProviderName;
            _factory = DbProviderFactories.GetFactory(_providerInvariantName);
        }
        public virtual DbConnection GetConnection()
        {
            DbConnection connection = Factory.CreateConnection();
            connection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[_connectionStringName].ConnectionString;
            return connection;
        }
        public virtual DbCommand GetCommand(DbConnection connection)
        {
            return GetCommand(connection, null);
        }
        public virtual DbCommand GetCommand()
        {
            return GetCommand(null, null);
        }
        public virtual DbCommand GetCommand(DbConnection connection, DbTransaction trans)
        {
            DbCommand command = Factory.CreateCommand();
            if (connection != null)
                command.Connection = connection;
            if (trans != null)
                command.Transaction = trans;
            command.CommandType = System.Data.CommandType.Text;
            return command;
        }
        public virtual DbParameter GetParameter()
        {
            DbParameter param = Factory.CreateParameter();
            return param;
        }
        public DbParameter GetParameter(System.Data.DbType type, string name, object value)
        {
            DbParameter param = Factory.CreateParameter();
            param.DbType = type;
            param.ParameterName = name;
            param.Value = value;
            return param;
        }
    }
}
