namespace Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Feedback
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FeedbackId { get; set; }

        public int? UserId { get; set; }
        public int? GameId { get; set; }
        public string Text { get; set; }
        public virtual Game Game { get; set; }
        public virtual User User { get; set; }
    }
}
