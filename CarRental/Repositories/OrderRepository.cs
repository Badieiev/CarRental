using CarRental.Models;
using IdentityTest2.Migrations;

namespace CarRental.Repositories
{
    internal class OrderRepository : Repository<Order>
    {
        public OrderRepository() : base(new ApplicationDbContext())
        {
        }
    }
}