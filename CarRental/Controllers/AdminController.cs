using CarRental.App_Start;
using CarRental.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace CarRental.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AdminController()
        {

        }

        public AdminController(ApplicationSignInManager signInManage, ApplicationUserManager userManager)
        {
            _signInManager = signInManage;
            _userManager = userManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager TheUserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult ListManagers()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            var managerRole = roleManager.FindByName("manager");

            if (managerRole == null)
                throw new HttpRequestException();

            var managers = TheUserManager.Users
                .Where(u => u.Roles.Select(r => r.RoleId)
                .Contains(managerRole.Id));
            
            if (managers == null)
                throw new HttpRequestException();

            return View(managers);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult EditManager(string id)
        {
            var manager = TheUserManager.FindById(id);

            if (manager == null)
                throw new HttpRequestException();

            return View(manager);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult EditManager(ApplicationUser manager)
        {
            if (!ModelState.IsValid) throw new HttpRequestException();

            var user = TheUserManager.FindById(manager.Id);
            if (user == null) throw new HttpRequestException();

            user.Email = manager.Email;
            user.UserName = manager.Email;

            TheUserManager.Update(user);

            return RedirectToAction("ListManagers");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult CreateManager()
        {
            if (!ModelState.IsValid) throw new HttpRequestException();

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult CreateManager(RegisterViewModel model)
        {
            if (!ModelState.IsValid) throw new HttpRequestException();

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };            
            var result = TheUserManager.Create(user, model.Password);
            if (result.Succeeded)
            {
                var manager = TheUserManager.FindByEmail(user.Email);
                TheUserManager.AddToRole(manager.Id, "manager");
                return RedirectToAction("ListManagers", "Admin");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteManager(string id)
        {
            if (!ModelState.IsValid) throw new HttpRequestException();

            var manager = TheUserManager.FindById(id);

            if (manager == null)
                return RedirectToAction("ListManagers", "Admin");

            TheUserManager.Delete(manager);

            return RedirectToAction("ListManagers", "Admin");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult ListUsers()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            var userRole = roleManager.FindByName("user");

            if (userRole == null)
                throw new HttpRequestException();

            var managers = TheUserManager.Users
                .Where(u => u.Roles.Select(r => r.RoleId)
                .Contains(userRole.Id));

            if (managers == null)
                throw new HttpRequestException();

            return View(managers);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult BlockUser(string id)
        {
            var user = TheUserManager.FindById(id);           

            if (user == null)
                throw new HttpRequestException();

            user.IsBlocked = !user.IsBlocked;
            TheUserManager.Update(user);

            return Json(new { IsBlocked = user.IsBlocked });
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult EditUser(string id)
        {
            var user = TheUserManager.FindById(id);

            if (user == null)
                throw new HttpRequestException();

            return View(user);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult EditUser(ApplicationUser user)
        {
            if (!ModelState.IsValid) throw new HttpRequestException();

            var existingUser = TheUserManager.FindById(user.Id);
            if (existingUser == null) throw new HttpRequestException();

            existingUser.Email = user.Email;
            existingUser.UserName = user.Email;

            TheUserManager.Update(existingUser);

            return RedirectToAction("ListUsers");
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteUser(string id)
        {
            if (!ModelState.IsValid) throw new HttpRequestException();

            var user = TheUserManager.FindById(id);

            if (user == null)
                return RedirectToAction("ListUsers", "Admin");

            TheUserManager.Delete(user);

            return RedirectToAction("ListUsers", "Admin");
        }
    }
}