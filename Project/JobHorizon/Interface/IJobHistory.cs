using JobHorizon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHorizon.Interface
{
    interface IJobHistory:IRepository<JobHistory>
    {
        IEnumerable<JobHistory> GetApprovedJob(int id);
        IEnumerable<JobHistory> GetApprovedJobByType(string type);
    }
}
