﻿using GameHubCSharp.Data;
using GameHubCSharp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.Controllers
{
    public class UserController : Controller
    {
        private SignInManager<User> _signManager;
        private readonly ApplicationDbContext db;
        private UserManager<User> _userManager;

        public UserController(UserManager<User> userManager, SignInManager<User> signManager,ApplicationDbContext db)
        {
            _userManager = userManager;
            _signManager = signManager;
            this.db = db;
        }
        [HttpPost("/register")]
        public async Task<IActionResult> Registration(RegistartionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.UserName, Email=model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signManager.SignInAsync(user, false);
                    return RedirectToAction("Home", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("creatUser", error.Description);
                   
                    }
                }
            }
            return View();
        }


        [HttpGet("/register")]
        public IActionResult Registration()
        {
            return View();
        }


        [HttpGet("/login")]
        public IActionResult Login(string returnUrl = "")
        {
            var model = new LoginViewModel { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
           
            if (ModelState.IsValid)
            {
                //var result = await _signManager.PasswordSignInAsync(model.UserName,
                //   model.Password, model.RememberMe,false);
                var user = db.Users.FirstOrDefault(x => x.UserName == model.UserName);

                var check = await _userManager.CheckPasswordAsync(user,model.Password);

                if (check)
                {
                    await _signManager.SignInAsync(user, model.RememberMe);
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid login attempt");
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}