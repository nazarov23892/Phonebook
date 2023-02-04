using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Models.ViewModels
{
    public class ContactListViewModel
    {
        private IEnumerable<Contact> contacts;

        public int PageNo { get; set; } = 1;

        public int PageSize { get; set; } = 12;

        public string FilterName { get; set; }

        public string FilterPhonenumber { get; set; }

        public string FilterTag { get; set; }

        public string SortOption { get; set; }

        public int PageCount { get; set; }

        public IEnumerable<Contact> Contacts { get; set; }

        public int TotalCount { get; set; }
    }
}
