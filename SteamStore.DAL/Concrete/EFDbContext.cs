using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamStore.DAL.Concrete
{
    public class EFDbContext : DbContext
    {
        public EFDbContext() : base("EFDbContext")
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
