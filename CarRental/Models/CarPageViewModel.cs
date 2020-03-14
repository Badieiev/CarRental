using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRental.Models
{
    public class CarPageViewModel
    {
        public IEnumerable<Car> Cars { get; set; }
        public PageInfo PageInfo { get; set; }

        public SelectList Brands { get; set; }
        public SelectList CarTypes { get; set; }

        public SelectList SortType { get; set; }
    }
}