using AutoMapper;
using GameHubCSharp.Data;
using GameHubCSharp.Data.Models;
using GameHubCSharp.Services;
using GameHubCSharp.Services.IServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureApplicationCookie(x => x.LoginPath = "/login");
            services.AddDbContext<ApplicationDbContext>(options =>
                options
                .UseLazyLoadingProxies()
                .UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            
            services.AddCors(options =>
            {
                options.AddPolicy("Allow",
                                  builder =>
                                  {
                                      builder.
                                      AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader();
                                  });
            });
           

            services.AddDefaultIdentity<User>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
               
            }).AddRoles<IdentityRole<Guid>>()

                .AddEntityFrameworkStores<ApplicationDbContext>();
           //TODO exception in roles 
           // services.AddIdentity<User, string>().AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddAutoMapper(typeof(Startup));

            services.AddControllersWithViews();
            services.AddTransient<IHomeService, HomeService>();
            services.AddTransient<IGameEventService, GameEventService>();
            services.AddTransient<IPlayerService, PlayerService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<IPostService, PostService>();
        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors("Allow");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
