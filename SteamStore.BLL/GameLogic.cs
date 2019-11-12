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
    public class GameLogic : IGameBLL
    {
        IGameDAL _gameDao;
        public GameLogic(IGameDAL gameDao)
        {
            _gameDao = gameDao;
        }

        public void AddGame(string name, decimal price, string image, string description, string category, string producer)
        {
            Game game = new Game(name, price, image, description, category, producer);
            _gameDao.AddGame(game);
        }

        public IEnumerable<Game> GetGames()
        {
           return _gameDao.GetGames();
        }
        public Game GetGame(int id)
        {
            return _gameDao.GetGames().FirstOrDefault(g => g.GameId == id);
        }

        public IEnumerable<Game> GetGames(int[] ids)
        {
            List<Game> games = _gameDao.GetGames().ToList();
            List<Game> foundGames = new List<Game>();
            foreach(int id in ids)
            {
                var game = games.Find(g => g.GameId == id);
                foundGames.Add(game);
            }
            return foundGames;
        }
    }
}
