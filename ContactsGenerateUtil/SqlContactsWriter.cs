using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Linq;
using Phonebook.Models;

namespace ContactsGenerateUtil
{
    class SqlContactsWriter
    {
        private const string connectionStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Phonebook;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        private const string insertStr =
            @"insert 
                into 
            Contacts 
                (Lastname, Firstname, Patronymic, Phonenumber)
            values
                (@lastname, @firstname, @patronymic, @phonenumber);
            SELECT SCOPE_IDENTITY(); ";

        public IEnumerable<Contact> Contacts { get; set; }

        public int Write()
        {
            int counter = 0;
            using (SqlConnection sqlConnection = new SqlConnection(
                connectionString: connectionStr))
            {
                using (SqlCommand sqlCommand = new SqlCommand(
                    cmdText: insertStr,
                    connection: sqlConnection))
                {
                    sqlConnection.Open();

                    sqlCommand.Parameters.Add(
                        new SqlParameter { ParameterName = "@lastname" });
                    sqlCommand.Parameters.Add(
                        new SqlParameter { ParameterName = "@firstname" });
                    sqlCommand.Parameters.Add(
                        new SqlParameter { ParameterName = "@patronymic" });
                    sqlCommand.Parameters.Add(
                        new SqlParameter { ParameterName = "@phonenumber" });

                    foreach (var contact in Contacts ?? Enumerable.Empty<Contact>())
                    {
                        sqlCommand.Parameters["@lastname"].Value = contact.Lastname;
                        sqlCommand.Parameters["@firstname"].Value = contact.Firstname;
                        sqlCommand.Parameters["@patronymic"].Value = contact.Patronymic;
                        sqlCommand.Parameters["@phonenumber"].Value = contact.Phonenumber;

                        int? id = sqlCommand.ExecuteScalar() as int?;
                        if (id.HasValue && id.Value > 0)
                        {
                            counter++;
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return counter;
        }
    }
}
