using CarRental.App_Start;
using CarRental.Models;
using CarRental.Repositories;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CarRental.Controllers
{
    public class OrderController : Controller
    {
        private CarRepository carRepository = new CarRepository();
        private OrderRepository orderRepository = new OrderRepository();
        private ApplicationUserManager _userManager;
       
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

        [Authorize]
        public async Task<ActionResult> MyOrders()
        {
            var user =  await TheUserManager.FindByNameAsync(HttpContext.User.Identity.Name);
            if (user == null)
            {
                throw new Exception("user doesnt exist");
            }
            return View(user.Orders);
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
        public async Task<ActionResult> CreateOrder (CarOrderViewModel carOrderViewModel)
        {
            var user = await TheUserManager.FindByNameAsync(HttpContext.User.Identity.Name);
            if (user == null)
            {
                throw new Exception("user doesnt exist");
            }

            var order = new Order 
            { 
                CarId = carOrderViewModel.CarId,
                Driver = carOrderViewModel.Driver,
                PassportId = carOrderViewModel.PassportId,
                ReturnDate = carOrderViewModel.ReturnDate,
                Status = Status.NotPaid,
                ApplicationUserId = user.Id
            };

            orderRepository.Add(order);

            user.Orders.Add(order);
            //await TheUserManager.UpdateAsync(user);

            orderRepository.SaveChanges();

            return RedirectToAction("MyOrders");
        }

        [Authorize]
        [HttpGet]
        public ActionResult Payment (int id)
        {
            var order = orderRepository.Get(id);
            if (order == null)
            {
                throw new Exception("order doesnt exist");
            }

            CarOrderViewModel carOrderViewModel = new CarOrderViewModel
            {
                OrderId = order.OrderId,
                CarId = order.CarId,
                Name = order.Car.Name,
                Price = order.Car.Price,
                Brand = order.Car.Brand.Brand,
                Type = order.Car.Type.Type,
                ReturnDate = order.ReturnDate,
                PassportId = order.PassportId,
                Driver = order.Driver
            }; 
            return View(carOrderViewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Payment (CarOrderViewModel carOrderViewModel)
        {
            var order = orderRepository.Get(carOrderViewModel.OrderId);
            if (order == null)
            {
                throw new Exception("order doesnt exist");
            }
            order.Status = Status.Pending;
            orderRepository.SaveChanges();
            return RedirectToAction("MyOrders");
        }
    }
}