using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Phonebook.Models
{
    public class TagDbTool
    {
        private const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Phonebook;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private const string selectString =
            @"select
            t.Tag
            from
            Tags t";

        public void Select(Action<IDataRecord> itemRowReadedFunc)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString: connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(cmdText: selectString, connection: sqlConnection))
                {
                    sqlConnection.Open();
                    using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlReader != null && sqlReader.HasRows)
                        {
                            while (sqlReader.Read())
                            {
                                itemRowReadedFunc(sqlReader);
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return;
        }

    }
}
