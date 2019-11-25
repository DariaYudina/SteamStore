using SteamStore.AbstractBLL;
using SteamStore.WebUI.Models;
using System;
using System.Linq;
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
        public IUserBLL _userLogic;
        public OrderController(IGameBLL gameLogic, IOrderBLL orderLogic, IUserBLL userLogic)
        {
            _gameLogic = gameLogic;
            _orderLogic = orderLogic;
            _userLogic = userLogic;
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
                addorderModel.OrderPrice = game.Price * addorderModel.OrderQuantity;
                MailAddress from = new MailAddress("dashaudina06@gmail.com", "Order");
                MailAddress to = new MailAddress(addorderModel.Email);
                MailMessage msg = new MailMessage(from, to);
                string test = "";
                for (int i = 0; i < addorderModel.OrderQuantity; i++)
                {
                    test += @"<tr style =""padding: 5px;"">
                                <td>" + $"{game.Name}" + @"</td>
                                <td>" + $"{game.Price}" + @" руб.</td>
                                <td>" + $"{Guid.NewGuid().ToString()}" + @"</td>
                            </tr>";
                }
                msg.Subject = "Steam Store: Покупка игры";
                msg.Body = @"<table style=""font-family:Play,Arial,sans-serif;font-weight:600;font-size: 18px;color: dimgrey;"">
                                <tr style =""padding: 5px;"">
                                    <td colspan=""2"" style = ""text-align:center;color: tomato"">STEAM GAMES</td>
                                </tr>
                                <tr style =""padding: 5px;"">
                                    <td colspan=""2"">Вы приобрели игру:" + @"</td>
                                </tr>"+
                                $"{ test }"+
                                @"<tr style =""padding: 5px;"">
                                    <td> Количество: </td>
                                    <td>" + $"{addorderModel.OrderQuantity}" + @"</td>
                                </tr>
                                <tr style =""padding: 5px;""> 
                                    <td colspan=""2"">Общая стоимость: " + $"{addorderModel.OrderPrice}" + @" руб.</td>
                                </tr>
                            </table>";
                msg.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("dashaudina06@gmail.com", "dD89271518969");
                smtp.EnableSsl = true;
                smtp.Send(msg);
                
                if (!User.Identity.IsAuthenticated)
                {
                    _orderLogic.AddOrder(16, _gameId, DateTime.Now, addorderModel.OrderQuantity, addorderModel.OrderPrice, addorderModel.Email);
                }
                else
                {
                    var user = _userLogic.GetUsers().FirstOrDefault(u => u.Login == User.Identity.Name);
                    _orderLogic.AddOrder(user.UserId, _gameId, DateTime.Now, addorderModel.OrderQuantity, addorderModel.OrderPrice, addorderModel.Email);
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