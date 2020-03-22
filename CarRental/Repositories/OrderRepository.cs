using CarRental.Models;

namespace CarRental.Repositories
{
    internal class OrderRepository : Repository<Order>
    {
        public OrderRepository() : base(new ApplicationDbContext())
        {
        }
    }
}