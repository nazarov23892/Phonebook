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
                Contacts c
            where
                c.Phonenumber like(@phonenumber)
                and 
                (
                c.Firstname like(@name) 
                or c.Lastname like(@name) 
                or c.Patronymic like(@name)
                ) ";

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
        public string FilterName { get; set; }
        public string FilterPhone { get; set; }
        public string SortColumn { get; set ; }
        public SortDirection SortDirection { get; set; }

        private IEnumerable<Contact> GetContacts()
        {
            List<Contact> contacts = new List<Contact>();

            string sqlSelect = String.IsNullOrEmpty(SortColumn)
                    ? selectString
                    : selectString + GetSqlOrderSection(SortColumn, SortDirection);

            using (SqlConnection sqlConnection = new SqlConnection(connectionString: connectionString))
            {
                sqlConnection.Open();;
                using (SqlCommand sqlCommand = new SqlCommand(
                    cmdText: sqlSelect,
                    connection: sqlConnection))
                {
                    sqlCommand.Parameters.Add(
                        new SqlParameter {ParameterName = "@name", Value = $"%{FilterName}%" });
                    sqlCommand.Parameters.Add(
                        new SqlParameter { ParameterName = "@phonenumber", Value = $"%{FilterPhone}%" });

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
                }
                sqlConnection.Close();
            }
            return contacts;
        }

        public void AddContact(Contact contact)
        {
            var parameters = new[] {
                new SqlParameter { ParameterName = "@lastname", Value = (object)contact.Lastname ?? DBNull.Value},
                new SqlParameter { ParameterName = "@firstname", Value = (object)contact.Firstname ?? DBNull.Value},
                new SqlParameter { ParameterName = "@patronymic", Value = (object)contact.Patronymic ?? DBNull.Value },
                new SqlParameter { ParameterName = "@phonenumber", Value = (object)contact.Phonenumber ?? DBNull.Value }
            };
            var result = ExecuteSqlCommand(
                sqlCommand: insertCommandString,
                sqlParameters: parameters,
                sqlExecuteMode: CommandExecuteMode.Scalar) as decimal?;
            return;
        }

        public void DeleteContact(int contactId)
        {
            var parameters = new[] {
                new SqlParameter { ParameterName = "@contactId", Value = contactId }
            };
            var result = ExecuteSqlCommand(
                sqlCommand: deleteCommandString,
                sqlParameters: parameters,
                sqlExecuteMode: CommandExecuteMode.NonQuery) as int?;
            return;
        }

        public void SaveContact(Contact contact)
        {
            var parameters = new[] {
                new SqlParameter { ParameterName = "@contactId", Value = contact.ContactId },
                new SqlParameter { ParameterName = "@lastname", Value = (object)contact.Lastname ?? DBNull.Value},
                new SqlParameter { ParameterName = "@firstname", Value = (object)contact.Firstname ?? DBNull.Value},
                new SqlParameter { ParameterName = "@patronymic", Value = (object)contact.Patronymic ?? DBNull.Value },
                new SqlParameter { ParameterName = "@phonenumber", Value = (object)contact.Phonenumber ?? DBNull.Value }
            };
            var result = ExecuteSqlCommand(
                sqlCommand: updateCommandString,
                sqlParameters: parameters,
                sqlExecuteMode: CommandExecuteMode.NonQuery) as int?;
            return;
        }

        private object ExecuteSqlCommand(string sqlCommand, IEnumerable<SqlParameter> sqlParameters, CommandExecuteMode sqlExecuteMode)
        {
            object result;
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
                    result = sqlExecuteMode == CommandExecuteMode.Scalar
                        ? command.ExecuteScalar()
                        : command.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }
            return result;
        }

        private string GetSqlOrderSection(string columnName, SortDirection sortDir)
        {
            string sort = sortDir == SortDirection.Descending ? "desc" : "asc";
            return $"order by c.{columnName} {sort}";
        }

        private enum CommandExecuteMode
        {
            Scalar, NonQuery
        }
    }
}
