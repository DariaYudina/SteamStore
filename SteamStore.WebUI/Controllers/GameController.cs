using Entities;
using Newtonsoft.Json;
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
        public ActionResult AddGame(AddGameModel addGameModel, IList<HttpPostedFileBase> images)
        {
            var imageslist = images.ToList();
            string[] gameimages = new string[3];
            byte[] imageData = null;
            if (ModelState.IsValid)
            {
                for (int i = 0; i < gameimages.Length; i++)
                {
                    if (imageslist[i] != null)
                    {
                        using (var binaryReader = new BinaryReader(imageslist[i].InputStream))
                        {
                            imageData = binaryReader.ReadBytes(imageslist[i].ContentLength);
                        }
                        gameimages[i] = Convert.ToBase64String(imageData);
                    }
                    else
                    {

                        gameimages[i] = AddGameModel.defaultImage;
                        if(i == 1)
                        {
                            gameimages[i] = AddGameModel.defailtprofileimage;
                        }
                    }
                }
                _gameLogic.AddGame(addGameModel.Name, addGameModel.Price, gameimages[0], addGameModel.Description, addGameModel.Category, addGameModel.Producer, gameimages[1], gameimages[2]);
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
        public ActionResult _GameCommentPartial(int id)
        {
            ViewBag.id = id;
            return PartialView("_GameCommentPartial");
        }

        [HttpPost]
        public ActionResult _GameCommentPartial()
        {
            Stream req = Request.InputStream;
            req.Seek(0, System.IO.SeekOrigin.Begin);
            string json = new StreamReader(req).ReadToEnd();

            // TODO: добавление комментария в базу (надо ещё одну модель сделать и уже её отсылать обратно),
            // сделать нормальную дату, а то сейчас так /Date(1575464649011)/

            var value = JsonConvert.DeserializeObject<AddCommentModel>(json);
            var gameId = JsonConvert.DeserializeObject<AddCommentModel>(json);
            return Json(new { author = User.Identity.Name, date = DateTime.Now.ToString(), text = value.value });
        }
    }
}