using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRental.Models
{
    public class CarType
    {
        [Key]
        public int TypeId { get; set; }
        public string Type { get; set; }
    }
}