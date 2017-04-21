using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeltExam.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace BeltExam.Controllers
{
    public class LoginController : Controller
    {
        private BeltExamContext _Context;
        public LoginController(BeltExamContext Context)
        {
            _Context = Context;
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.valErrors = ModelState.Values;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("register")]
        public IActionResult Register(RegisterViewModel NewUser)
        {
            User Validate = _Context.Users.Where(user => user.UserName == NewUser.UserName).SingleOrDefault(); 
            if (ModelState.IsValid && Validate == null)
            {
                User ValidUser = new User()  {
                    UserName = NewUser.UserName,
                    Email = NewUser.Email,
                    Password = NewUser.Password
                };
                _Context.Users.Add(ValidUser);
                _Context.SaveChanges();
                Validate = _Context.Users.Where(user => user.UserName == ValidUser.UserName).SingleOrDefault();
                HttpContext.Session.SetInt32("UserId", (int)Validate.UserId);
                return RedirectToAction("Index", "Home");
            }
            else 
            {
                ViewBag.valErrors = ModelState.Values;
                return View("Index");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("login")]
        public IActionResult Login(LoginViewModel User)
        {
            if (ModelState.IsValid)
            {
                User Validate = _Context.Users.Where(user => user.Email == User.Email).SingleOrDefault(); 
                if (Validate != null){
                    if ((string)Validate.Password == User.Password){
                        HttpContext.Session.SetInt32("UserId", (int)Validate.UserId);
                        return RedirectToAction("Index", "Home");
                    }
                    else 
                    {
                        ModelState.AddModelError("error", "Bad Password");   
                        ViewBag.valErrors = ModelState.Values;              
                        return View("Index");
                    }
                }
                else {
                    ModelState.AddModelError("error", "No account for that Username");   
                        ViewBag.valErrors = ModelState.Values;              
                        return View("Index");
                }
            }
            else 
            {
                ViewBag.valErrors = ModelState.Values;                
                return View("Index");
            }
        }
        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
