using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Phonebook.Components;
using Phonebook.Models;
using Phonebook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using Xunit;

namespace Phonebook.Tests
{
    public class PageIndexesPanelTests
    {

        [Fact]
        public void WhenFirstPageTest()
        {
            ContactListViewModel model = new ContactListViewModel
            {
                FilterName = null,
                FilterPhonenumber = null,
                FilterTag = null,
                SortOption = null,
                Contacts = null,
                TotalCount = 299,
                PageCount = 30,
                PageNo = 1,
                PageSize = 10
            };
            
            PageIndexesPanelViewComponent target = new PageIndexesPanelViewComponent();
            Task<ViewViewComponentResult> result = target.InvokeAsync(model);
            result.Wait();
            var view = result.Result;

            int? pageFirst = view.ViewData["PageFirst"] as int?;
            int? pageLast = view.ViewData["PageLast"] as int?;

            Assert.NotNull(pageFirst);
            Assert.NotNull(pageLast);
            Assert.Equal(1, pageFirst.Value);
            Assert.Equal(7, pageLast.Value);
        }

        [Fact]
        public void WhenSecondPageTest()
        {
            ContactListViewModel model = new ContactListViewModel
            {
                FilterName = null,
                FilterPhonenumber = null,
                FilterTag = null,
                SortOption = null,
                Contacts = null,
                TotalCount = 299,
                PageCount = 30,
                PageNo = 2,
                PageSize = 10
            };

            PageIndexesPanelViewComponent target = new PageIndexesPanelViewComponent();
            Task<ViewViewComponentResult> result = target.InvokeAsync(model);
            result.Wait();
            var view = result.Result;

            int? pageFirst = view.ViewData["PageFirst"] as int?;
            int? pageLast = view.ViewData["PageLast"] as int?;

            Assert.NotNull(pageFirst);
            Assert.NotNull(pageLast);
            Assert.Equal(1, pageFirst.Value);
            Assert.Equal(7, pageLast.Value);
        }

        [Fact]
        public void WhenMiddlePageTest()
        {
            ContactListViewModel model = new ContactListViewModel
            {
                FilterName = null,
                FilterPhonenumber = null,
                FilterTag = null,
                SortOption = null,
                Contacts = null,
                TotalCount = 299,
                PageCount = 30,
                PageNo = 15,
                PageSize = 10
            };

            PageIndexesPanelViewComponent target = new PageIndexesPanelViewComponent();
            Task<ViewViewComponentResult> result = target.InvokeAsync(model);
            result.Wait();
            var view = result.Result;

            int? pageFirst = view.ViewData["PageFirst"] as int?;
            int? pageLast = view.ViewData["PageLast"] as int?;

            Assert.NotNull(pageFirst);
            Assert.NotNull(pageLast);
            Assert.Equal(12, pageFirst.Value);
            Assert.Equal(18, pageLast.Value);
        }

        [Fact]
        public void WhenPrelastPageTest()
        {
            ContactListViewModel model = new ContactListViewModel
            {
                FilterName = null,
                FilterPhonenumber = null,
                FilterTag = null,
                SortOption = null,
                Contacts = null,
                TotalCount = 299,
                PageCount = 30,
                PageNo = 29,
                PageSize = 10
            };

            PageIndexesPanelViewComponent target = new PageIndexesPanelViewComponent();
            Task<ViewViewComponentResult> result = target.InvokeAsync(model);
            result.Wait();
            var view = result.Result;

            int? pageFirst = view.ViewData["PageFirst"] as int?;
            int? pageLast = view.ViewData["PageLast"] as int?;

            Assert.NotNull(pageFirst);
            Assert.NotNull(pageLast);
            Assert.Equal(24, pageFirst.Value);
            Assert.Equal(30, pageLast.Value);
        }

        [Fact]
        public void WhenLastPageTest()
        {
            ContactListViewModel model = new ContactListViewModel
            {
                FilterName = null,
                FilterPhonenumber = null,
                FilterTag = null,
                SortOption = null,
                Contacts = null,
                TotalCount = 299,
                PageCount = 30,
                PageNo = 30,
                PageSize = 10
            };

            PageIndexesPanelViewComponent target = new PageIndexesPanelViewComponent();
            Task<ViewViewComponentResult> result = target.InvokeAsync(model);
            result.Wait();
            var view = result.Result;

            int? pageFirst = view.ViewData["PageFirst"] as int?;
            int? pageLast = view.ViewData["PageLast"] as int?;

            Assert.NotNull(pageFirst);
            Assert.NotNull(pageLast);
            Assert.Equal(24, pageFirst.Value);
            Assert.Equal(30, pageLast.Value);
        }

        [Fact]
        public void WhenEmptyList_Test()
        {
            ContactListViewModel model = new ContactListViewModel
            {
                FilterName = null,
                FilterPhonenumber = null,
                FilterTag = null,
                SortOption = null,
                Contacts = null,
                TotalCount = 0,
                PageCount = 0,
                PageNo = 1,
                PageSize = 10
            };

            PageIndexesPanelViewComponent target = new PageIndexesPanelViewComponent();
            Task<ViewViewComponentResult> result = target.InvokeAsync(model);
            result.Wait();
            var view = result.Result;

            int? pageFirst = view.ViewData["PageFirst"] as int?;
            int? pageLast = view.ViewData["PageLast"] as int?;

            Assert.NotNull(pageFirst);
            Assert.NotNull(pageLast);
            Assert.Equal(1, pageFirst.Value);
            Assert.Equal(1, pageLast.Value);
        }
    }
}
