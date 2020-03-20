using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobHorizon.Models;
using JobHorizon.Repository;
using JobHorizon.Interface;

namespace JobHorizon.Controllers
{
    public class ForumController : Controller
    {
        IRepository<Forum> forumRepo = new ForumRepository();
        IRepository<User> userRepo = new UserRepository();
        IRepository<Comment> commentRepo = new CommentRepository();
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Comment = commentRepo.GetAll();
            return View(forumRepo.GetAll().OrderByDescending(x=> x.ForumId ));
        }
        [HttpPost]
        public ActionResult Index(Forum forum)
        {
            
            forum.CreatedBy = Int32.Parse(Session["UserId"].ToString());
            forum.CreatedAt = System.DateTime.Now;
            forumRepo.Insert(forum);
            
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(forumRepo.Get(id));
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            forumRepo.Delete(id);
            return RedirectToAction("Index");
        }

       public ActionResult Comment(string comment1,int? forum_id)
        {
            int id = int.Parse(this.RouteData.Values["id"].ToString());
            Comment com = new Comment();
            com.CreatedAt = System.DateTime.Now;
            com.Comment1 = comment1;
            com.ForumId = id;
            com.UserId = Int32.Parse(Session["UserId"].ToString());
            commentRepo.Insert(com);
            return RedirectToAction("Index");

        }
    }
}