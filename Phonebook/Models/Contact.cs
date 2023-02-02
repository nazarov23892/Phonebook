using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Phonebook.Models
{
    public class Contact
    {
        public int ContactId { get; set; }

        [StringLength(maximumLength:20)]
        public string Firstname { get; set; }

        [StringLength(maximumLength: 20)]
        public string Lastname { get; set; }

        [StringLength(maximumLength: 20)]
        public string Patronymic { get; set; }

        [Required]
        [StringLength(maximumLength: 11, MinimumLength = 6, 
            ErrorMessage = "phonenumber length must be between 6 and 11 digits")]
        [RegularExpression(pattern: "([0-9]+)", 
            ErrorMessage = "phonenumber should have only digits")]
        public string Phonenumber { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}
