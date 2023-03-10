using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Models
{
    public class SqlContactRepository : IContactRepository
    {
        private ContactDbTool contactsDbTool;
        private TagDbTool tagsDbTool;
        private ContactsTagsDbTool contactsTagsDbTool;

        public SqlContactRepository(string connectionString)
        {
            contactsDbTool = new ContactDbTool { ConnectionString = connectionString };
            tagsDbTool = new TagDbTool { ConnectionString = connectionString };
            contactsTagsDbTool = new ContactsTagsDbTool { ConnectionString = connectionString };
        }

        public IEnumerable<Contact> Contacts
        {
            get => GetContacts();
        }
        
        public string FilterName { get; set; }
        public string FilterPhone { get; set; }
        public string SortColumn { get; set ; }
        public string FilterTag { get; set; }
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
                filterTag: FilterTag,
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

        public Contact GetContact(int contactId)
        {
            int count = 0;
            List<Contact> contacts = new List<Contact>();
            contactsDbTool.SelectById(
                contactId: contactId,
                itemRowReadedFunc: row =>
                {
                    int personId2 = row.GetInt32(row.GetOrdinal("ContactId"));
                    string lastName = row["Lastname"] as string;
                    string firstname = row["Firstname"] as string;
                    string patronimic = row["Patronymic"] as string;
                    string phonenumber = row["Phonenumber"] as string;

                    contacts.Add(
                        new Contact 
                        {
                            ContactId = personId2,
                            Lastname = lastName,
                            Firstname = firstname,
                            Patronymic = patronimic,
                            Phonenumber = phonenumber
                        });
                    count++;
                });
            Contact contact = contacts.Count == 0
                ? null
                : contacts[0];
            
            if (contact != null)
            {
                contact.Tags = GetTags(contactId);
            }
            return contact;
        }

        public IEnumerable<string> GetTags(int contactId)
        {
            int count = 0;
            List<string> tags = new List<string>();
            tagsDbTool.SelectByContact(
                contactId: contactId,
                itemRowReadedFunc: row =>
                {
                    string tag = row["Tag"] as string;
                    if (String.IsNullOrEmpty(tag))
                    {
                        return;
                    }
                    tags.Add(tag);
                    count++;
                });
            return tags;
        }

        public void AddContact(Contact contact)
        {
            int insertedId = contactsDbTool.Insert(lastname: contact.Lastname, 
                firstname: contact.Firstname, 
                patronymic: contact.Patronymic, 
                phonenumber: contact.Phonenumber);

            foreach (string tag in contact.Tags ?? Enumerable.Empty<string>())
            {
                contactsTagsDbTool.Insert(contactId: insertedId, tag: tag);
            }
            return;
        }

        public void DeleteContact(int contactId)
        {
            contactsDbTool.Delete(contactId);
            contactsTagsDbTool.DeleteByContact(contactId);
            return;
        }

        public void SaveContact(Contact contact)
        {
            contactsDbTool.Update(contactId: contact.ContactId, 
                lastname: contact.Lastname, 
                firstname: contact.Firstname, 
                patronymic: contact.Patronymic, 
                phonenumber: contact.Phonenumber);

            contactsTagsDbTool.DeleteByContact(contactId: contact.ContactId);
            foreach (string tag in contact.Tags ?? Enumerable.Empty<string>())
            {
                contactsTagsDbTool.Insert(contactId: contact.ContactId, tag: tag);
            }
            return;
        }
    }
}
