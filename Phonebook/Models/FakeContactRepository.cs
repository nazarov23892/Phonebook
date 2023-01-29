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
        public string FilterName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string FilterPhone { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string SortColumn { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public SortDirection SortDirection { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public FakeContactRepository()
        {
            contacts.AddRange(GetContacts());
        }

        public void AddContact(Contact contact)
        {
            int id = 1 + contacts.Max(i => i.ContactId);
            contact.ContactId = id;
            contacts.Add(contact);
        }

        public void DeleteContact(int contactId)
        {
            contacts.RemoveAll(i => i.ContactId == contactId);
        }

        public void SaveContact(Contact contact)
        {
            var item = contacts
                .FirstOrDefault(i => i.ContactId == contact.ContactId);
            if (item == null)
            {
                return;
            }
            item.Lastname = contact.Lastname;
            item.Firstname = contact.Firstname;
            item.Patronymic = contact.Patronymic;
            item.Phonenumber = contact.Phonenumber;
        }

        private IEnumerable<Contact> GetContacts()
        {
            string json = File.ReadAllText(path: fileName);
            var contacts = JsonSerializer.Deserialize(
                json: json,
                returnType: typeof(IEnumerable<Contact>)) as IEnumerable<Contact>;
            return contacts;
        }

        public Contact GetContact(int contactId)
        {
            throw new NotImplementedException();
        }
    }
}
