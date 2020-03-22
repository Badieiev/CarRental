using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.Models
{
    public class CarOrderViewModel
    {
        public int OrderId { get; set; }
        public int CarId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public string PassportId { get; set; }
        public bool Driver { get; set; }
        public DateTime ReturnDate { get; set; }
               
    }
}