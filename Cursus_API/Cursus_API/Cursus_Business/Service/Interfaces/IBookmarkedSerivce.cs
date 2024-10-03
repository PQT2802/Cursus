using Cursus_Data.Models.DTOs.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Interfaces
{
    public interface IBookmarkedSerivce
    {
        Task<dynamic> GetListBookMark(string userId);
        Task<dynamic> AddBookmark(BookMarkDTO bookmark, string userId);
        Task<dynamic> RemoveBookMark(int BookMarkId);
    }
}
