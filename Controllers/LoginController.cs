using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AuthCrudApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using AuthCrudApp.ViewModels;
using Microsoft.Extensions.Logging;
using NuGet.Protocol.Plugins;


namespace AuthCrudApp.Controllers
{
   
    public class LoginController : Controller
    {
        private readonly AuthDBContext DBContext;
        private readonly ILogger<LoginController> _logger;

        public LoginController(AuthDBContext DBContext, ILogger<LoginController> logger)
        {
            this.DBContext = DBContext;
            _logger = logger;
        }
       
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var login = DBContext.Users.Where(m => m.Username == model.Username && m.Password == model.Password).FirstOrDefault();
                    if (login != null)
                    {
                        HttpContext.Session.SetString("Username", login.Username);
                        _logger.LogInformation($"User {login.Username} logged in successfully."); 
                        return RedirectToAction("Index", "Employee");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = " Invalid Credential";
                        _logger.LogWarning($"Invalid login attempt for user {model.Username}."); 
                        return View();
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login."); 
                ViewBag.ErrorMessage = "An error occurred during login.";
                return View();
            }
        }

        public IActionResult LogOut()
        {
            try
            {
                var username = HttpContext.Session.GetString("Username");
                if (!string.IsNullOrEmpty(username))
                {
                    _logger.LogInformation($"User {username} logged out."); 
                }

                HttpContext.Session.Clear();
                HttpContext.Session.Remove("Username");

                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during logout."); 
                return RedirectToAction("Index", "Login");
            }
        }

    }
}
