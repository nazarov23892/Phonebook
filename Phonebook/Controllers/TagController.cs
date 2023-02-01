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

        [HttpPost]
        public IActionResult Create(string Tag)
        {
            if (CheckValidTag(Tag))
            {
                tagRepository.AddTag(Tag);
            }
            return RedirectToAction(actionName: nameof(this.List), controllerName: "Tag");
        }

        [HttpPost]
        public IActionResult Delete(string tag)
        {
            tagRepository.DeleteTag(tag);
            return RedirectToAction(actionName: nameof(this.List), controllerName: "Tag");
        }

        private bool CheckValidTag(string Tag)
        {
            return !String.IsNullOrEmpty(Tag);
        }
    }
}
