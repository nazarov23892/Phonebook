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

        private const string insertString =
            @"insert 
                into 
            [Contacts] 
                ([Lastname], [Firstname], [Patronymic], [Phonenumber])
            values
                (@lastname, @firstname, @patronymic, @phonenumber);
            SELECT SCOPE_IDENTITY(); ";

        private const string updateString =
           @"UPDATE [c]
            SET 
                [c].[Lastname] = @lastname,
                [c].[Firstname] = @firstname,
                [c].[Patronymic] = @patronymic,
                [c].[Phonenumber] = @phonenumber
            FROM [Contacts] c
            WHERE [c].[ContactId] = @contactId ";

        private const string deleteString =
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
            using (SqlConnection sqlConnection = new SqlConnection(
                connectionString: connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(
                    cmdText: insertString,
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

                    sqlCommand.Parameters["@lastname"].Value = contact.Lastname;
                    sqlCommand.Parameters["@firstname"].Value = contact.Firstname;
                    sqlCommand.Parameters["@patronymic"].Value = contact.Patronymic;
                    sqlCommand.Parameters["@phonenumber"].Value = contact.Phonenumber;

                    var result = sqlCommand.ExecuteScalar() as decimal?;
                    sqlConnection.Close();
                }
            }
        }

        public void DeleteContact(int contactId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(
                   connectionString: connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(
                    cmdText: deleteString,
                    connection: sqlConnection))
                {
                    sqlConnection.Open();

                    sqlCommand.Parameters.Add(
                        new SqlParameter { ParameterName = "@contactId" });

                    sqlCommand.Parameters["@contactId"].Value = contactId;
                    var result = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
        }

        public void SaveContact(Contact contact)
        {
            using (SqlConnection sqlConnection = new SqlConnection(
                connectionString: connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(
                    cmdText: updateString,
                    connection: sqlConnection))
                {
                    sqlConnection.Open();

                    sqlCommand.Parameters.Add(
                        new SqlParameter { ParameterName = "@contactId" });
                    sqlCommand.Parameters.Add(
                        new SqlParameter { ParameterName = "@lastname" });
                    sqlCommand.Parameters.Add(
                        new SqlParameter { ParameterName = "@firstname" });
                    sqlCommand.Parameters.Add(
                        new SqlParameter { ParameterName = "@patronymic" });
                    sqlCommand.Parameters.Add(
                        new SqlParameter { ParameterName = "@phonenumber" });

                    sqlCommand.Parameters["@contactId"].Value = contact.ContactId;
                    sqlCommand.Parameters["@lastname"].Value = contact.Lastname;
                    sqlCommand.Parameters["@firstname"].Value = contact.Firstname;
                    sqlCommand.Parameters["@patronymic"].Value = contact.Patronymic;
                    sqlCommand.Parameters["@phonenumber"].Value = contact.Phonenumber;

                    var result = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
        }
    }
}
