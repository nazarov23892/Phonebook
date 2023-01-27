using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Phonebook.Models.ViewModels
{
    public class PageIndexesPanelViewComponent: ViewComponent
    {
        public async Task<ViewViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
