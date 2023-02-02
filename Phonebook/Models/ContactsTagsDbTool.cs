using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Phonebook.Models
{
    public class ContactsTagsDbTool : DbTool
    {
        private const string insertString =
                @"insert into ContactsTags(ContactId, TagId)
                select @contactId, t.TagId from Tags t 
                where t.Tag = @tag and not exists(
	                select ct.TagId 
	                from ContactsTags ct inner join Tags t on ct.TagId = t.TagId
	                where ct.ContactId = @contactId and t.Tag = @tag)";

        private const string deleteString =
                @"delete ct
                from ContactsTags ct
                where ct.ContactId = @contactId";

        public int Insert(int contactId, string tag)
        {
            var parameters = new[]
            {
                new SqlParameter{ ParameterName = "@contactId", Value = contactId },
                new SqlParameter{ ParameterName = "@tag", Value = tag }
            };
            int? result = ExecuteSqlCommand(
                sqlCommand: insertString,
                sqlParameters: parameters,
                sqlExecuteMode: CommandExecuteMode.NonQuery) as int?;

            return result.HasValue
                ? result.Value
                : 0;
        }

        public int DeleteByContact(int contactId)
        {
            var parameters = new[]
            {
                new SqlParameter { ParameterName = "@contactId", Value = contactId }
            };
            int? result = ExecuteSqlCommand(
                sqlCommand: deleteString,
                sqlParameters: parameters,
                sqlExecuteMode: CommandExecuteMode.NonQuery) as int?;

            return result.HasValue
                ? result.Value
                : 0;
        }
    }
}
