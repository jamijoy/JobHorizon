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
    public class JobListController : Controller
    {
        JobListRepository JobListRepo = new JobListRepository();
        BidRepository BidRepo = new BidRepository();
        public ActionResult Index()
        {
            try
            {
                if (Session["UserType"].ToString() == "Worker")
                {
                    ViewBag.TableTitle = "Jobs According to your Work Type";
                    string WorkType = Session["WorkType"].ToString();
                    return View(JobListRepo.JobForWorker(WorkType));
                }
                else if(Session["UserType"].ToString() == "Customer")
                {
                    ViewBag.TableTitle = "Jobs That You Have Posted";
                    int id = Int32.Parse(Session["UserId"].ToString());
                    return View(JobListRepo.JobPostedByCustomer(id));
                }
                else
                {
                    ViewBag.TableTitle = "All The Active Jobs";
                    return View(JobListRepo.GetAll());
                }
            }
            catch (Exception)
            {

                return Content("Sql Statement Failed");
            }
            
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            try
            {
                ViewBag.BidList = BidRepo.GetBidList(id);
                return View(JobListRepo.Get(id));
            }
            catch (Exception)
            {

                return View(JobListRepo.Get(id));
            }
            
            

        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(JobList u)
        {
            u.PostedBy = Int32.Parse(Session["UserId"].ToString());
            u.Status = "Active";
            JobListRepo.Insert(u);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(JobListRepo.Get(id));
        }

        [HttpPost]
        public ActionResult Edit(JobList u)
        {
            u.PostedBy = Int32.Parse(Session["UserId"].ToString());
            u.Status = "Active";
            JobListRepo.Update(u);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(JobListRepo.Get(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            JobListRepo.Delete(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Bid(int id)
        {
            Session["JobId"] = id;
            return RedirectToAction("Create","Bid");
        }
        [HttpGet]
        public ActionResult Approve(int id)
        {
            Session["BidId"] = id;
            return RedirectToAction("Approve", "JobHistory");
        }
    }
}