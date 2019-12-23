using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SteamStore.WebUI.Models
{
    public class FilterGameModel
    {
        public int[] Categories { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
    }
}