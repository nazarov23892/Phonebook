using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Phonebook.Models;

namespace Phonebook.Controllers
{
    public class TagController : Controller
    {
        private ITagRepository tagRepository;

        public TagController(ITagRepository repository)
        {
            tagRepository = repository;
        }

        public IActionResult List()
        {
            return View(tagRepository.Tags);
        }

    }
}
