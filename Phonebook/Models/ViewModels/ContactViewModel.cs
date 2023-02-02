using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Models.ViewModels
{
    public class ContactViewModel
    {
        public Contact Contact { get; set; }
        public IEnumerable<string> TagList { get; set; }
    }
}
