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
    }
}
