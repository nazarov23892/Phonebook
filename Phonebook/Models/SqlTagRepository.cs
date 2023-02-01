using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Models
{
    public class SqlTagRepository : ITagRepository
    {
        private List<string> tags = new List<string>
        {
            "tag#1", "tag#2", "tag#3"
        };

        public IEnumerable<string> Tags 
        { 
            get => tags; 
        }

        public void AddTag(string tag)
        {
            throw new NotImplementedException();
        }

        public void DeleteTag(string tag)
        {
            throw new NotImplementedException();
        }

    }
}
