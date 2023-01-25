using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}
