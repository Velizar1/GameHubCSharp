﻿using GameHubCSharp.Data.Models;

namespace GameHubCSharp.Services
{
    public interface IPlayerService
    {
        public Player FindPlayerById(string id);
    }
}