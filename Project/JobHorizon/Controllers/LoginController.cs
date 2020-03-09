using JobHorizon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobHorizon.Controllers
{
    public class LoginController : Controller
    {
        JobHorizonContext context = new JobHorizonContext();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(User u)
        {
            try
            {
                var login = context.Set<User>().SingleOrDefault(x => x.Email == u.Email && x.Password == u.Password);
                if(login!=null)
                {
                    //return Content(login.ToString());
                    Session["UserId"] = login.UserId;
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    TempData["Error"] = "Login Attempt Failed";
                    return RedirectToAction("Index");
                }
                
            }
            catch (Exception e)
            {

                TempData["Error"] = "Login Attempt Failed";
                return RedirectToAction("Index");
            }

        }
    }
}