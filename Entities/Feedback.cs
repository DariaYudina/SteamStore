namespace Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Feedback
    {
        public Feedback() { }
        public Feedback(int userId, int gameId, string text, DateTime date) : this()
        {
            UserId = userId;
            GameId = gameId;
            Text = text;
            DateComment = date;
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FeedbackId { get; set; }

        public int? UserId { get; set; }
        public int? GameId { get; set; }
        public string Text { get; set; }
        public virtual Game Game { get; set; }
        public virtual User User { get; set; }
        public DateTime DateComment { get; set; }
    }
}
