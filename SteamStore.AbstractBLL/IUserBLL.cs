using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamStore.AbstractBLL
{
    public interface IUserBLL
    {
        bool AddUser(string login, string password, string email, string avatar);
        IEnumerable<User> GetUsers();
        bool VerifyUser(string login, string password);
        void ConfirmEmail(string userName);
    }
}
