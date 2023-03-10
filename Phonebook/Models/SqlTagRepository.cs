using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Models
{
    public class SqlTagRepository : ITagRepository
    {
        private TagDbTool tagsDbTool;
        private ContactsTagsDbTool contactsTagsDbTool;

        public SqlTagRepository(string connectionString)
        {
            contactsTagsDbTool = new ContactsTagsDbTool { ConnectionString = connectionString };
            tagsDbTool = new TagDbTool { ConnectionString = connectionString };
        }

        public IEnumerable<string> Tags
        {
            get => GetTags();
        }

        public void AddTag(string tag)
        {
            tagsDbTool.Insert(tag);
        }

        public void DeleteTag(string tag)
        {
            contactsTagsDbTool.DeleteByTag(tag);
            tagsDbTool.Delete(tag);
        }

        private IEnumerable<string> GetTags()
        {
            List<string> tags = new List<string>();
            tagsDbTool.Select(itemRow =>
            {
                string tag = itemRow["Tag"] as string;
                if (!String.IsNullOrEmpty(tag))
                {
                    tags.Add(tag);
                }
            });
            return tags;
        }
    }
}
