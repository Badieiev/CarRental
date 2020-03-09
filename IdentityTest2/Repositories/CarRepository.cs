using IdentityTest2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityTest2.Repositories
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
        public IList<Car> FindCars(int startIndex, int page)
        {
            return DbSet
                .OrderBy(car => car.Name)
                .Skip(startIndex)
                .Take(page)
                .ToList();
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