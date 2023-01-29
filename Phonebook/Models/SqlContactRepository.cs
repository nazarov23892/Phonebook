using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Models
{
    public class SqlContactRepository : IContactRepository
    {
        private ContactDbTool contactsDbTool = new ContactDbTool();

        public IEnumerable<Contact> Contacts
        {
            get => GetContacts();
        }
        
        public string FilterName { get; set; }
        public string FilterPhone { get; set; }
        public string SortColumn { get; set ; }
        public SortDirection SortDirection { get; set; }

        private IEnumerable<Contact> GetContacts()
        {
            List<Contact> contacts = new List<Contact>();

            OrderDirection orderDirection = SortDirection == SortDirection.Descending
                ? OrderDirection.Descending
                : OrderDirection.Ascending;

            int count = 0;
            contactsDbTool.Select(
                filterName: FilterName,
                filterPhone: FilterPhone,
                sortColumn: SortColumn,
                orderDirection: orderDirection,
                itemRowReadedFunc: row =>
                {
                    int contactId = row.GetInt32(row.GetOrdinal("ContactId"));
                    string lastName = row["Lastname"] as string;
                    string firstname = row["Firstname"] as string;
                    string patronymic = row["Patronymic"] as string;
                    string phonenumber = row["Phonenumber"] as string;

                    contacts.Add(
                        new Contact
                        {
                            ContactId = contactId,
                            Lastname = lastName,
                            Firstname = firstname,
                            Patronymic = patronymic,
                            Phonenumber = phonenumber
                        });
                    count++;
                });
            return contacts;
        }

        public void AddContact(Contact contact)
        {
            contactsDbTool.Insert(lastname: contact.Lastname, 
                firstname: contact.Firstname, 
                patronymic: contact.Patronymic, 
                phonenumber: contact.Phonenumber);
            return;
        }

        public void DeleteContact(int contactId)
        {
            contactsDbTool.Delete(contactId);
            return;
        }

        public void SaveContact(Contact contact)
        {
            contactsDbTool.Update(contactId: contact.ContactId, 
                lastname: contact.Lastname, 
                firstname: contact.Firstname, 
                patronymic: contact.Patronymic, 
                phonenumber: contact.Phonenumber);
            return;
        }
    }
}
