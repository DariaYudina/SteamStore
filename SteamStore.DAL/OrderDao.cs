using Entities;
using SteamStore.AbstractDAL;
using SteamStore.DAL.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamStore.DAL
{
    public class OrderDao : IOrderDAL
    {
        public void AddOrder(Order order)
        {
            using (var db = new EFDbContext())
            {
                db.Orders.Add(order);
                db.SaveChanges();
            }
        }
        public IEnumerable<Order> GetOrders()
        {
            using (var db = new EFDbContext())
            {
                return db.Orders.Include("Game").Include("User").ToList();
            }
        }
    }
}
