using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Tools.Connection.Database
{
    public sealed class Connection
    {
        private string _connectionString;
        private DbProviderFactory _factory;

        public Connection(string connectionString, DbProviderFactory factory)
        {
            _connectionString = connectionString;
            _factory = factory;

            using (DbConnection dbConnection = CreateConnection())
            {
                dbConnection.Open();
            }
        }

        public object ExecuteScalar(Command command)
        {
            using (DbConnection dbConnection = CreateConnection())
            {
                using (DbCommand DbCommand = CreateCommand(command, dbConnection))
                {
                    dbConnection.Open();
                    object result = DbCommand.ExecuteScalar();
                    return (result is DBNull) ? null : result;
                }
            }
        }

        public IEnumerable<TResult> ExecuteReader<TResult>(Command command, Func<IDataReader, TResult> selector)
        {
            using (DbConnection dbConnection = CreateConnection())
            {
                using (DbCommand DbCommand = CreateCommand(command, dbConnection))
                {
                    dbConnection.Open();
                    using (DbDataReader dataReader = DbCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            yield return selector(dataReader);
                        }
                    }
                }
            }
        }

        public int ExecuteNonQuery(Command command)
        {
            using (DbConnection dbConnection = CreateConnection())
            {
                using (DbCommand DbCommand = CreateCommand(command, dbConnection))
                {
                    dbConnection.Open();
                    return DbCommand.ExecuteNonQuery();
                }
            }
        }

        private DbConnection CreateConnection()
        {
            DbConnection dbConnection = _factory.CreateConnection();
            dbConnection.ConnectionString = _connectionString;

            return dbConnection;
        }

        private DbCommand CreateCommand(Command command, DbConnection connection)
        {
            DbCommand dbCommand = connection.CreateCommand();
            dbCommand.CommandText = command.Query;

            if (command.IsStoredProcedure)
                dbCommand.CommandType = CommandType.StoredProcedure;

            foreach (KeyValuePair<string, object> kvp in command.Parameters)
            {
                DbParameter parameter = dbCommand.CreateParameter();
                parameter.ParameterName = kvp.Key;
                parameter.Value = kvp.Value;

                dbCommand.Parameters.Add(parameter);
            }

            return dbCommand;
        }
    }
}
