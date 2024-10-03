using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Interfaces
{
    public interface ICourseVersionDetailService
    {
        Task<dynamic> GetTopPurchasedCourse(int? year = null, int? month = null, int? quarter = null);
        Task<dynamic> GetTopBadCourse(int? year = null, int? month = null, int? quarter = null);
    }
}
