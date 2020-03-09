using JobHorizon.Interface;
using JobHorizon.Models;
using JobHorizon.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobHorizon.Controllers
{
    public class UserController : Controller
    {
        IRepository<User> userRepo = new UserRepository();

        public ActionResult Index()
        {
            return View(userRepo.GetAll());
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            return View(userRepo.Get(id));

        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(User u, HttpPostedFileBase file)
        {
            u.Rating = 0;
            u.Account_Status = "Active";
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Images"),
                                               Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    TempData["Message"] = "File uploaded successfully";
                    u.ProfileImage = file.FileName;
                }
                catch (Exception ex)
                {
                    TempData["Message"] = "ERROR:" + ex.Message.ToString();
                }
            }
            userRepo.Insert(u);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(userRepo.Get(id));
        }

        [HttpPost]
        public ActionResult Edit(User u)
        {
            userRepo.Update(u);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(userRepo.Get(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            userRepo.Delete(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
        public ActionResult MessegeList()
        {
            return View(userRepo.GetAll());
        }
        public ActionResult Messege(int id)
        {
            Session["ContactId"] = id;
            //return Content(id.ToString());
            return RedirectToAction("Index","Messege");
        }
    }
}