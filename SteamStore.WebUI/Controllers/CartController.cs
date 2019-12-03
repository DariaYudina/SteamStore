using Entities;
using Newtonsoft.Json;
using SteamStore.AbstractBLL;
using SteamStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SteamStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        public IGameBLL _gameLogic;
        private List<CartItemModel> CartItems = new List<CartItemModel>();
        public CartController(IGameBLL gameLogic)
        {
            _gameLogic = gameLogic;
        }
        [HttpGet]
        public ActionResult MyCart()
        {
           
            if (HttpContext.Request.Cookies.AllKeys.Any(key => key.Equals("CartProducts")))
            {
               
                var cartItems = JsonConvert.DeserializeObject<CookieItemModel[]>(HttpUtility.UrlDecode(HttpContext.Request.Cookies["CartProducts"].Value));
                foreach (var item in cartItems)
                {
                    CartItems.Add(new CartItemModel(_gameLogic.GetGame(item.id), item.count));
                }

                return View(CartItems);
            }
            else
            {
                return View(CartItems);
            }
        }
        
    }
}