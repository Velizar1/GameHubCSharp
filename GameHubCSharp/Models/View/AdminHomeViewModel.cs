using GameHubCSharp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.Models.View
{
    public class AdminHomeViewModel
    {
        private ICollection<User> users;
        private ICollection<Post> posts;
        private User admin;

        public ICollection<User> Users { get => users; set => users = value; }
        public ICollection<Post> Posts { get => posts; set => posts = value; }
        public User Admin { get => admin; set => admin = value; }
        public List<GameEvent> GameEvents { get; set; }
        public GameViewModel GameViewModel { get; set; }
        public List<Game> Games { get; set; }
        public Post Post { get; set; }
        public Category Category { get; set; }
        public List<Category> Categories { get; internal set; }
        public string Add { get; set; }
        public string Delete { get; set; }
    }
}
