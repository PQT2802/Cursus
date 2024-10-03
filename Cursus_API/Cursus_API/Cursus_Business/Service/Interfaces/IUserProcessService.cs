using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Interfaces
{
    public interface IUserProcessService
    {
        Task<dynamic> StudyCourse(string userId, string courseContentId);
    }
}
