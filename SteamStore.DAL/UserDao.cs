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
    public class UserDao : IUserDAL
    {
        public void AddUser(User user)
        {
            using (var db = new EFDbContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        public void ConfirmEmail(string userName)
        {
            using (var db = new EFDbContext())
            {
                var user = db.Users.Where(u => u.Login == userName).FirstOrDefault();
                user.ConfirmedEmail = true;
                db.SaveChanges();
            }
        }
        public IEnumerable<User> GetUsers()
        {
            using (var db = new EFDbContext())
            {
                return  db.Users.ToList();
            }           
        }
    }
}
