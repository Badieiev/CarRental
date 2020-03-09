using IdentityTest2.Models;
using IdentityTest2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentityTest2.Controllers
{    
    public class HomeController : Controller
    {
        private CarRepository carRepository = new CarRepository();

        [AllowAnonymous]
        public ActionResult Index(int page=1)
        {
            int pageSize = 5;
            var cars = carRepository.FindCars(pageSize * (page-1), pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = carRepository.TotalCarsNumber() };
            CarPageViewModel carPageViewModel = new CarPageViewModel { Cars = cars, PageInfo = pageInfo };
            return View(carPageViewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}