using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SteamStore.WebUI.Models
{
    public class StatisticModel
    {
        public decimal AllIncome { get; set; }
        public int AllCount { get; set; }
        public decimal Monthlyincome { get; set; }
        public int MonthlyCount { get; set; }
        //public Game MostPopularGame { get; set; }
        //public string Genre { get; set; }
    }
}