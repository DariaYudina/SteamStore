using Entities;
using SteamStore.AbstractBLL;
using SteamStore.AbstractDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamStore.BLL
{
    public class OrderLogic : IOrderBLL
    {
        IOrderDAL _orderdal;
        public OrderLogic(IOrderDAL orderdal)
        {
            _orderdal = orderdal;
        }
        public void AddOrder(int userId, int gameId, DateTime date, int orderQuantity, decimal orderprice, string email)
        {
            Order order = new Order(userId, gameId, date, orderQuantity, orderprice, email);
            _orderdal.AddOrder(order);
        }

        public IEnumerable<Order> GetOrders()
        {
            return _orderdal.GetOrders();
        }
    }
}
