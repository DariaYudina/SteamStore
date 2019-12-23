using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamStore.AbstractBLL
{
    public interface IFeedBackBLL
    {
        void AddFeedback(int userId, int gameId, string text, DateTime date);
        IEnumerable<Feedback> GetFeedbacks();
        IEnumerable<Feedback> GetFeedbackByGameId(int gameId);
    }
}
