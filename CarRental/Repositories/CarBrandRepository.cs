using CarRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.Repositories
{
    public class CarBrandRepository : Repository<CarBrand>
    {
        public CarBrandRepository() : base(new ApplicationDbContext())
        { 
        }

        public CarBrand FindBrandByName(string brand)
        {
            var brands = DbSet.Where(b => b.Brand == brand);
            if (brands.Count() == 0)
            {
                return null;
            }

            return brands.First();
        }
    }
}