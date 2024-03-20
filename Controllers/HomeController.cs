using Microsoft.AspNetCore.Mvc;

namespace AuthCrudApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
