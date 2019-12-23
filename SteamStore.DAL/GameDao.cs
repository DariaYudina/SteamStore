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
    public class GameDao : IGameDAL
    {
        public void AddGame(Game game)
        {
            using (var db = new EFDbContext())
            {
                db.Games.Add(game);
                db.SaveChanges();
            }
        }
        public IEnumerable<Game> GetGames()
        {
            using (var db = new EFDbContext())
            {
                return db.Games.ToList();
            }
        }

        public Category GetCategory(int id)
        {
            using (var db = new EFDbContext())
            {
                return db.Categories.FirstOrDefault(x => x.CategoryId == id);
            }
        }

        public IEnumerable<Category> GetCategories()
        {
            using (var db = new EFDbContext())
            {
                return db.Categories.ToList();
            }
        }

        public void EditGame(int id, string name, Category category, string producer, decimal price, decimal discount, string description)
        {
            using (var db = new EFDbContext())
            {
                var game = db.Games.Find(id);
                game.Name = name;
                game.Category = category.CategoryName;
                game.CategoryId = category.CategoryId;
                game.Producer = producer;
                game.Description = description;
                game.Discount = discount;
                db.SaveChanges();
            }
        }

        public void RemoveGame(int id)
        {
            using (var db = new EFDbContext())
            {
                var game = db.Games.Find(id);
                db.Games.Remove(game);
                db.SaveChanges();
            }
        }
    }
}
