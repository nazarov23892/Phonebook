using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using Phonebook.Models;

namespace ContactsGenerateUtil
{
    class ContactParser
    {
        private List<Contact> contacts = new List<Contact>();

        public IEnumerable<Contact> Contacts { get => contacts; }

        public StreamReader StreamReader { get; set; }

        public int Parse()
        {
            if (StreamReader == null )
            {
                return 0;
            }
            contacts.Clear();
            int count = 0;
            while (!StreamReader.EndOfStream)
            {
                string line = StreamReader.ReadLine();
                Contact contact = ParseContact(line);
                if (contact == null)
                {
                    continue;
                }
                contact.ContactId = count + 1;
                contacts.Add(contact);
                count++;
            }
            return count;
        }

        private Contact ParseContact(string srcLine)
        {
            if (String.IsNullOrEmpty(srcLine))
            {
                return null;
            }
            string[] strArr = srcLine.Split(new[] { "  ", " " },
                StringSplitOptions.RemoveEmptyEntries);
            if (strArr.Length != 3)
            {
                return null;
            }
            foreach (var item in strArr)
            {
                if (!CheckName(item))
                {
                    return null;
                }
            }
            Contact contact = new Contact
            {
                Lastname = strArr[0],
                Firstname = strArr[1],
                Patronymic = strArr[2]
            };
            return contact;
        }

        private bool CheckName(string Name)
        {
            return !String.IsNullOrEmpty(Name) || Name.All(c => Char.IsLetter(c));
        }
    }
}
