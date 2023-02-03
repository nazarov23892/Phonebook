using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Phonebook.Models
{
    public class TagDbTool: DbTool
    {
        private const string selectString =
            @"select
            t.Tag
            from
            Tags t";

        private const string selectByContactString =
            @"select
            t.Tag
            from
            ContactsTags ct
            inner join Tags t on t.TagId = ct.TagId
            where
            ct.ContactId = @id";

        private const string insertCommandString =
            @"insert 
            into Tags (Tag)
            select @tag 
            where not exists (select t.Tag from Tags t where t.Tag = @tag)";

        private const string deleteCommandString =
            @"delete t from Tags t where t.Tag = @tag";

        public void Select(Action<IDataRecord> itemRowReadedFunc)
        {
            ExecuteSelect(sqlSelectCommand: selectString,
                itemRowReadedProc: itemRowReadedFunc,
                sqlParameters: null);

            return;
        }

        public void SelectByContact(int contactId, Action<IDataRecord> itemRowReadedFunc)
        {
            SqlParameter[] parameters = new[]
            {
                new SqlParameter { ParameterName = "@id", Value = contactId }
            };
            ExecuteSelect(sqlSelectCommand: selectByContactString,
                itemRowReadedProc: itemRowReadedFunc,
                sqlParameters: parameters);

            return;
        }

        public int Insert(string tag)
        {
            var parameters = new[] {
                new SqlParameter { ParameterName = "@tag", Value = (object)tag ?? DBNull.Value},
            };
            var result = ExecuteSqlCommand(
                sqlCommand: insertCommandString,
                sqlParameters: parameters,
                sqlExecuteMode: CommandExecuteMode.Scalar) as decimal?;

            return result.HasValue
                ? Convert.ToInt32(result.Value)
                : -1;
        }

        public int Delete(string tag)
        {
            var parameters = new[] {
                new SqlParameter { ParameterName = "@tag", Value = tag }
            };
            var result = ExecuteSqlCommand(
                sqlCommand: deleteCommandString,
                sqlParameters: parameters,
                sqlExecuteMode: CommandExecuteMode.NonQuery) as int?;

            return result.HasValue
                ? result.Value
                : 0;
        }

    }
}
