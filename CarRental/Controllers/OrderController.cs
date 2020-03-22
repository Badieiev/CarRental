using CarRental.Models;
using CarRental.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRental.Controllers
{
    public class OrderController : Controller
    {
        private CarRepository carRepository = new CarRepository();
        private OrderRepository orderRepository = new OrderRepository();
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreateOrder(int id)
        {
            var car = carRepository.Get(id);
            if (car == null)
            {
                throw new Exception("car is not exist");
            }
            CarOrderViewModel carOrderViewModel = new CarOrderViewModel{ 
                CarId = car.CarId,
                Name = car.Name,
                Price = car.Price,
                Brand = car.Brand.Brand,
                Type = car.Type.Type,
                ReturnDate= DateTime.Now};
            return View(carOrderViewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateOrder (CarOrderViewModel carOrderViewModel)
        {
            var order = new Order { 
                CarId = carOrderViewModel.CarId,
                Driver=carOrderViewModel.Driver,
                PassportId = carOrderViewModel.PassportId,
                ReturnDate= carOrderViewModel.ReturnDate };
            orderRepository.Add(order);
            orderRepository.SaveChanges();
            return null;
        }

        public ActionResult Payment (CarOrderViewModel carOrderViewModel)
        {
            return null;
        }
    }
}