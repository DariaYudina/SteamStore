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

    }
}
