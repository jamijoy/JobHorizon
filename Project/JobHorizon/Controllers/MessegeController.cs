using JobHorizon.Interface;
using JobHorizon.Models;
using JobHorizon.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobHorizon.Controllers
{
    public class MessegeController : Controller
    {
        MessegeRepository MessegeRepo = new MessegeRepository();
        [HttpGet]
        public ActionResult Index()
        {
            int id2 = Int32.Parse(Session["ContactId"].ToString());
            //return Content(id2.ToString());
            int id = Int32.Parse( Session["UserId"].ToString());
            try
            {
                ViewBag.MyMessege = MessegeRepo.GetMyMessege(id, id2);
                ViewBag.MyReply = MessegeRepo.GetMyReply(id, id2);
                return View();
            }
            catch (Exception)
            {

                throw;
            } 
        }
        [HttpPost]
        public ActionResult Index(Messege m)
        {
            m.SenderId = Int32.Parse(Session["UserId"].ToString());
            m.ReceiverId = Int32.Parse(Session["ContactId"].ToString());
            m.CreatedAt = DateTime.Now;
            MessegeRepo.Insert(m);
            //return Content(m.SenderId.ToString()+" "+ m.ReceiverId.ToString() + " " + m.CreatedAt.ToString() + " " + m.Messege1.ToString()+" "+m.MessegeId.ToString());
            return RedirectToAction("Index");
        }
    }
}