﻿using GameHubCSharp.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.BL.Services.IServices
{
    public interface INotificationService
    {
        public Notification Add(Notification notification);
        public Notification Delete(Notification notification);
        public List<Notification> GetForEvent(GameEvent gameEvent);

       

    }
}