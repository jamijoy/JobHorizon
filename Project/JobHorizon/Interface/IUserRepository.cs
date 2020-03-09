using JobHorizon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHorizon.Interface
{
    interface IUserRepository:IRepository<User>
    {
        IEnumerable<User> GetConnectedPeople(int MyId);
    }
}
