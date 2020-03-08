using IdentityTest2.App_Start;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IdentityTest2.Models
{
    public class DatabaseInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());

            var adminRole = new IdentityRole { Name = "admin" };
            var managerRole = new IdentityRole { Name = "manager" };
            var userRole = new IdentityRole { Name = "user" };

            roleManager.Create(adminRole);
            roleManager.Create(managerRole);
            roleManager.Create(userRole);

            var admin = new ApplicationUser { Email = "admin@mail.com", UserName = "admin@mail.com" };
            string password = "Admin_1";

            var result = userManager.Create(admin, password);

            if (result.Succeeded)
            {
                userManager.AddToRole(admin.Id, adminRole.Name);
                userManager.AddToRole(admin.Id, managerRole.Name);
            }

            base.Seed(context);
        }
    }
}