using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Phonebook.Models.ViewModels;

namespace Phonebook.Models.ViewModels
{
    public class PageIndexesPanelViewComponent: ViewComponent
    {
        private const int PageNeightboreNum = 3;

        public async Task<ViewViewComponentResult> InvokeAsync(ContactListViewModel model)
        {
            int pageFirst = model.PageNo > 1 + PageNeightboreNum
                ? model.PageNo - PageNeightboreNum
                : 1;
            int pageLast = pageFirst + 2 * PageNeightboreNum < model.PageCount
                ? pageFirst + 2 * PageNeightboreNum
                : model.PageCount;
            int range = pageLast - pageFirst;

            if (range < 2 * PageNeightboreNum)
            {
                int diff = 2 * PageNeightboreNum - range;
                pageFirst = (pageFirst - diff > 1)
                    ? pageFirst - diff
                    : 1;
            }
            ViewBag.PageFirst = pageFirst;
            ViewBag.PageLast = pageLast;
            return View((object)model);
        }
    }
}
