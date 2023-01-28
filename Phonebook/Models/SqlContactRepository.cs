using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Phonebook.Models
{
    public class SqlContactRepository : IContactRepository
    {
        private const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Phonebook;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private const string selectString =
            @"select 
                c.ContactId, 
                c.Lastname, 
                c.Firstname, 
                c.Patronymic, 
                c.Phonenumber
            from
                Contacts c";

        private const string insertCommandString =
            @"insert 
                into 
            [Contacts] 
                ([Lastname], [Firstname], [Patronymic], [Phonenumber])
            values
                (@lastname, @firstname, @patronymic, @phonenumber);
            SELECT SCOPE_IDENTITY(); ";

        private const string updateCommandString =
           @"UPDATE [c]
            SET 
                [c].[Lastname] = @lastname,
                [c].[Firstname] = @firstname,
                [c].[Patronymic] = @patronymic,
                [c].[Phonenumber] = @phonenumber
            FROM [Contacts] c
            WHERE [c].[ContactId] = @contactId ";

        private const string deleteCommandString =
            @"delete [c]
            from [Contacts] c
            where [c].[ContactId] = @contactId ";

        public IEnumerable<Contact> Contacts
        {
            get => GetContacts();
        }

        private IEnumerable<Contact> GetContacts()
        {
            List<Contact> contacts = new List<Contact>();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString: connectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(
                    cmdText: selectString, 
                    connection: sqlConnection);
               
                using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        Contact contact = new Contact
                        {
                            ContactId = sqlReader.GetFieldValue<int>(sqlReader.GetOrdinal("ContactId")),
                            Lastname = sqlReader["Lastname"] as string,
                            Firstname = sqlReader["Firstname"] as string,
                            Patronymic = sqlReader["Patronymic"] as string,
                            Phonenumber = sqlReader["Phonenumber"] as string
                        };
                        contacts.Add(contact);
                    }
                }
                sqlConnection.Close();
            }
            return contacts;
        }

        public void AddContact(Contact contact)
        {
            var parameters = new[] {
                new SqlParameter { ParameterName = "@lastname", Value = contact.Lastname },
                new SqlParameter { ParameterName = "@firstname", Value = contact.Firstname },
                new SqlParameter { ParameterName = "@patronymic", Value = contact.Patronymic },
                new SqlParameter { ParameterName = "@phonenumber", Value = contact.Phonenumber }
            };
            var result = ExecuteSqlScalar(
                sqlCommand: insertCommandString,
                sqlParameters: parameters) as decimal?;
            return;
        }

        public void DeleteContact(int contactId)
        {
            var parameters = new[] {
                new SqlParameter { ParameterName = "@contactId", Value = contactId }
            };
            var result = ExecuteSqlNonQuery(
                sqlCommand: deleteCommandString,
                sqlParameters: parameters);
            return;
        }

        public void SaveContact(Contact contact)
        {
            var parameters = new[] {
                new SqlParameter { ParameterName = "@contactId", Value = contact.ContactId },
                new SqlParameter { ParameterName = "@lastname", Value = contact.Lastname },
                new SqlParameter { ParameterName = "@firstname", Value = contact.Firstname },
                new SqlParameter { ParameterName = "@patronymic", Value = contact.Patronymic },
                new SqlParameter { ParameterName = "@phonenumber", Value = contact.Phonenumber }
            };
            var result = ExecuteSqlNonQuery(
                sqlCommand: updateCommandString,
                sqlParameters: parameters);
            return;
        }

        private int ExecuteSqlNonQuery(string sqlCommand, IEnumerable<SqlParameter> sqlParameters)
        {
            int scalarResult = 0;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString: connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(cmdText: sqlCommand, connection: sqlConnection);
                foreach (var param in sqlParameters ?? Enumerable.Empty<SqlParameter>())
                {
                    command.Parameters.Add(param);
                }
                scalarResult = command.ExecuteNonQuery();
                sqlConnection.Close();
            }
            return scalarResult;
        }

        private object ExecuteSqlScalar (string sqlCommand, IEnumerable<SqlParameter> sqlParameters)
        {
            object scalarResult;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString: connectionString))
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
                    scalarResult = command.ExecuteScalar();
                }
                sqlConnection.Close();
            }
            return scalarResult;
        }
    }
}
