using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Phonebook.Models;
using Phonebook.Models.ViewModels;

namespace Phonebook.Controllers
{
    public class ContactController : Controller
    {
        private IContactRepository contactsRepository;

        public int PageSize { get; private set; } = 12;

        public ContactController(IContactRepository repository)
        {
            contactsRepository = repository;
        }

        public IActionResult List(int page = 1, string fname = null)
        {
            var contacts = contactsRepository.Contacts
                .Where(p => 
                    String.IsNullOrEmpty(fname) 
                    || p.Lastname.Contains(fname, StringComparison.OrdinalIgnoreCase)
                    || p.Firstname.Contains(fname, StringComparison.OrdinalIgnoreCase));

            ContactListViewModel viewModel = new ContactListViewModel
            {
                PageSize = this.PageSize,
                Contacts = contacts,
                PageNo = page,
                FilterName = fname
            };
            return View(viewModel);
        }

        public ViewResult Show(int id)
        {
            var contact = contactsRepository.Contacts
                .FirstOrDefault(i => i.ContactId == id);
            return View(contact);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return View(contact);
            }

            contactsRepository.AddContact(contact);
            return RedirectToAction(
                actionName: nameof(this.List),
                controllerName: "Contact");
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            var contact = contactsRepository.Contacts
                .FirstOrDefault(i => i.ContactId == id);
            return View(contact);
        }

        [HttpPost]
        public IActionResult Edit(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return View(contact);
            }
            contactsRepository.SaveContact(contact);

            return RedirectToAction(
                actionName: nameof(this.List),
                controllerName: "Contact");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            contactsRepository.DeleteContact(id);
            return RedirectToAction(
                actionName: nameof(this.List),
                controllerName: "Contact");
        }
    }
}
