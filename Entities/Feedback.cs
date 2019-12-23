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
        [Key]
        public int FeedbackId { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }
        [ForeignKey("Game")]
        public int? GameId { get; set; }
        public string Text { get; set; }
        public Game Game { get; set; }
        public User User { get; set; }
        public DateTime DateComment { get; set; }
    }
}
