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
    public class FeedBackDao : IFeedBackDAL
    {
        public void AddFeedback(Feedback feedBack)
        {
            using (var db = new EFDbContext())
            {
                db.Feedbacks.Add(feedBack);
                db.SaveChanges();
            }
        }

        public IEnumerable<Feedback> GetFeedBacks()
        {
            var feedBacks = new List<Feedback>();
            using (var db = new EFDbContext())
            {
                feedBacks = db.Feedbacks.ToList();
            }
            return feedBacks;
        }

        public IEnumerable<Feedback> GetFeedbackByGameId(int gameId)
        {
            using (var db = new EFDbContext())
            {
                return db.Feedbacks.Where(x => x.GameId == gameId).ToList();
            }
        }
    }
}
