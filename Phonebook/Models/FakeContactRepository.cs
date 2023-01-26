using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace Phonebook.Models
{
    public class FakeContactRepository : IContactRepository
    {
        private List<Contact> contacts = new List<Contact>();
        private const string fileName = @"Data\contacts-items.json";

        public IEnumerable<Contact> Contacts 
        {
            get => contacts;
        }

        public FakeContactRepository()
        {
            contacts.AddRange(GetContacts());
        }

        public int AddContact(Contact contact)
        {
            throw new NotImplementedException();
        }

        public void DeleteContact(int contactId)
        {
            throw new NotImplementedException();
        }

        public void SaveContact(Contact contact)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<Contact> GetContacts()
        {
            string json = File.ReadAllText(path: fileName);
            var contacts = JsonSerializer.Deserialize(
                json: json,
                returnType: typeof(IEnumerable<Contact>)) as IEnumerable<Contact>;
            return contacts.Take(10);
        }
    }
}
