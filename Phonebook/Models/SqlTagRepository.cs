using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Models
{
    public class SqlTagRepository : ITagRepository
    {
        private TagDbTool tagsDbTool = new TagDbTool
        {
            ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Phonebook;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
        };

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
