using CarRental.Models;
using CarRental.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRental.Controllers
{    
    public class HomeController : Controller
    {
        private CarRepository carRepository = new CarRepository();
        private CarBrandRepository carBrandRepository = new CarBrandRepository();
        private CarTypeRepository carTypeRepository = new CarTypeRepository();

        [AllowAnonymous]
        public ActionResult Index(int page = 1, string Brand = "All", string Type = "All", string SortType = "Price")
        {
            int pageSize = 5;
            var cars = carRepository.FindCars(pageSize * (page-1), pageSize, Brand, Type, SortType);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = carRepository.FilteredCarsNumber(Brand, Type) };
            
            CarPageViewModel carPageViewModel = new CarPageViewModel { Cars = cars, PageInfo = pageInfo };

            var brands = carBrandRepository.GetAll().ToList();
            brands.Insert(0, new CarBrand { Brand = "All", BrandID = 0 });            
            carPageViewModel.Brands = new SelectList(brands.Select(brand => brand.Brand).ToList(), Brand);

            var carTypes = carTypeRepository.GetAll().ToList();
            carTypes.Insert(0, new CarType { Type = "All", TypeId = 0 });
            carPageViewModel.CarTypes = new SelectList(carTypes.Select(type => type.Type).ToList(), Type);

            carPageViewModel.SortType = new SelectList(new List<string> { "Price", "Name" }, SortType);

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