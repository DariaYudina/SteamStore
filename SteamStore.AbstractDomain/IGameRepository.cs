﻿using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamStore.AbstractDomain
{
    public interface IGameRepository
    {
        IEnumerable<Game> Games { get; }
    }
}
