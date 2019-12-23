using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamStore.AbstractDAL
{
    public interface IGameDAL
    {
        IEnumerable<Game> GetGames();
        void AddGame(Game game);
        Category GetCategory(int id);
        IEnumerable<Category> GetCategories();
        void EditGame(int id, string name, Category category, string producer, decimal price, decimal discount, string description);
        void RemoveGame(int id);
    }
}
