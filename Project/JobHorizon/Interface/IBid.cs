using JobHorizon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHorizon.Interface
{
    interface IBid : IRepository<BidList>
    {
        IEnumerable<BidList> GetBidList(int id);
    }
}
