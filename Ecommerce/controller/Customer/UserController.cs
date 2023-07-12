using Ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Customer.Controllers
{

    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly AppDbContext _db;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<IdentityUser> userManager, AppDbContext db ,
            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            _db = db;
           _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View(_db.ApplicationUsers.ToList());
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                var result = await userManager.CreateAsync(user, user.PasswordHash);
                if (result.Succeeded)
                {
                //  var isSaveRole =  await userManager.AddToRoleAsync(user,role:"User");
                    TempData["Save"] = "User has been created successfully";
                    return RedirectToAction("index");

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(c => c.Id == id);
            if (user == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id);
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUser user)
        {
            var userInfo = _db.ApplicationUsers.FirstOrDefault(c => c.Id == user.Id);
            if (userInfo == null)
            {

                Response.StatusCode = 404;
                return View("ProductNotFound", user.Id);
            }

            userInfo.FirstName = user.FirstName;
            userInfo.LastName = user.LastName;
            var result = await userManager.UpdateAsync(userInfo);
            if (result.Succeeded)
            {
                TempData["Save"] = "User has been updated successfully";
                return RedirectToAction("index");

            }
            return View(userInfo);

        }
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(c => c.Id == id);
            if (user == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id);
            }
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Locout(string id)
        {

            if (id == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id);
            }
            var user = _db.ApplicationUsers.FirstOrDefault(c => c.Id == id);

            if (user == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id);
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Locout(ApplicationUser user)
        {
            var userInfo = _db.ApplicationUsers.FirstOrDefault(c => c.Id == user.Id);
            if (userInfo == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound",user.Id);
            }
            userInfo.LockoutEnd = DateTime.Now.AddYears(100);
            int rowAffected = _db.SaveChanges();
            if (rowAffected > 0)
            {

                TempData["Save"] = "User has been Lockout successfully";
                return RedirectToAction("index");
            }
            return View(userInfo);

        }

      
        public async Task<IActionResult> Active(string id)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(c => c.Id == id);
            if (user == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id);
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Active(ApplicationUser user)
        {
            var userInfo = _db.ApplicationUsers.FirstOrDefault(c => c.Id == user.Id);
            if (userInfo == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", user.Id);
            }
            userInfo.LockoutEnd = DateTime.Now.AddDays(-1);
            int rowAffected = _db.SaveChanges();
            if (rowAffected > 0)
            {

                TempData["Save"] = "User has been active successfully";
                return RedirectToAction("index");
            }
            return View(userInfo);

        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(c => c.Id == id);
            if (user == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id);
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ApplicationUser user)
        {
            var userInfo = _db.ApplicationUsers.FirstOrDefault(c => c.Id == user.Id);
            if (userInfo == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", user.Id);
            }
            _db.ApplicationUsers.Remove(userInfo);
            int rowAffected = _db.SaveChanges();
            if (rowAffected > 0)
            {

                TempData["Save"] = "User has been delete successfully";
                return RedirectToAction("index");
            }
            return View(userInfo);

        }
    }
}

