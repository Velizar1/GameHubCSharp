using GameHubCSharp.Models.SchedulerMIdels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((h, s) =>
            {
                s.AddQuartz(q =>
                {
                    q.UseMicrosoftDependencyInjectionScopedJobFactory();
                    var jobKey = new JobKey(nameof(EventJob));

                    q.AddJob<EventJob>(opts => opts.WithIdentity(jobKey));

                    q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("EventJob-trigger")
                    .WithCronSchedule(@"1 * * * * ?"));
                });
                s.AddQuartzHostedService(
                    q => q.WaitForJobsToComplete = true);

            })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
