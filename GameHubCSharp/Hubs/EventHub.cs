using AutoMapper;
using GameHubCSharp.DAL.Data;
using GameHubCSharp.Models;
using GameHubCSharp.BL.Models.DTO;
using GameHubCSharp.BL.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameHubCSharp.BL.Services.IServices;

namespace GameHubCSharp.Hubs
{
    public class EventHub : Hub
    {
        private readonly ApplicationDbContext db;
        private readonly IUserService userService;
        private readonly IGameEventService gameEventService;
        private readonly IMapper mapper;
        private readonly IPlayerService playerService;

        public EventHub(ApplicationDbContext db, IUserService userService, IGameEventService gameEventService, IMapper mapper, IPlayerService playerService)
        {
            this.db = db;
            this.userService = userService;
            this.gameEventService = gameEventService;
            this.mapper = mapper;
            this.playerService = playerService;
        }

        public async Task UpdateEvents()
        {
            var list = gameEventService.FindAll().ToList();
            var list2 = list.Select(x => {
                var re = mapper.Map<HomeEventRestViewModel>(x);
                re.OwnerName = playerService.FindPlayerById(x.OwnerId).UsernameInGame;
                re.ImageUrl = x.Game.ImageUrl;
                re.TakenPlaces = x.NumberOfPlayers;
                return re;
            })
                .ToList();
            await this.Clients.All.SendAsync("UpdateEventList", new
            {
                GameEvents = list2.ToArray().OrderByDescending(x=>x.StartDate) 
            });
        }
    }
}
