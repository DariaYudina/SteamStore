﻿using Entities;
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

        public void AddGame(string name, decimal price, string image, string description, string category, string producer, string profileImage, string backgroundImage)
        {
            Game game = new Game(name, price, image, description, category, producer, profileImage, backgroundImage);
            _gameDao.AddGame(game);
        }

        public IEnumerable<Game> GetGames()
        {
            var games = _gameDao.GetGames();
            foreach(var game in games)
            {
                game.Category = _gameDao.GetCategory(game.CategoryId).CategoryName;
            }
           return games;
        }
        public Game GetGame(int id)
        {
            var game = _gameDao.GetGames().FirstOrDefault(g => g.GameId == id);
            game.Category = _gameDao.GetCategory(game.CategoryId).CategoryName;
            return game;
        }

        public IEnumerable<Game> GetGames(int[] ids)
        {
            List<Game> games = _gameDao.GetGames().ToList();
            foreach (var game in games)
            {
                game.Category = _gameDao.GetCategory(game.CategoryId).CategoryName;
            }
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
