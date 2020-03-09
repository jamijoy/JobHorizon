using JobHorizon.Interface;
using JobHorizon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobHorizon.Repository
{
    public class MessegeRepository : Repository<Messege>, IMessegeRepository
    {
        JobHorizonContext context;

        public MessegeRepository()
        {
            context = new JobHorizonContext();
        }

        public IEnumerable<Messege> GetMyMessege(int SenderId, int ReceiverId)
        {
            return context.Set<Messege>().Where(x => x.SenderId == SenderId && x.ReceiverId == ReceiverId).ToList();
        }

        public IEnumerable<Messege> GetMyReply(int ReceiverId, int SenderId)
        {
            return context.Set<Messege>().Where(x => x.ReceiverId == ReceiverId && x.SenderId == SenderId).ToList();
        }
    }
}