using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeltExam.Models;
using Microsoft.AspNetCore.Http;

namespace BeltExam.Controllers
{
    public class HomeController : Controller
    {
        private BeltExamContext _Context;
        public HomeController(BeltExamContext Context)
        {
            _Context = Context;
        }
        // GET: /Home/
        [HttpGet]
        [RouteAttribute("home")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("UserId") == null ){
                return RedirectToAction("Index", "Login");
            }
            else {
                return View();
            }
        }
    }
}
