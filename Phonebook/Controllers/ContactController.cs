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

        public IActionResult List(int page = 1, string fname = null, string fphone = null,
            string fsort = null)
        {
            var contacts = contactsRepository.Contacts;
            if (fsort == "lastname-asc")
            {
                contacts = contacts.OrderBy(keySelector: c => c.Lastname);
            }
            else if (fsort == "lastname-desc")
            {
                contacts = contacts.OrderByDescending(keySelector: c => c.Lastname);
            }
            else if (fsort == "firstname-asc")
            {
                contacts = contacts.OrderBy(keySelector: c => c.Firstname);
            }
            else if (fsort == "firstname-desc")
            {
                contacts = contacts.OrderByDescending(keySelector: c => c.Firstname);
            }

            contacts = contacts
            .Where(c =>
                String.IsNullOrEmpty(fphone)
                || c.Phonenumber.Contains(fphone, StringComparison.OrdinalIgnoreCase))
            .Where(p =>
                String.IsNullOrEmpty(fname)
                || p.Lastname.Contains(fname, StringComparison.OrdinalIgnoreCase)
                || p.Firstname.Contains(fname, StringComparison.OrdinalIgnoreCase));

            ContactListViewModel viewModel = new ContactListViewModel
            {
                PageSize = this.PageSize,
                Contacts = contacts,
                PageNo = page,
                FilterName = fname,
                FilterPhonenumber = fphone,
                SortOption = fsort
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
