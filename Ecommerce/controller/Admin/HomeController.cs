
using Ecommerce.Models;
using Ecommerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.controller
{
   
    public class HomeController:Controller
    {
       

         private readonly AppDbContext _db;

        public HomeController(AppDbContext db)
        {
           _db = db;
           
        }

        public ViewResult Start(ProductType productType)
        {
            return View(_db.productTypes.ToList());
        }
        //[AllowAnonymous]
        public ViewResult Index(ProductType productType)
        {
            return View(_db.productTypes.ToList());


        }

       
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductType productType)
        {
            if (ModelState.IsValid)
            {
                _db.productTypes.Add(productType);
                await _db.SaveChangesAsync();
                TempData["save"] = "Product type has been saved";
                return RedirectToAction("Index");
            }
            return View(productType);
        }

      
        [HttpGet]
        public ViewResult Edit(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }
            var product = _db.productTypes.Find(id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductType productType)
        {
            if (ModelState.IsValid)
            {
                _db.Update(productType);
                await _db.SaveChangesAsync();
                TempData["edit"] = "Product type has been Updated";
                return RedirectToAction("Index");
            }
            return View(productType);
        }


        //private readonly Iproductrepository _productrepository;

        //public HomeController(Iproductrepository productrepository)
        //{
        //   _productrepository = productrepository;
        //}

        //public ViewResult Index()
        //{
        //    var model  = _productrepository.GetAllProduct();
        //    return View(model);

        //}

        [HttpGet]
        public ViewResult Details(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }
            var product = _db.productTypes.Find(id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Details(ProductType productType)
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
            var product = _db.productTypes.Find(id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int ? id,ProductType productType)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }
            
            if (id!=productType.id)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }
            var product = _db.productTypes.Find(id);
            if(productType==null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }

            if (ModelState.IsValid)
            {
                _db.Remove(product);
                await _db.SaveChangesAsync();
                TempData["del"] = "Product type has been deleted";
                return RedirectToAction("Index");
            }
            return View(productType);
        }



    }
}
