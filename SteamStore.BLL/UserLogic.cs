using Entities;
using SteamStore.AbstractBLL;
using SteamStore.AbstractDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SteamStore.BLL
{
    public class UserLogic : IUserBLL
    {
        IUserDAL _userDao;
        public UserLogic(IUserDAL userDao)
        {
            _userDao = userDao;
        }

        public bool AddUser(string login, string password, string email, string avatar)
        {
            if (!_userDao.GetUsers().Any(x => x.Login == login))
            {
                User user = new User(login, ComputeHash(password, new MD5CryptoServiceProvider()), email, avatar);
                _userDao.AddUser(user);
                return true;
            }
            else return false;
        }

        public void ConfirmEmail(string userName)
        {
            _userDao.ConfirmEmail(userName);
        }

        public IEnumerable<User> GetUsers()
        {
            return _userDao.GetUsers();
        }
        private string ComputeHash(string input, HashAlgorithm algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }

        bool IUserBLL.VerifyUser(string login, string password)
        {
            var user = GetUsers().FirstOrDefault(x => x.Login == login);
            if (user != null && user.Password == ComputeHash(password, new MD5CryptoServiceProvider()) && user.ConfirmedEmail)
            {
                return true;           
            }
            return false;
        }
    }
}
