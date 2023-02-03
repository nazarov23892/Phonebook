using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Models
{
    public interface IContactRepository
    {
        public IEnumerable<Contact> Contacts { get; }
        public string FilterName { get; set; }
        public string FilterPhone { get; set; }
        public string FilterTag { get; set; }
        public string SortColumn { get; set; }
        public SortDirection SortDirection { get; set; }

        public Contact GetContact(int contactId);

        public void AddContact(Contact contact);

        public void SaveContact(Contact contact);

        public void DeleteContact(int contactId);
    }

    public enum SortDirection
    {
        Ascending,
        Descending
    }
}
