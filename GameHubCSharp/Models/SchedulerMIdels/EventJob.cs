using GameHubCSharp.DAL.Data;
using GameHubCSharp.Services;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.Models.SchedulerMIdels
{
    public class EventJob : IJob
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger<EventJob> logger;
        private readonly IGameEventService eventService;

        public EventJob(ApplicationDbContext context,
            ILogger<EventJob> logger,
            IGameEventService eventService)
        {
            this.context = context;
            this.logger = logger;
            this.eventService = eventService;
        }
        public Task Execute(IJobExecutionContext context)
        {
            eventService.DeleteAllExpiredGameEvents();
            logger.LogInformation($"{DateTime.Now.Minute}");
            return Task.CompletedTask;
        }
    }
}
