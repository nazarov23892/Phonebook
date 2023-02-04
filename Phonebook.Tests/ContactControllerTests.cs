using System;
using System.Linq;
using Xunit;
using Moq;
using System.Collections.Generic;
using Phonebook.Controllers;
using Phonebook.Models;
using Phonebook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Phonebook.Tests
{
    public class ContactControllerTests
    {
        private IEnumerable<Contact> GenerateContactList(int count)
        {
            List<Contact> contacts = new List<Contact>();
            for (int i = 0; i < count; i++)
            {
                contacts.Add(new Contact { ContactId = 1 + i, Firstname = $"c-{1 + i}" });
            }
            return contacts;
        }

        [Fact]
        public void PaginationTest()
        {
            // prepare
            var mock = new Mock<IContactRepository>();
            mock.Setup(p => p.Contacts)
                .Returns(GenerateContactList(29));

            ContactController controller = new ContactController(contactsRepo: mock.Object, null)
            {
                PageSize = 10
            };

            // act
            var result = controller.List(new ContactListViewModel
            {
                Contacts = null,
                FilterName = null,
                FilterPhonenumber = null,
                FilterTag = null,
                SortOption = null,
                PageCount = 0,
                PageNo = 2,
                PageSize = 0,
                TotalCount = 0
            }) as ViewResult;
            ContactListViewModel viewModel = result.ViewData.Model as ContactListViewModel;

            // assert
            Assert.Equal(2, viewModel.PageNo);
            Assert.Equal(10, viewModel.PageSize);
            Assert.Equal(3, viewModel.PageCount);
            Assert.Equal(29, viewModel.TotalCount);
        }

        [Fact]
        public void PaginatonItemsWhenFirstPageTest()
        {
            // prepare
            var mock = new Mock<IContactRepository>();
            mock.Setup(p => p.Contacts)
                .Returns(GenerateContactList(29));

            ContactController controller = new ContactController(contactsRepo: mock.Object, null)
            {
                PageSize = 10
            };

            // act
            var result = controller.List(new ContactListViewModel
            {
                Contacts = null,
                FilterName = null,
                FilterPhonenumber = null,
                FilterTag = null,
                SortOption = null,
                PageCount = 0,
                PageNo = 1,
                PageSize = 0,
                TotalCount = 0
            }) as ViewResult;
            ContactListViewModel viewModel = result.ViewData.Model as ContactListViewModel;

            Contact[] contacts = viewModel.Contacts.ToArray();

            // assert
            Assert.Equal(10, contacts.Length);
            Assert.Equal(1, contacts[0].ContactId);
            Assert.Equal(10, contacts[9].ContactId);
        }

        [Fact]
        public void PaginatonItemsWhenLastPageTest()
        {
            // prepare
            var mock = new Mock<IContactRepository>();
            mock.Setup(p => p.Contacts)
                .Returns(GenerateContactList(29));

            ContactController controller = new ContactController(contactsRepo: mock.Object, null)
            {
                PageSize = 10
            };

            // act
            var result = controller.List(new ContactListViewModel
            {
                Contacts = null,
                FilterName = null,
                FilterPhonenumber = null,
                FilterTag = null,
                SortOption = null,
                PageCount = 0,
                PageNo = 3,
                PageSize = 0,
                TotalCount = 0
            }) as ViewResult;
            ContactListViewModel viewModel = result.ViewData.Model as ContactListViewModel;

            Contact[] contacts = viewModel.Contacts.ToArray();

            // assert
            Assert.Equal(9, contacts.Length);
            Assert.Equal(21, contacts[0].ContactId);
            Assert.Equal(29, contacts[8].ContactId);
        }
    }
}
