using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SteamStore.WebUI.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Game> Games { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}