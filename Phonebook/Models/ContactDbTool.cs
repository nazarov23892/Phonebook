﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Phonebook.Models
{
    public class ContactDbTool: DbTool
    {
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

        private const string selectByIdString =
            @"select 
                c.ContactId, 
                c.Lastname, 
                c.Firstname, 
                c.Patronymic, 
                c.Phonenumber
            from
                Contacts c
            where
                c.ContactId = @contactId ";

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

        public int Insert(string lastname, string firstname, string patronymic, string phonenumber)
        {
            var parameters = new[] {
                new SqlParameter { ParameterName = "@lastname", Value = (object)lastname ?? DBNull.Value},
                new SqlParameter { ParameterName = "@firstname", Value = (object)firstname ?? DBNull.Value},
                new SqlParameter { ParameterName = "@patronymic", Value = (object)patronymic ?? DBNull.Value },
                new SqlParameter { ParameterName = "@phonenumber", Value = (object)phonenumber ?? DBNull.Value }
            };
            var result = ExecuteSqlCommand(
                sqlCommand: insertCommandString,
                sqlParameters: parameters,
                sqlExecuteMode: CommandExecuteMode.Scalar) as decimal?;

            return result.HasValue
                ? Convert.ToInt32(result.Value)
                : -1;
        }

        public int Update(int contactId, string lastname, string firstname, string patronymic, string phonenumber)
        {
            var parameters = new[] {
                new SqlParameter { ParameterName = "@contactId", Value = contactId },
                new SqlParameter { ParameterName = "@lastname", Value = (object)lastname ?? DBNull.Value},
                new SqlParameter { ParameterName = "@firstname", Value = (object)firstname ?? DBNull.Value},
                new SqlParameter { ParameterName = "@patronymic", Value = (object)patronymic ?? DBNull.Value },
                new SqlParameter { ParameterName = "@phonenumber", Value = (object)phonenumber ?? DBNull.Value }
            };
            var result = ExecuteSqlCommand(
                sqlCommand: updateCommandString,
                sqlParameters: parameters,
                sqlExecuteMode: CommandExecuteMode.NonQuery) as int?;
            return result.HasValue
                ? result.Value
                : 0;
        }

        public int Delete(int contactId)
        {
            var parameters = new[] {
                new SqlParameter { ParameterName = "@contactId", Value = contactId }
            };
            var result = ExecuteSqlCommand(
                sqlCommand: deleteCommandString,
                sqlParameters: parameters,
                sqlExecuteMode: CommandExecuteMode.NonQuery) as int?;

            return result.HasValue
                ? result.Value
                : 0;
        }

        public void SelectById(int contactId, Action<IDataRecord> itemRowReadedFunc)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString: ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(
                    cmdText: selectByIdString,
                    connection: sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.Add(new SqlParameter { ParameterName = "@contactId", Value = contactId });

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

        public void Select(string filterName, string filterPhone, string sortColumn, OrderDirection orderDirection, Action<IDataRecord> itemRowReadedFunc)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString: ConnectionString))
            {
                string sqlText = String.IsNullOrEmpty(sortColumn)
                ? selectString
                : selectString + GetSqlOrderSection(sortColumn, orderDirection);

                using (SqlCommand sqlCommand = new SqlCommand(cmdText: sqlText, connection: sqlConnection))
                {
                    sqlConnection.Open();

                    sqlCommand.Parameters.Add(
                        new SqlParameter { ParameterName = "@phonenumber", Value = $"%{filterPhone}%" });
                    sqlCommand.Parameters.Add(
                        new SqlParameter { ParameterName = "@name", Value = $"%{filterName}%" });

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

        private string GetSqlOrderSection(string columnName, OrderDirection orderDirection)
        {
            string sort = orderDirection == OrderDirection.Descending ? "desc" : "asc";
            return $"order by c.{columnName} {sort}";
        }

    }

    public enum OrderDirection
    {
        Ascending,
        Descending
    }
}
