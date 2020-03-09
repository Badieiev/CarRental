using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IdentityTest2.Models
{
    public class CarBrand
    {
        [Key]
        public int BrandID { get; set; }
        public string Brand { get; set; }
    }
}