using Cursus_Data.Models.DTOs;
using Cursus_Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Repositories.Interfaces
{
    public interface IBookmarkedRepository
    {
        Task<List<CourseForGuestDTO>> GetListBookMark(string userId);
        Task AddBookMark(Bookmark mark);
        Task RemoveBookMark(Bookmark mark);
        Task<Bookmark> GetBookMarkById(int id);
    }
}
