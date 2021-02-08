using GameHubCSharp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.Models
{
    public class ConnectionIdProvider
    {
        public static Dictionary<string, string> ids = new Dictionary<string, string>();
        public static List<Notification> notifications = new List<Notification>();
        public static List<GameEvent> events = new List<GameEvent>();
    }
}
