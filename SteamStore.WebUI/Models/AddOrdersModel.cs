using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SteamStore.WebUI.Models
{
    public class AddOrdersModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        [Required]
        public int OrderQuantity { get; set; }
        public decimal OrderPrice { get; set; }
    }
}