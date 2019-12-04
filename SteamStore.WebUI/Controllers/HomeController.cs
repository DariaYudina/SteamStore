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
        IGameBLL _gameLogic;
        public HomeController(IGameBLL gameLogic)
        {
            _gameLogic = gameLogic;
        }
        [HttpGet]
        public ActionResult Index()
        {
            return View(_gameLogic.GetGames().Take(5));
        }
       
        public ActionResult Test()
        {
            return PartialView("_GameCommentPartial", null);
        }
        public ActionResult GameSearch(string name)
        {
            var allgames = _gameLogic.GetGames().Where(g => g.Contains(name)).ToList();
            return PartialView(allgames);
        }
    }
}