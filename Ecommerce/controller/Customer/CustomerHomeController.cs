using Ecommerce.Models;
using Ecommerce.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace Ecommerce.controller.Customer
{
    public class CustomerHomeController : Controller
    {
        private readonly AppDbContext _db;

        public CustomerHomeController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(int ? page)
        {
            return View(_db.products.Include(c => c.ProductType).Include(c => c.SpecialTag)
                                         .ToList().ToPagedList(page??1,9));
        }
        [HttpGet]
        public ViewResult Details(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }
            var product = _db.products.Include(c => c.ProductType).FirstOrDefault(c => c.id == id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }
            return View(product);
        }

        [HttpPost]
        [ActionName("Details")]
        public ActionResult ProductDetails(int? id)
        {
            List<Products> products = new List<Products>();
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }
            var product = _db.products.Include(c => c.ProductType).FirstOrDefault(c => c.id == id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }
            products = HttpContext.Session.Get<List<Products>>("products");
            if (products == null)
            {
                products = new List<Products>();
            }
            products.Add(product);
            HttpContext.Session.Set("products", products);
            return RedirectToAction("index");
        }

        [HttpGet]
        [ActionName("Remove")]
        public IActionResult RemoveToCart(int? id)
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");

            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }

            }
            return RedirectToAction("Index");
        }

        [HttpPost]

        public IActionResult Remove(int? id)
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");

            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }

            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Cart()
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if(products==null)
            {
                products = new List<Products>();
            }
            return View(products);
        }
    }
}