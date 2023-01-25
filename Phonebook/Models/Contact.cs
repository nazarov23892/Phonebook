using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Models
{
    public class Contact
    {
        public int ContactId { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Patronymic { get; set; }

        public string Phonenumber { get; set; }

    }
}
