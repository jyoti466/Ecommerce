using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.controller
{
    public class SpecialTagController : Controller
    {
        private readonly AppDbContext _app;

        public SpecialTagController(AppDbContext app)
        {
            _app = app;
        }
        public ViewResult Index(SpecialTag specialTag)
        {
            return View(_app.specialTags.ToList());


        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpecialTag specialTag)
        {
            if (ModelState.IsValid)
            {
                _app.specialTags.Add(specialTag);
                TempData["save"] = "Product type has been saved";
                await _app.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(specialTag);
        }

        [HttpGet]
        public ViewResult Edit(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }
            var special = _app.specialTags.Find(id);
            if (special == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }
            return View(special);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SpecialTag specialTag)
        {
            if (ModelState.IsValid)
            {
                _app.Update(specialTag);
                await _app.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(specialTag);
        }


       

        [HttpGet]
        public ViewResult Details(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }
            var special = _app.specialTags.Find(id);
            if (special == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }
            return View(special);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(SpecialTag specialTag)
        {
            return RedirectToAction("index");
        }


        [HttpGet]
        public ViewResult Delete(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }
            var special = _app.specialTags.Find(id);
            if (special == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }
            return View(special);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, SpecialTag specialTag)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }

            if (id != specialTag.id)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }
            var special = _app.specialTags.Find(id);
            if (specialTag == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }

            if (ModelState.IsValid)
            {
                _app.Remove(special);
                await _app.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(specialTag);
        }



    }
}

    
   





