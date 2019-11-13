using SteamStore.AbstractBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SteamStore.WebUI.Controllers
{
    public class OrderController : Controller
    {

        public IGameBLL _gameLogic;
        public OrderController(IGameBLL gameLogic)
        {
            _gameLogic = gameLogic;
        }
        [HttpGet]
        public ActionResult OrderForm(int id)
        {
            return View(_gameLogic.GetGame(id));
        }
    }
}