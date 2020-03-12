using CarRental.App_Start;
using CarRental.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CarRental.Models
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

            CarBrand[] brands =
            {
                new CarBrand { Brand="Toyota" },
                new CarBrand { Brand="Volvo" },
                new CarBrand { Brand="KIA" },
                new CarBrand { Brand="ZaZ" }
            };

            CarType[] types =
            {
                new CarType { Type="Econom" },
                new CarType { Type="Comfort" },
                new CarType { Type="Business" }
            };

            Car [] cars =
            {
                new Car {
                    Name = "Corolla",
                    Price = 100,
                    Brand = brands[0],
                    Type = types[0]
                },
                new Car {
                    Name = "Yaris",
                    Price = 150,
                    Brand = brands[0],
                    Type = types[0]
                },
                new Car {
                    Name = "Camry",
                    Price = 200,
                    Brand = brands[0],
                    Type = types[1]
                },
                new Car {
                    Name = "Land Cruiser",
                    Price = 300,
                    Brand = brands[0],
                    Type = types[2]
                },
                new Car {
                    Name = "RAV 4",
                    Price = 500,
                    Brand = brands[0],
                    Type = types[2]
                },

                new Car {
                    Name = "XC90",
                    Price = 400,
                    Brand = brands[1],
                    Type = types[1]
                },
                new Car {
                    Name = "S60",
                    Price = 350,
                    Brand = brands[1],
                    Type = types[2]
                },

                new Car {
                    Name = "Ceed",
                    Price = 350,
                    Brand = brands[2],
                    Type = types[1]
                },
                new Car {
                    Name = "Rio",
                    Price = 450,
                    Brand = brands[2],
                    Type = types[1]
                },
                new Car {
                    Name = "Soul",
                    Price = 550,
                    Brand = brands[2],
                    Type = types[2]
                },
                new Car {
                    Name = "Sportage",
                    Price = 550,
                    Brand = brands[2],
                    Type = types[1]
                },

                new Car {
                    Name = "Lanos",
                    Price = 50,
                    Brand = brands[3],
                    Type = types[0]
                },
            };

            CarRepository carRepository = new CarRepository();

            foreach(var car in cars)
            {
                carRepository.Add(car);
            }

            carRepository.SaveChanges();

            base.Seed(context);
        }
    }
}