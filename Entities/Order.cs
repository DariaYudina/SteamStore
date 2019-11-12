namespace Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderId { get; set; }

        public int? UserId { get; set; }

        public int? GameId { get; set; }

        public DateTime? OrderDate { get; set; }

        public int? OrderQuantity { get; set; }

        public decimal? OrderPrice { get; set; }

        public virtual Game Game { get; set; }

        public virtual User User { get; set; }
    }
}
