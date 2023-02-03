using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Phonebook.Models
{
    public class DbTool
    {
        public string ConnectionString { get; set; }

        protected void ExecuteSelect(string sqlSelectCommand, 
            Action<IDataRecord> itemRowReadedProc, 
            IEnumerable<SqlParameter> sqlParameters = null)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString: ConnectionString))
            {
               using (SqlCommand sqlCommand = new SqlCommand(cmdText: sqlSelectCommand, connection: sqlConnection))
                {
                    sqlConnection.Open();

                    foreach (var sqlParameter in sqlParameters ?? Enumerable.Empty<SqlParameter>())
                    {
                        sqlCommand.Parameters.Add(sqlParameter);
                    }
                    using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlReader != null && sqlReader.HasRows)
                        {
                            while (sqlReader.Read())
                            {
                                itemRowReadedProc(sqlReader);
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return;
        }

        protected object ExecuteSqlCommand(string sqlCommand, IEnumerable<SqlParameter> sqlParameters, CommandExecuteMode sqlExecuteMode)
        {
            object result;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString: ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand(
                    cmdText: sqlCommand,
                    connection: sqlConnection))
                {
                    foreach (var param in sqlParameters ?? Enumerable.Empty<SqlParameter>())
                    {
                        command.Parameters.Add(param);
                    }
                    result = sqlExecuteMode == CommandExecuteMode.Scalar
                        ? command.ExecuteScalar()
                        : command.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }
            return result;
        }

        protected enum CommandExecuteMode
        {
            Scalar, NonQuery
        }
    }


}
