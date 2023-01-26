using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Phonebook.Models;

namespace Phonebook.Controllers
{
    public class ContactController : Controller
    {
        private IContactRepository contactsRepository;

        public ContactController(IContactRepository repository)
        {
            contactsRepository = repository;
        }

        public IActionResult List()
        {
            return View(contactsRepository.Contacts);
        }
    }
}
