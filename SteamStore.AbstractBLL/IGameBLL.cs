using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamStore.AbstractBLL
{
    public interface IGameBLL
    {
        IEnumerable<Game> GetGames();
        void AddGame(string name, decimal price, decimal discount, string image, string description, Category category, string producer, string profileImage, string backgroundImage);
        Game GetGame(int id);
        IEnumerable<Game> GetGames(params int[] ids);
        IEnumerable<Category> GetCategories();
        void EditGame(int id, string name, Category category, string producer, decimal price, decimal dicsount, string description);
        void RemoveGame(int id);
    }
}
