using Entities;
using SteamStore.AbstractBLL;
using SteamStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SteamStore.WebUI.Controllers
{
    public class GameController : Controller
    {
        public IGameBLL _gameLogic;
        public GameController(IGameBLL gameLogic)
        {
            _gameLogic = gameLogic;
        }
        [HttpGet]
        public ActionResult Catalog(int page = 1)
        {
            var games = _gameLogic.GetGames().ToList();
            int pageSize = 5; // количество объектов на страницу
            IEnumerable<Game> gamesPerPages = games.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = games.Count };
            IndexViewModel ivm = new IndexViewModel { PageInfo = pageInfo, Games = gamesPerPages };
            return View(ivm);
        }
        [HttpGet]
        public ActionResult AddGame()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddGame(AddGameModel addGameModel, HttpPostedFileBase image)
        {
            var gameimage = "";
            byte[] imageData = null;
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    using (var binaryReader = new BinaryReader(image.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(image.ContentLength);
                    }
                    gameimage = Convert.ToBase64String(imageData);
                }
                else
                {
                    gameimage = AddGameModel.defaultImage;
                }
                _gameLogic.AddGame(addGameModel.Name, addGameModel.Price, gameimage, addGameModel.Description, addGameModel.Category, addGameModel.Producer);
                return RedirectToAction("Catalog");
            }
            return View(addGameModel);
        }
        [HttpGet]
        public ActionResult GameProfile(int id)
        {
            if(_gameLogic.GetGame(id) == null)
            {
                return HttpNotFound();
            }
            return View(_gameLogic.GetGame(id));
        }
        [HttpGet]
        public ActionResult BuyGame(int id)
        {
            return View(_gameLogic.GetGame(id));
        }

    }
}