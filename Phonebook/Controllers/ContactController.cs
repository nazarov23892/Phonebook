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
        private ITagRepository tagsRepository;

        public int PageSize { get; set; } = 10;

        public ContactController(IContactRepository contactsRepo, ITagRepository tagsRepo)
        {
            contactsRepository = contactsRepo;
            tagsRepository = tagsRepo;
        }

        public IActionResult List(ContactListViewModel viewModel)
        {
            var sortOption = (Column: "", SortDir: SortDirection.Ascending);
            if (viewModel.SortOption == "lastname-asc")
            {
                sortOption.Column = "Lastname";
                sortOption.SortDir = SortDirection.Ascending;
            }
            else if (viewModel.SortOption == "lastname-desc")
            {
                sortOption.Column = "Lastname";
                sortOption.SortDir = SortDirection.Descending;
            }
            else if (viewModel.SortOption == "firstname-asc")
            {
                sortOption.Column = "Firstname";
                sortOption.SortDir = SortDirection.Ascending;
            }
            else if (viewModel.SortOption == "firstname-desc")
            {
                sortOption.Column = "Firstname";
                sortOption.SortDir = SortDirection.Descending;
            }
            contactsRepository.SortColumn = sortOption.Column;
            contactsRepository.SortDirection = sortOption.SortDir;
            contactsRepository.FilterName = viewModel.FilterName;
            contactsRepository.FilterPhone = viewModel.FilterPhonenumber;
            contactsRepository.FilterTag = viewModel.FilterTag;

            IEnumerable<Contact> contacts = contactsRepository.Contacts;
            int pageCount = (contacts.Count() / PageSize)
                + (contacts.Count() % PageSize > 0 ? 1 : 0);

            int rangeStart = (viewModel.PageNo - 1) * PageSize; 
            IEnumerable<Contact> subset = contacts
                .Skip(rangeStart)
                .Take(PageSize);

            viewModel.TotalCount = contacts.Count();
            viewModel.PageSize = PageSize;
            viewModel.PageCount = pageCount;
            viewModel.Contacts = subset;
            return View(viewModel);
        }

        public ViewResult Show(int id)
        {
            Contact contact = contactsRepository.GetContact(id);
            return View(contact);
        }

        [HttpGet]
        public ViewResult Create()
        {
            ContactViewModel contactViewModel = new ContactViewModel
            {
                Contact = null,
                TagList = tagsRepository.Tags
            };
            return View(contactViewModel);
        }

        [HttpPost]
        public IActionResult Create(ContactModifyViewModel contactModel)
        {
            Contact contact = contactModel.Contact;
            ValidateContact(contact, ModelState);
            if (!ModelState.IsValid)
            {
                return View(contact);
            }
            contact.Tags = contactModel.TagsToAssign;
            contactsRepository.AddContact(contact);
            return RedirectToAction(
                actionName: nameof(this.List),
                controllerName: "Contact");
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Contact contact = contactsRepository.GetContact(id);
            ContactViewModel contactViewModel = new ContactViewModel
            {
                Contact = contact,
                TagList = tagsRepository.Tags
            };
            return View(contactViewModel);
        }

        [HttpPost]
        public IActionResult Edit(ContactModifyViewModel contactViewModel)
        {
            Contact contact = contactViewModel.Contact;
            ValidateContact(contact, ModelState);
            if (!ModelState.IsValid)
            {
                return View(contact);
            }
            contact.Tags = contactViewModel.TagsToAssign;
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

        private static void ValidateContact(Contact contact, Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState)
        {
            if (String.IsNullOrEmpty(contact.Lastname)
                && String.IsNullOrEmpty(contact.Firstname))
            {
                modelState.AddModelError("", "firstname or lastname must be non-empty");
            }
        }
    }
}
