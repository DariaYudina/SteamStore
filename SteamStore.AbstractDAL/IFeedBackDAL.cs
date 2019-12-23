using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamStore.AbstractDAL
{
    public interface IFeedBackDAL
    {
        IEnumerable<Feedback> GetFeedBacks();
        void AddFeedback(Feedback feedBack);
        IEnumerable<Feedback> GetFeedbackByGameId(int gameId);
    }
}
