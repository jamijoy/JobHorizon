using JobHorizon.Interface;
using JobHorizon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobHorizon.Repository
{
    public class JobHistoryRepository : Repository<JobHistory>, IJobHistory
    {
        JobHorizonContext context;
        public JobHistoryRepository()
        {
            context = new JobHorizonContext();
        }

        public IEnumerable<JobHistory> GetApprovedJob(int id)
        {
            return context.Set<JobHistory>().Where(x => x.JobList.PostedBy == id && x.Status=="Pending");
        }

        public IEnumerable<JobHistory> GetApprovedJobByType(string type)
        {
            return context.Set<JobHistory>().Where(x => x.JobList.JobType == type && x.Status == "Pending");
        }

        public IEnumerable<JobHistory> GetHistoryById(int id)
        {
            return context.Set<JobHistory>().Where(x => x.BitBy == id && x.Status == "Finished");
        }
        public string HistoryList(int id)
        {
            //public static List<JobHistory> history;

            //using (var context = new StuffContext()) 
            //{ 
            //    history = new List<JobHistory>();
            //    history = (from r in context.Set<JobHistory>().Where(x => x.BitBy == id && x.Status == "Finished") select r).ToList();
            //}

            //List<object> history = new List<object>();
            List<JobHistory> history1 = new List<JobHistory>();

            string str = "";
            history1 = context.Set<JobHistory>().Where(x => x.BitBy == id && x.Status == "Finished").ToList();
            foreach (var item in history1)
            {
                //history.Add(item.BitBy);
                //history.Add(item.HistoryId);
                //history.Add(item.JobId);
                //history.Add(item.JobList);
                //history.Add(item.Rating);
                //history.Add(item.Status);
                //history.Add(item.User);
                str = str + "\n History ID : " + item.HistoryId.ToString();
                str = str + "\n Job ID : " + item.JobId.ToString();
                str = str + "\n Rating : " + item.Rating.ToString();
                str = str + "\n Status : " + item.Status.ToString();
                str = str + " \n .........................................";
            }
            return str;
        }
    }
}