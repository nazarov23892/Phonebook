using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Models
{
    public class SqlTagRepository : ITagRepository
    {
        private TagDbTool tagsDbTool = new TagDbTool();

        public IEnumerable<string> Tags
        {
            get => GetTags();
        }

        public void AddTag(string tag)
        {
            throw new NotImplementedException();
        }

        public void DeleteTag(string tag)
        {
            throw new NotImplementedException();
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
