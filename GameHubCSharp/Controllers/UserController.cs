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
        private UserManager<User> _userManager;

        public UserController(UserManager<User> userManager, SignInManager<User> signManager)
        {
            _userManager = userManager;
            _signManager = signManager;
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
                    return RedirectToAction("Home", "HomeController");
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
                var result = await _signManager.PasswordSignInAsync(model.Username,
                   model.Password, model.RememberMe,false);

                if (result.Succeeded)
                {
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


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
