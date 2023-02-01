using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Phonebook.Models
{
    public class DbTool
    {
        public string ConnectionString { get; set; }

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
