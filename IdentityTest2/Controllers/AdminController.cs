using IdentityTest2.App_Start;
using IdentityTest2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentityTest2.Controllers
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
        public ActionResult Index()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            var managerRole = roleManager.FindByName("manager");

            if (managerRole == null)
                return HttpNotFound();

            var managers = TheUserManager.Users
                .Where(u => u.Roles.Select(r => r.RoleId)
                .Contains(managerRole.Id));
            
            if (managers == null)
                return HttpNotFound();

            return View(managers);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult EditManager(string id)
        {
            var manager = TheUserManager.FindById(id);

            if (manager == null)
                return HttpNotFound();

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult EditManager(ApplicationUser manager)
        {
            return null;
        }
    }
}