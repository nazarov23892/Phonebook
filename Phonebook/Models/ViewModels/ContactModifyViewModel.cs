using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Models.ViewModels
{
    public class ContactModifyViewModel
    {
        public Contact Contact { get; set; }
        public string[] TagsToAssign { get; set; }
    }
}
