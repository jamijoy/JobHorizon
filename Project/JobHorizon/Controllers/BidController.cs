using JobHorizon.Models;
using JobHorizon.Repository;
using System;
using System.Web.Mvc;

namespace JobHorizon.Controllers
{

    public class BidController : Controller
    {
        BidRepository BidRepo = new BidRepository();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(BidList m)
        {
            m.JobId = Int32.Parse(Session["JobId"].ToString());
            m.BidBy = Int32.Parse(Session["UserId"].ToString());
            BidRepo.Insert(m);
            return RedirectToAction("Index", "JobList");
        }
    }
}