using AutoMapper;
using GameHubCSharp.DAL.Data.Models;
using GameHubCSharp.BL.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.BL.Mapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<GameEvent, GameEventViewModel>();
            CreateMap<GameEventViewModel, GameEvent>();

            CreateMap<GameEventAddViewModel, GameEvent>();
            CreateMap<GameEvent, GameEventAddViewModel>();

            CreateMap<GameEvent, HomeEventRestViewModel>();
            CreateMap<HomeEventRestViewModel, GameEvent>();

            CreateMap<Game, GameViewModel>();
            CreateMap<GameViewModel, Game>();
            
            CreateMap<Player, PlayerViewModel>().ForMember(x => x.Username
            , m => m.MapFrom(src => src.User.UserName));// with options for binding Username to User.UserNamep
            CreateMap<PlayerViewModel, Player>();

            CreateMap<Post, PostViewModel>();
            CreateMap<PostViewModel, Post>();


        }
    }
}
