using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Models
{
    public interface ITagRepository
    {
        public IEnumerable<string> Tags { get; }
        
        public void AddTag(string tag);

        public void DeleteTag(string tag);
    }
}
