namespace Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order() { }
        public Order(int userId, int gameId, DateTime date, int orderQuantity, decimal orderprice, string email)
        {
            UserId = userId;
            GameId = gameId;
            OrderDate = date;
            OrderQuantity = orderQuantity;
            OrderPrice = orderprice;
            Email = email;
        }
        public int OrderId { get; set; }
        public int? UserId { get; set; }
        public int? GameId { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? OrderQuantity { get; set; }
        public decimal? OrderPrice { get; set; }
        public virtual Game Game { get; set; }
        public virtual User User { get; set; }
        public string Email { get; set; }
    }
}
