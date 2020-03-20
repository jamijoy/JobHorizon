using JobHorizon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHorizon.Interface
{
    interface IMessegeRepository:IRepository<Messege>
    {
        IEnumerable<Messege> GetMyMessege(int SenderId, int ReceiverId);
        IEnumerable<Messege> GEtConnectedPeople(int id);
    }

}
