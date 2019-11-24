using SteamStore.AbstractBLL;
using SteamStore.WebUI.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace SteamStore.WebUI.Controllers
{
    public class OrderController : Controller
    {
        private static int _gameId;
        public IGameBLL _gameLogic;
        public IOrderBLL _orderLogic;
        public OrderController(IGameBLL gameLogic, IOrderBLL orderLogic)
        {
            _gameLogic = gameLogic;
            _orderLogic = orderLogic;
        }
        [HttpGet]
        public ActionResult OrderForm(int id)
        {
            _gameId = id;
            return View(_gameLogic.GetGame(id));
        }
        [HttpPost]
        public ActionResult OrderFormPartial(AddOrderModel addorderModel)
        {
            var game = _gameLogic.GetGame(_gameId);
            if (ModelState.IsValid)
            {
                MailAddress from = new MailAddress("dashaudina06@gmail.com", "Order");
                MailAddress to = new MailAddress(addorderModel.Email);
                MailMessage msg = new MailMessage(from, to);
                msg.Subject = "Steam Store: Покупка игры";
                msg.Body = $"Вы приобрели игру {game.Name}.\n " +
                           $"Цена игры: {game.Price} руб.\n" +
                           $"Ключ активации: {Guid.NewGuid().ToString()}\n" +                        
                           $"Теперь вы можете активировать игру в Steam.";
                msg.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("dashaudina06@gmail.com", "dD89271518969");
                smtp.EnableSsl = true;
                smtp.Send(msg);
                
                if (!User.Identity.IsAuthenticated)
                {
                    _orderLogic.AddOrder(16, _gameId, DateTime.Now, addorderModel.OrderQuantity, game.Price * addorderModel.OrderQuantity, addorderModel.Email);
                }
                return RedirectToAction("OrderCompleted", "Order");
            }
            return View(addorderModel);
        }
        [HttpGet]
        public ActionResult OrderFormPartial(int id)
        {
            ViewBag.id = id;
            return PartialView("OrderFormPartial");
        }
        [HttpGet]
        public ActionResult OrderCompleted()
        {
            return View();
        }
    }
}