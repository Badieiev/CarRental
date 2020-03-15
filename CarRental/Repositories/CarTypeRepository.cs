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
        
        public CarType FindTypeByName(string type)
        {
            var types = DbSet.Where(b => b.Type == type);
            if (types.Count() == 0)
            {
                return null;
            }

            return types.First();
        }
    }
}