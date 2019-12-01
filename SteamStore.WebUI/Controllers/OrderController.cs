using Newtonsoft.Json;
using SteamStore.AbstractBLL;
using SteamStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace SteamStore.WebUI.Controllers
{
    public class OrderController : Controller
    {
        private static int _gameId;
        public IGameBLL _gameLogic;
        public IOrderBLL _orderLogic;
        public IUserBLL _userLogic;
        private List<CartItemModel> CartItems = new List<CartItemModel>();
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
        [HttpGet]
        public ActionResult OrdersForm()
        {
            if (HttpContext.Request.Cookies.Count != 0)
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
        [HttpPost]
        public ActionResult OrderFormPartial(AddOrderModel addorderModel)
        {
            var game = _gameLogic.GetGame(_gameId);
            if (ModelState.IsValid)
            {
                addorderModel.OrderPrice = game.Price * addorderModel.OrderQuantity;

                MailAddress from = new MailAddress("dashaudina06@gmail.com", "STEAM GAMES");
                MailAddress to = new MailAddress(addorderModel.Email);
                MailMessage msg = new MailMessage(from, to);
                string itemscart = "";
                for (int i = 0; i < addorderModel.OrderQuantity; i++)
                {
                    itemscart += @"<tr>
                                <td style =""padding: 10px; font-family: Play,Arial,sans-serif;"" >" + $"{game.Name}" + @"</td>
                                <td style =""padding: 10px; font-family: Play,Arial,sans-serif;"" >" + $"{game.Price.ToString("#.##")}" + @" руб.</td>
                                <td style =""padding: 10px; font-family: Play,Arial,sans-serif; color: #0094e9;"" >" + $"{Guid.NewGuid().ToString()}" + @"</td>
                            </tr>";
                }
                msg.Subject = "STEAM GAMES: Список покупок";
                msg.Body = @"<table style=""font-family:Play,Arial,sans-serif;font-weight:500;font-size: 18px; color:dimgrey; padding: 30px; width: 100%; padding: 30px;margin: 5px; border: 50px solid silver;"">
                                <tr>
                                    <td style =""padding: 10px; text-align:center;color: tomato; font-family: Play,Arial,sans-serif;"" colspan=""3"">STEAM GAMES</td>
                                </tr>
                                <tr >
                                    <td style =""padding: 10px;"" colspan=""3"">Вы приобрели товар(ы):" + @"</td>
                                </tr>"+
                                @"<tr>
                                <td style =""font-family: Play,Arial,sans-serif; padding: 10px;"" > Название </td>
                                <td style =""font-family: Play,Arial,sans-serif; padding: 10px;"" > Цена </td>
                                <td style =""font-family: Play,Arial,sans-serif; padding: 10px;"" > Ключ активации </td>
                                 </tr>" +
                                $"{ itemscart }"+
                                @"<tr >
                                    <td style =""font-family: Play,Arial,sans-serif; padding: 10px;""> Количество игр:  " + $" {addorderModel.OrderQuantity}" + @"</td>
                                    <td style =""font-family: Play,Arial,sans-serif; padding: 10px;""></td>
                                </tr>
                                <tr> 
                                    <td style =""font-family: Play,Arial,sans-serif; padding: 10px;"" colspan=""2"">Общая стоимость:  " + $" {addorderModel.OrderPrice.ToString("#.##")}" + @" руб.</td>
                                </tr>
                                <tr> 
                                    <td style =""font-family: Play,Arial,sans-serif; padding: 10px;"" colspan=""2""> Теперь вы можете активировать игру(ы) в Steam</td>
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
        [HttpPost]
        public ActionResult OrdersFormPartial(AddOrdersModel addordersModel)
        {
            if (ModelState.IsValid)
            {
                if(HttpContext.Request.Cookies.Count > 0)
                {
                    var cartItems = JsonConvert.DeserializeObject<CookieItemModel[]>(HttpUtility.UrlDecode(HttpContext.Request.Cookies["CartProducts"].Value));
                    addordersModel.OrderPrice = 0;
                    addordersModel.OrderQuantity = 0;
                    foreach (var item in cartItems)
                    {
                        CartItems.Add(new CartItemModel(_gameLogic.GetGame(item.id), item.count));
                    }
                    string itemscart = "";
                    for (int i = 0; i < CartItems.Count; i++)
                    {
                        for (int j = 0; j < CartItems[i].Gamequantity; j++)
                        {
                            addordersModel.OrderPrice += CartItems[i].Game.Price;
                            addordersModel.OrderQuantity++;
                            itemscart += @"<tr>
                                <td style =""padding: 10px; font-family: Play,Arial,sans-serif;"" >" + $"{CartItems[i].Game.Name}" + @"</td>
                                <td style =""padding: 10px; font-family: Play,Arial,sans-serif;"" >" + $"{CartItems[i].Game.Price.ToString("#.##")}" + @" руб.</td>
                                <td style =""padding: 10px; font-family: Play,Arial,sans-serif; color: #0094e9;"" >" + $"{Guid.NewGuid().ToString()}" + @"</td>
                            </tr>";
                        }
                    }
                    MailAddress from = new MailAddress("dashaudina06@gmail.com", "STEAM GAMES");
                    MailAddress to = new MailAddress(addordersModel.Email);
                    MailMessage msg = new MailMessage(from, to);
                    msg.Subject = "STEAM GAMES: Список покупок";
                    msg.Body = @"<table style=""font-family:Play,Arial,sans-serif;font-weight:500;font-size: 18px; color:dimgrey; padding: 30px; width: 100%; padding: 30px;margin: 5px; border: 50px solid silver;"">
                                <tr>
                                    <td style =""padding: 10px; text-align:center;color: tomato; font-family: Play,Arial,sans-serif;"" colspan=""3"">STEAM GAMES</td>
                                </tr>
                                <tr >
                                    <td style =""padding: 10px;"" colspan=""3"">Вы приобрели товар(ы):" + @"</td>
                                </tr>" +
                                    @"<tr>
                                <td style =""font-family: Play,Arial,sans-serif; padding: 10px;"" > Название </td>
                                <td style =""font-family: Play,Arial,sans-serif; padding: 10px;"" > Цена </td>
                                <td style =""font-family: Play,Arial,sans-serif; padding: 10px;"" > Ключ активации </td>
                                 </tr>" +
                                    $"{ itemscart }" +
                                    @"<tr >
                                    <td style =""font-family: Play,Arial,sans-serif; padding: 10px;"">Количество игр:  " + $" {addordersModel.OrderQuantity}" + @"</td>
                                    <td style =""font-family: Play,Arial,sans-serif; padding: 10px;""></td>
                                </tr>
                                <tr> 
                                    <td style =""font-family: Play,Arial,sans-serif; padding: 10px;"" colspan=""2"">Общая стоимость:  " + $" {addordersModel.OrderPrice.ToString("#.##")}" + @" руб.</td>
                                </tr>
                                <tr> 
                                    <td style =""font-family: Play,Arial,sans-serif; padding: 10px;"" colspan=""2""> Теперь вы можете активировать игру(ы) в Steam</td>
                                </tr>
                            </table>";
                    msg.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("dashaudina06@gmail.com", "dD89271518969");
                    smtp.EnableSsl = true;
                    smtp.Send(msg);
                    decimal orderamount = 0;
                    for (int i = 0; i < CartItems.Count; i++)
                    {
                        orderamount = CartItems[i].Gamequantity * CartItems[i].Game.Price;
                        if (!User.Identity.IsAuthenticated)
                        {
                            _orderLogic.AddOrder(16, CartItems[i].Game.GameId, DateTime.Now, CartItems[i].Gamequantity, orderamount, addordersModel.Email);
                        }
                        else
                        {
                            var user = _userLogic.GetUsers().FirstOrDefault(u => u.Login == User.Identity.Name);
                            _orderLogic.AddOrder(user.UserId, CartItems[i].Game.GameId, DateTime.Now, addordersModel.OrderQuantity, orderamount, addordersModel.Email);
                        }
                    }
                    return RedirectToAction("OrderCompleted", "Order");
                }
            }
            return View(addordersModel);
        }
        [HttpGet]
        public ActionResult OrderFormPartial(int id)
        {
            ViewBag.id = id;
            return PartialView("OrderFormPartial");
        }
        [HttpGet]
        public ActionResult OrdersFormPartial()
        {
            return PartialView("OrdersFormPartial");
        }
        [HttpGet]
        public ActionResult OrderCompleted()
        {
            return View();
        }
        [HttpGet]
        public ActionResult MyOrders()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return HttpNotFound();
            }

        }
    }
}