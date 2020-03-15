using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.Models
{
    public enum Status
    {
        Pending,
        Accepted,
        Declined,
        Returned,
        Damaged
    }
    public class Order
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public int CarId { get; set; }
        public string PassportId { get; set; }

        public DateTime ReturnDate { get; set; }

        public Status Status { get; set; }

        public bool Driver { get; set; }

        public string DamageDescription { get; set; }

        public int DamageCost { get; set; }

    }
}