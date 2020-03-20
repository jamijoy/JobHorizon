using JobHorizon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobHorizon.Controllers
{
    public class DashboardController : Controller
    {
        JobHorizonContext context = new JobHorizonContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Statistics()
        {
            try
            {
                ViewBag.NoOfUser = context.Set<User>().Count();
                ViewBag.NoOfJobs = context.Set<JobList>().Count();
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}