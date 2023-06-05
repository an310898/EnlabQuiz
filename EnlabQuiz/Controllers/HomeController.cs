using EnlabQuiz.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EnlabQuiz.Controllers
{
    public class HomeController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Quiz()
        {
            return View();
        }
    }
}