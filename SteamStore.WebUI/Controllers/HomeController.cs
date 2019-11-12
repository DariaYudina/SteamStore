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
        public HomeController(IUserBLL userLogic)
        {
            _userLogic = userLogic;
        }
        [HttpGet]
        public ActionResult Index()
        {
            return View(_userLogic.GetUsers());
        }

    }
}