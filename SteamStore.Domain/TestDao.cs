using SteamStore.AbstractDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamStore.Domain
{
    public class TestDao : ITestDao
    {
        public string Test()
        {
            return "Hello";
        }
    }
}
