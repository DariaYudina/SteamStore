using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SteamStore.WebUI.Models
{
    public class CartItemModel
    {
        public CartItemModel(Game game, int gamequantity)
        {
            Game = game;
            Gamequantity = gamequantity;
        }
        public Game Game { get; set; }
        public int Gamequantity { get; set; }
    }
}