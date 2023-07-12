using Ecommerce.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.controller
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _he;

        public ProductController(AppDbContext db, IWebHostEnvironment he)
        {
            _db = db;
            _he = he;
        }
        [HttpGet]
        public IActionResult Index(Products products)
        {
            return View(_db.products.Include(c => c.ProductType).Include(f => f.SpecialTag).ToList());
        }

        [HttpPost]
        public IActionResult Index(decimal? LargeAmount, decimal? LowAmount)
        {
            var products = _db.products.Include(c => c.ProductType).Include(c => c.SpecialTag)
             .Where(c => c.Price >= LowAmount && c.Price <= LargeAmount).ToList();

            if (LowAmount == null || LargeAmount == null)
            {
                products = _db.products.Include(c => c.ProductType).Include(c => c.SpecialTag).ToList();         
            }
            return View(products);
        }
        [HttpGet]
        public IActionResult create()
        {
            ViewData["productTypeid"] = new SelectList(_db.productTypes.ToList(), "id", "Name");
            ViewData["Tagid"] = new SelectList(_db.specialTags.ToList(), "id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> create(Products products, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                var searchProduct = _db.products.FirstOrDefault(c => c.Name == products.Name);
                if (searchProduct != null)
                {
                    ViewBag.message = "This Product is already Exist";
                    ViewData["productTypeid"] = new SelectList(_db.productTypes.ToList(), "id", "Name");
                    ViewData["Tagid"] = new SelectList(_db.specialTags.ToList(), "id", "Name");
                    return View(products);
                }
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/images",
                                             Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    products.Image = "images/" + image.FileName;
                }
                if (image == null)
                {
                    products.Image = "images/noimage.png";
                }
                _db.products.Add(products);
                await _db.SaveChangesAsync();
                TempData["save"] = "Product has been saved";
                return RedirectToAction("index");
            }
            return View(products);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ViewData["productTypeid"] = new SelectList(_db.productTypes.ToList(), "id", "Name");
            ViewData["Tagid"] = new SelectList(_db.specialTags.ToList(), "id", "Name");
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }
            var product = _db.products.Include(c => c.ProductType).Include(c => c.SpecialTag)
                                        .FirstOrDefault(c => c.id == id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Products products, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/images",
                                             Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    products.Image = "images/" + image.FileName;
                }
                if (image == null)
                {
                    products.Image = "images/noimage.png";
                }
                _db.products.Update(products);
                await _db.SaveChangesAsync();
                return RedirectToAction("index");
            }
            return View(products);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }
            var product = _db.products.Include(c => c.ProductType).Include(c => c.SpecialTag)
                                        .FirstOrDefault(c => c.id == id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }
            return View(product);
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }
            var product = _db.products.Include(c => c.ProductType).Include(c => c.SpecialTag)
                                               .Where(c => c.id == id).FirstOrDefault();
            if (product == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }
            return View(product);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }
            var product = _db.products.FirstOrDefault(c => c.id == id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }
            _db.products.Remove(product);
            await _db.SaveChangesAsync();
            return RedirectToAction("index");

        }
    }
}
