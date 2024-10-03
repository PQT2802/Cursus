using Cursus_Business.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Interfaces
{
    public interface ICartService
    {
        Task<dynamic> AddCourseToCart(string userId, string courseId);
        Task<Result> ViewCart(string userId);
    }
}
