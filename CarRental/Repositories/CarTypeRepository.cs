using CarRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.Repositories
{
    public class CarTypeRepository : Repository<CarType>
    {
        public CarTypeRepository() : base(new ApplicationDbContext())
        {
        }
    }
}