using Ninject.Modules;
using SteamStore.AbstractBLL;
using SteamStore.AbstractDAL;
using SteamStore.BLL;
using SteamStore.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SteamStore.WebUI.Infastructure
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserBLL>().To<UserLogic>().InSingletonScope();
            Bind<IUserDAL>().To<UserDao>().InSingletonScope();
            Bind<IGameBLL>().To<GameLogic>().InSingletonScope();
            Bind<IGameDAL>().To<GameDao>().InSingletonScope();
            Bind<IOrderBLL>().To<OrderLogic>().InSingletonScope();
            Bind<IOrderDAL>().To<OrderDao>().InSingletonScope();
        }
    }
}