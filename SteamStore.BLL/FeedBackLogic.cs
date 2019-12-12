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
    public class FeedBackLogic : IFeedBackBLL
    {
        IFeedBackDAL _feedBackDao;
        public FeedBackLogic(IFeedBackDAL feedBackDao)
        {
            _feedBackDao = feedBackDao;
        }
        public void AddFeedback(int userId, int gameId, string text, DateTime date)
        {
            Feedback feedback = new Feedback(userId, gameId, text, date);
            _feedBackDao.AddFeedback(feedback);
        }

        public IEnumerable<Feedback> GetFeedbacks()
        {
            return _feedBackDao.GetFeedBacks();
        }
    }
}
