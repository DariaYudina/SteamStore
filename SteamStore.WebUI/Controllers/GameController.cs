﻿using Entities;
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
        public IFeedBackBLL _feedbackLogic;
        public IUserBLL _userBLL;
        public static int _gameId;
        public GameController(IGameBLL gameLogic, IFeedBackBLL feedbackLogic, IUserBLL userBLL)
        {
            _gameLogic = gameLogic;
            _feedbackLogic = feedbackLogic;
            _userBLL = userBLL;
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
            SelectList categories = new SelectList(_gameLogic.GetCategories(), "CategoryId", "CategoryName");
            ViewBag.Categories = categories;
            return View();
        }
        [HttpPost]
        public ActionResult AddGame(AddGameModel addGameModel, IList<HttpPostedFileBase> images)
        {
            SelectList categories = new SelectList(_gameLogic.GetCategories(), "CategoryId", "CategoryName");
            ViewBag.Categories = categories;
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
                _gameLogic.AddGame(addGameModel.Name, addGameModel.Price, addGameModel.Discount, gameimages[0], addGameModel.Description, addGameModel.Category, addGameModel.Producer, gameimages[1], gameimages[2]);
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
            List<Feedback> model = _feedbackLogic.GetFeedbacks().Where(x => x.GameId == id).ToList();
            foreach(var f in model)
            {
                f.User = _userBLL.GetUsers().FirstOrDefault(x => x.UserId == f.UserId);
            }
            return PartialView("_GameCommentPartial", model);
        }

        [HttpPost]
        public ActionResult _GameCommentPartial()
        {
            Stream req = Request.InputStream;
            req.Seek(0, System.IO.SeekOrigin.Begin);
            string json = new StreamReader(req).ReadToEnd();

            var jsoncomment = JsonConvert.DeserializeObject<AddCommentModel>(json);
            var datetime = DateTime.Now;
            var userId = _userBLL.GetUsers().FirstOrDefault(u => u.Login == User.Identity.Name).UserId;
            _feedbackLogic.AddFeedback(userId, jsoncomment.gameid, jsoncomment.value, datetime);
            return Json(new { author = User.Identity.Name, date = datetime.ToString(), text = jsoncomment.value });
        }

        [HttpPost]
        public ActionResult GameSearch()
        {
            Stream req = Request.InputStream;
            req.Seek(0, System.IO.SeekOrigin.Begin);
            string json = new StreamReader(req).ReadToEnd();
            var searchSegment = JsonConvert.DeserializeObject<FindGameModel>(json);

            return Json(_gameLogic.GetGames().Where(x => x.Name.ToLower().Contains(searchSegment.Value)).ToList());
        }

        [HttpPost]
        public ActionResult GamesFilter()
        {
            Stream req = Request.InputStream;
            req.Seek(0, System.IO.SeekOrigin.Begin);
            string json = new StreamReader(req).ReadToEnd();
            var filterProps = JsonConvert.DeserializeObject<FilterGameModel>(json);

            var jsonResult =  Json(_gameLogic.GetGames().Where(x => {
                if (filterProps.Categories.Length == 0 && (filterProps.PriceTo == null || filterProps.PriceFrom == null))
                {
                    return true;
                }

                if (filterProps.Categories.Length != 0 && filterProps.PriceTo != null && filterProps.PriceFrom != null)
                {
                    return filterProps.Categories.Contains(x.CategoryId) && x.Price >= filterProps.PriceFrom && x.Price <= filterProps.PriceTo;
                } else
                {
                    if (filterProps.PriceTo == null || filterProps.PriceFrom == null)
                    {
                        return filterProps.Categories.Contains(x.CategoryId);
                    } else
                    {
                        return x.Price >= filterProps.PriceFrom && x.Price <= filterProps.PriceTo;
                    }
                }

            }));

            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpGet]
        public ActionResult EditGame(int id)
        {
            _gameId = id;
            SelectList categories = new SelectList(_gameLogic.GetCategories(), "CategoryId", "CategoryName");
            ViewBag.Categories = categories;
            var game = _gameLogic.GetGame(id);
            var model = new EditGameModel()
            {
                Category = new Category() { CategoryId = game.CategoryId, CategoryName = game.Category },
                Description = game.Description,
                Name = game.Name,
                Price = game.Price,
                Producer = game.Producer
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult EditGame(EditGameModel model)
        {
            _gameLogic.EditGame(_gameId, model.Name, model.Category, model.Producer, model.Price, model.Discount, model.Description);
            return RedirectToAction("GameProfile", new { id = _gameId});
        }

        [HttpGet]
        public ActionResult RemoveGame(int id)
        {
            _gameLogic.RemoveGame(id);
            return RedirectToAction("Catalog");
        }
    }
}