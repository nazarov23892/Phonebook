using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Models
{
    public interface IContactRepository
    {
        public IEnumerable<Contact> Contacts { get; }

        public void AddContact(Contact contact);

        public void SaveContact(Contact contact);

        public void DeleteContact(int contactId);
    }
}
