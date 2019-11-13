using SteamStore.AbstractBLL;
using SteamStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SteamStore.WebUI.Controllers
{
    public class UserController : Controller
    {
        public IUserBLL _userLogic;
        public UserController(IUserBLL userLogic)
        {
            _userLogic = userLogic;
        }
        [HttpGet]
        public ActionResult SignUp()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("~");
            }
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(SignupModel signupModel, HttpPostedFileBase Avatar)
        {
            if (ModelState.IsValid)
            {
                string image = "";
                byte[] imageData = null;
                if (Avatar != null)
                {
                    using (var binaryReader = new BinaryReader(Avatar.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(Avatar.ContentLength);
                    }
                    image = Convert.ToBase64String(imageData);
                }
                else
                {
                    image = SignupModel.defaultavatar;
                }
                MailAddress from = new MailAddress("dashaudina06@gmail.com", "Registration");
                MailAddress to = new MailAddress(signupModel.Email);
                MailMessage msg = new MailMessage(from, to);
                msg.Subject = "Подтверждение регистрации";
                msg.Body = string.Format("Для завершения регистрации перейдите по ссылке: " + "<a href=\"{0}\" title=\"Подтвердить регистрацию\">{0}</a>", Url.Action("ConfirmEmail", "User", new { userName = signupModel.Login, email = signupModel.Email }, Request.Url.Scheme));
                msg.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("dashaudina06@gmail.com", "dD89271518969");
                smtp.EnableSsl = true;
                smtp.Send(msg);

                if (_userLogic.AddUser(signupModel.Login, signupModel.Password, signupModel.Email, image))
                {
                    return RedirectToAction("Confirm", "User", new { email = signupModel.Email });
                }
                else
                {
                    return View(signupModel);
                }
            }
            return View(signupModel);
        }
        public ActionResult Confirm(string email)
        {
            ViewBag.Email = email;
            return View();
        }

        public ActionResult ConfirmEmail(string userName, string email)
        {
            _userLogic.ConfirmEmail(userName);
            return RedirectToAction("SignIn", "User");
        }
        [HttpGet]
        public ActionResult SignIn()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("~");
            }
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(SigninModel signinModel)
        {
            if (_userLogic.VerifyUser(signinModel.Login, signinModel.Password))
            {
                FormsAuthentication.SetAuthCookie(signinModel.Login, true);
                return Redirect("~");
            }
            else
            {
                return View(signinModel);
            }
        }
        [HttpGet]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}