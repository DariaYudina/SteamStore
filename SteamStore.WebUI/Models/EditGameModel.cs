using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SteamStore.WebUI.Models
{
    public class EditGameModel
    {
        [Required]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 200 символов")]
        [Display(Name = "Название игры")]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public decimal Discount { get; set; }
        [Required]
        [StringLength(2000, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 2000 символов")]
        public string Description { get; set; }
        [Required]
        //[StringLength(100, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 100 символов")]
        public Category Category { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 100 символов")]
        public string Producer { get; set; }
    }
}