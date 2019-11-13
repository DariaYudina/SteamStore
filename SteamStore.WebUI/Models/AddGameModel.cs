﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SteamStore.WebUI.Models
{
    public class AddGameModel
    {
        public static string defaultImage = "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";
        [Required]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 200 символов")]
        [Display(Name = "Название игры")]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        [StringLength(2000,MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 2000 символов")]
        public string Description { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 100 символов")]
        public string Category { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 100 символов")]
        public string Producer { get; set; }
    }
}