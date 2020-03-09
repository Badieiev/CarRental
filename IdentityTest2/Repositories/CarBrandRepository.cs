using IdentityTest2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityTest2.Repositories
{
    public class CarBrandRepository : Repository<CarBrand>
    {
        public CarBrandRepository() : base(new ApplicationDbContext())
        { 
        }
    }
}