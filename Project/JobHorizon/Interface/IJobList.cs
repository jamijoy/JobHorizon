using JobHorizon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHorizon.Interface
{
    interface IJobList: IRepository<JobList>
    {
        IEnumerable<JobList> JobPostedByCustomer(int id);
        IEnumerable<JobList> JobForWorker(string type);
    }
}
