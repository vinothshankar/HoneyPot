using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HoneypotDemo.Models;
using AspNetCore.Honeypot;

namespace HoneypotDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Honeypot]
        [HttpPost]
        public IActionResult Register(RegisterModel registerData)
        {
            if (HttpContext.IsHoneypotTrapped())
            {
                ModelState.Clear();
                ModelState.AddModelError("", "bot detection");

                return View("Index", new RegisterModel());
            }
            else
            {
                return View("Register");
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
