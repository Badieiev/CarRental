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
    }
}