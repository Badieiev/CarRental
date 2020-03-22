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

        public ActionResult Payment (CarOrderViewModel carOrderViewModel)
        {
            return null;
        }
    }
}