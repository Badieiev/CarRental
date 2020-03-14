using CarRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.Repositories
{
    public class CarRepository : Repository<Car>
    {
        public CarRepository() : base(new ApplicationDbContext())
        {

        }

        public int TotalCarsNumber()
        {
            return DbSet.Count();
        }

        public int FilteredCarsNumber(string brand, string type)
        {
            IQueryable<Car> cars = DbSet;

            if (brand != "All")
            {
                cars = cars.Where(car => car.Brand.Brand == brand);
            }

            if (type != "All")
            {
                cars = cars.Where(car => car.Type.Type == type);
            }

            return cars.Count();
        }

        public IList<Car> FindCars(int startIndex, int page, string brand, string type, string sortType)
        {
            IQueryable<Car> cars = DbSet;
            
            if (brand != "All")
            {
                cars = cars.Where(car => car.Brand.Brand == brand);
            }

            if (type != "All")
            {
                cars = cars.Where(car => car.Type.Type == type);
            }

            if (sortType == "Price")
            {
                cars = cars.OrderBy(car => car.Price);
            }
            else 
            {
                cars = cars.OrderBy(car => car.Name);
            }

            return cars.Skip(startIndex).Take(page).ToList();
        }

        public IList<Car> FindCarsByBrand(string brand, int startIndex, int page)
        {
            return DbSet.Where(car => car.Brand.Brand == brand)
                .Skip(startIndex)
                .Take(page)
                .ToList();
        }

        public IList<Car> FindCarsByType(string type)
        {
            return DbSet.Where(car => car.Type.Type == type).ToList();
        }
    }
}