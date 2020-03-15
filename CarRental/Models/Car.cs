using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.Models
{
    public class Car
    {
        public int CarId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        public int BrandId { get; set; }
        public virtual CarBrand Brand { get; set; }
        public int TypeId { get; set; }
        public virtual CarType Type { get; set; }

    }
}