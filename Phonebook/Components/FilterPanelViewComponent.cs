using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Phonebook.Models;
using Phonebook.Models.ViewModels;

namespace Phonebook.Components
{
    public class FilterPanelViewComponent: ViewComponent
    {
        private ITagRepository tagRepository;

        public FilterPanelViewComponent(ITagRepository tagRepo)
        {
            tagRepository = tagRepo;
        }

        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>
        {
            [""] = "none",
            ["lastname-asc"] = "by lastname ascending",
            ["lastname-desc"] = "by lastname descending",
            ["firstname-asc"] = "by firstname ascending",
            ["firstname-desc"] = "by firstname descending",
        };

        public async Task<ViewViewComponentResult> InvokeAsync()
        {
            ViewBag.SortOption = keyValuePairs;
            ViewBag.TagList = tagRepository.Tags;
            return View();
        }
    }
}
