using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Interfaces
{
    public interface IUserBehaviorService
    {
        Task UserBehaviorSearch(string userId, string courseId);
        
    }
}
