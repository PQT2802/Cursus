using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<dynamic> AddCourseToCart(string userId, string courseId);
        Task<dynamic> ViewCart(string userId);
        Task<dynamic> GetCartItemById(Guid id);
    }
}
