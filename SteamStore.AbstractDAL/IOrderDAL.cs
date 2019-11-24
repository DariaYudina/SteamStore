using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamStore.AbstractDAL
{
    public interface IOrderDAL
    {
        void AddOrder(Order order);
        IEnumerable<Order> GetOrders();
    }
}
