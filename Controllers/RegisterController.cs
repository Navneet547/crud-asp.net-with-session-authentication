
using AuthCrudApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using Microsoft.Extensions.Logging;


namespace AuthCrudApp.Controllers
{

    public class RegisterController : Controller
    {
        private readonly AuthDBContext dBContext;
        private readonly ILogger<RegisterController> _logger;

        public RegisterController(AuthDBContext dBContext, ILogger<RegisterController> logger)
        {
            this.dBContext = dBContext;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(User model)
        {
            var data = dBContext.Users.Where(x => x.Username == model.Username).FirstOrDefault();
            if (data != null)
            {
                ViewBag.UserExists = "User already exists.";
                _logger.LogError("User already exists.");
                return View();
            }
            else
            {
                dBContext.Users.Add(model);
                dBContext.SaveChanges();
                ViewBag.Message = "User Created Successfully";
                _logger.LogInformation($"User {model.Username} Created successfully.");
                return View();
            }
        }
    }
}
