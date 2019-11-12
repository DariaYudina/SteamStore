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
        void AddGame(string name, decimal price, string image, string description, string category, string producer);
        Game GetGame(int id);
        IEnumerable<Game> GetGames(params int[] ids);
    }
}
