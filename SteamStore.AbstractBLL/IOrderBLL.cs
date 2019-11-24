using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamStore.AbstractBLL
{
    public interface IOrderBLL
    {
        void AddOrder(int userId, int gameId, DateTime date, int orderQuantity, decimal orderprice, string email);
        IEnumerable<Order> GetOrders();
    }
}
