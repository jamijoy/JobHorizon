using iTextSharp.text;
using iTextSharp.text.pdf;
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
    public class JobHistoryController : Controller
    {
        Document doc;
        MemoryStream mem = new MemoryStream();

        JobListRepository JobListRepo = new JobListRepository();
        JobHistoryRepository JobHistoryRepo = new JobHistoryRepository();
        BidRepository BidRepo = new BidRepository();
        MessegeRepository msgRepo = new MessegeRepository();

        public ActionResult Index()
        {
            if(Session["UserType"].ToString() == "Admin")
            {
                return View(JobHistoryRepo.GetAll());
            }
            else if(Session["UserType"].ToString() == "Customer")
            {
                return View(JobHistoryRepo.GetApprovedJob(Int32.Parse(Session["UserId"].ToString())));
            }
            else
            {
                return View(JobHistoryRepo.GetApprovedJobByType(Session["WorkType"].ToString()));
            }
        }

        public ActionResult Completed()
        {
            return View(JobHistoryRepo.GetHistoryById(Convert.ToInt32(Session["UserId"])));
        }
        public ActionResult Approve()
        {
            var j = new JobHistory();
            int BidId = Int32.Parse(Session["BidId"].ToString());
            //job approve in jobhistory table
            var JobApprove = BidRepo.Get(BidId);

            j.JobId = JobApprove.JobId;
            j.BitBy = JobApprove.BidBy;
            j.Status = "Pending";
            j.Rating = 0;


            //return Content(Bid.ToString());
            JobHistoryRepo.Insert(j);
            // Modify in joblist table
            var joblist = JobListRepo.Get(JobApprove.JobId);
            joblist.Status = "Pending";
            JobListRepo.Update(joblist);

            ////Add to messege table two way ato communication
            //var m = new Messege();
            //m.SenderId = postedBy;
            //m.ReceiverId = Bid;
            //m.Messege1 = "Hello Mr/Ms " + j.User.Name+" I Have appointed you for this job";
            //m.CreatedAt = DateTime.Now;
            //msgRepo.Insert(m);

            //var m2 = new Messege();
            //m2.SenderId = Bid;
            //m2.ReceiverId = postedBy; 
            //m2.Messege1 = "Lets Discuss About the Job in details";
            //m2.CreatedAt = DateTime.Now;
            //msgRepo.Insert(m);

            return RedirectToAction("Index","JobList");
        }
        [HttpGet]
        public ActionResult Finished(int id)
        {
            Session["HistoryId"] = id;
            return View();
        }
        [HttpPost]
        public ActionResult Finished(JobHistory j)
        {
            
            var job = JobHistoryRepo.Get(Int32.Parse(Session["HistoryId"].ToString()));
            job.Rating = j.Rating;
            job.Status = "Finished";
            JobHistoryRepo.Update(job);

            return RedirectToAction("Index", "JobList");
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            JobHistoryRepo.Delete(id);
            return RedirectToAction("Index");
        }





        [HttpGet, ActionName("Download")]
        public ActionResult CreateFile()
        {
            //List<object> history = JobHistoryRepo.HistoryList(Convert.ToInt32(Session["UserId"]));
            byte[] abytes = this.preparePDF();
            return File(abytes, "application/pdf");
        }

        public byte[] preparePDF()
        {
            string str2 = JobHistoryRepo.HistoryList(Convert.ToInt32(Session["UserId"]));

            IList<string> newlist = new List<string>();
            newlist.Add("Haay");
            newlist.Add("there jami");
            Paragraph para = new Paragraph();
            Chunk data = new Chunk();

            //string str2 = "\n";
            //for (int i = 0; i < newlist.Count; i++)
            //{
            //    str2 = newlist[i].ToString();
            //    data.Append(str2 + Environment.NewLine);
            //    para.Add(new Chunk(str2));
            //    para.Add(Chunk.NEWLINE);
            //}

            para.Add(new Chunk(str2));

            string path = Server.MapPath("PDFs");
            Chunk hello = new Chunk("Hye There");
            doc = new Document(PageSize.A4, 0f, 0f, 0f, 0f);
            doc.SetPageSize(PageSize.A4);
            doc.SetMargins(20f, 20f, 20f, 20f);

            PdfWriter.GetInstance(doc, new FileStream(path + "/Report.pdf", FileMode.Create));
            PdfWriter.GetInstance(doc, mem);
            doc.Open();
            doc.Add(para);
            doc.Close();
            return mem.ToArray();
        }
    }
}