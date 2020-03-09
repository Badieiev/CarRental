using IdentityTest2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityTest2.Repositories
{
    public class CarTypeRepository : Repository<CarType>
    {
        public CarTypeRepository() : base(new ApplicationDbContext())
        {
        }
    }
}