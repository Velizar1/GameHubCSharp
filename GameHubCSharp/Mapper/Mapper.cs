using AutoMapper;
using GameHubCSharp.Data.Models;
using GameHubCSharp.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.Mapper
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
