using SteamStore.AbstractBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SteamStore.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IUserBLL _userLogic;
        IGameBLL _gameLogic;
        public HomeController(IGameBLL gameLogic)
        {
            _gameLogic = gameLogic;
        }
        [HttpGet]
        public ActionResult Index()
        {
            return View(_gameLogic.GetGames());
        }

    }
}