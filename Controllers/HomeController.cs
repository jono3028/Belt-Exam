using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeltExam.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
        [RouteAttribute("bright_ideas")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("UserId") == null ){
                return RedirectToAction("Index", "Login");
            }
            ViewBag.CurrentUser = _Context.Users.Where(u => u.UserId == (int)HttpContext.Session.GetInt32("UserId")).Single();
            ViewBag.AllIdeas = _Context.Ideas.Include(u => u.Owner).Include(l => l.LikedBy).OrderByDescending(l => l.LikedBy.Count).ToList();
            return View();
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RouteAttribute("newidea")]
        public IActionResult PostNewIdea(string UserIdea)
        {
            if (HttpContext.Session.GetInt32("UserId") == null ){
                return RedirectToAction("Index", "Login");
            }
            if (UserIdea.Length > 0)
            {
                Idea NewIdea = new Idea()
                {
                    UserId = (int)HttpContext.Session.GetInt32("UserId"),
                    UserIdea = UserIdea
                };
                _Context.Ideas.Add(NewIdea);
                _Context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        [RouteAttribute("like/{ideaId}")]
        public IActionResult PostLike(int ideaId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null ){
                return RedirectToAction("Index", "Login");
            }
            Like NewLike = new Like()
            {
                IdeaId = ideaId,
                UserId = (int)HttpContext.Session.GetInt32("UserId")
            };
            _Context.Likes.Add(NewLike);
            _Context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [RouteAttribute("users/{userId}")]
        public IActionResult UserInfo(int userId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null ){
                return RedirectToAction("Index", "Login");
            } 
            ViewBag.SelectedUser = _Context.Users.Where(u => u.UserId == userId).Include(i => i.UsersIdeas).Include(l => l.UserLikes).Single(); 
            return View();
        }
        [HttpGet]
        [RouteAttribute("bright_ideas/{ideaId}")]
        public IActionResult IdeaInfo(int ideaId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null ){
                return RedirectToAction("Index", "Login");
            } 
            ViewBag.SelectedIdea = _Context.Ideas.Where(i => i.IdeaId == ideaId).Include(u => u.Owner).Include(l => l.LikedBy).ThenInclude(u => u.User).Single(); 
            ViewBag.LikedBy = _Context.Likes.Where(i => i.IdeaId == ideaId).ToList();
            return View();
        }
        [HttpGet]
        [RouteAttribute("deleteidea/{ideaId}")]
        public IActionResult DeleteIdea(int ideaId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null ){
                return RedirectToAction("Index", "Login");
            } 
            var ToDelete = new Idea { IdeaId = ideaId };
            _Context.Ideas.Attach(ToDelete);
            _Context.Ideas.Remove(ToDelete);
            _Context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
