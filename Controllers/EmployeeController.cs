using AuthCrudApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace AuthCrudApp.Controllers
{
    public class EmployeeController : Controller
    {
       
        private readonly AuthDBContext dBContext;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(AuthDBContext dBContext, ILogger<EmployeeController> logger)
        {
            this.dBContext = dBContext;
            _logger = logger;
        }
        public IActionResult Index()
        {

            if (HttpContext.Session.GetString("Username") != null)
            {
                
                var listofData = dBContext.Employees.ToList();
                _logger.LogInformation("Employees List fetched successfully.");
                return View(listofData);
            }
            else
            {
                _logger.LogError("Without login user wants to fetch employee List ");
                return RedirectToAction("Index","Login");
            }
            
        }

        [HttpGet]
            public IActionResult Create()
            {
                if (HttpContext.Session.GetString("Username") != null)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }

            [HttpPost]
            public IActionResult Create(Employee model)
            {
                try
                {
                    dBContext.Employees.Add(model);
                    dBContext.SaveChanges();
                    ViewBag.Message = "Data Insert Successfully";

                    _logger.LogInformation("Data inserted successfully."); 

                    return View();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error inserting data."); 
                    ViewBag.Message = "Error inserting data.";

                    return View();
                }
            }
            [HttpGet]
            public IActionResult Edit(int id)
            {
                if (HttpContext.Session.GetString("Username") != null)
                {
                    var data = dBContext.Employees.Where(x => x.EmployeeId == id).FirstOrDefault();
                    return View(data);
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            [HttpPost]
            public IActionResult Edit(Employee Model)
            {
                var data = dBContext.Employees.Where(x => x.EmployeeId == Model.EmployeeId).FirstOrDefault();
                if (data != null)
                {
                    data.EmployeeName = Model.EmployeeName;
                    data.EmployeeGender = Model.EmployeeGender;
                    data.EmployeeAge = Model.EmployeeAge;
                    data.EmployeeAddress = Model.EmployeeAddress;
                    dBContext.SaveChanges();
                    _logger.LogInformation($"EmployeeID {Model.EmployeeId} has updated successfully ");
                }
                else
                {
                _logger.LogError($"EmployeeID {Model.EmployeeId} got some error in updation. ");
                }
                return RedirectToAction("index");
            }
            public IActionResult Delete(int id)
            {
                if (HttpContext.Session.GetString("Username") != null)
                {
                    var data = dBContext.Employees.Where(x => x.EmployeeId == id).FirstOrDefault();
                    dBContext.Employees.Remove(data);
                    _logger.LogInformation($"EmployeeID {id} has been deleted successfully ");
                    dBContext.SaveChanges();
                    return RedirectToAction("index");
                }
                else
                {
                _logger.LogError($"Error at Delete for EmployeeID {id}.");
                return RedirectToAction("Index", "Login");
                }
            }
            public IActionResult Detail(int id)
            {
                if (HttpContext.Session.GetString("Username") != null)
                {
                    var data = dBContext.Employees.Where(x => x.EmployeeId == id).FirstOrDefault();
                    _logger.LogInformation($"EmployeeID {id} Details get successfully ");
                    return View(data);
                }
                else
                {
                    _logger.LogError($"EmployeeID {id} Details get Error ");
                    return RedirectToAction("Index", "Login");
                }
           
            }
            public IActionResult Privacy()
            {
                if (HttpContext.Session.GetString("Username") != null)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

    }
}

