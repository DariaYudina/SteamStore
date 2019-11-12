using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamStore.AbstractDAL
{
    public interface IUserDAL
    {
        void AddUser(User user);
        IEnumerable<User> GetUsers();
        void ConfirmEmail(string userName);
    }
}
