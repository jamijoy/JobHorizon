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
    public class RegistrationController : Controller
    {
        
        IRepository<User> userRepo = new UserRepository();
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(User u, HttpPostedFileBase file)
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
            return RedirectToAction("Index", "Login");
        }
    }
}