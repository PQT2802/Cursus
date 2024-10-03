using Cursus_Business.Common;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Models.Entities;
using Cursus_Data.Repositories.Implements;
using Cursus_Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Implements
{
    public class UserBehaviorService : IUserBehaviorService
    {
        private readonly IUserBehaviorRepository _userBehaviorRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ICategoryRepository _categoryRepository;
        public UserBehaviorService(IUserBehaviorRepository userBehaviorRepository, ICourseRepository courseRepository, ICategoryRepository categoryRepository)
        {
            _userBehaviorRepository = userBehaviorRepository;
            _courseRepository = courseRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task UserBehaviorSearch(string userId, string courseId)
        {
            Course c = await _courseRepository.FindCourseByid(courseId);
            Category cate = await _categoryRepository.GetProgramLanguageById(c.CategoryId);

            if (!await _userBehaviorRepository.CheckUserBehaviorExist(userId))           
                await _userBehaviorRepository.AddUserBehavior(userId, cate.Name, DateTime.Now);            
            else           
                await _userBehaviorRepository.UpdateUserBehavior(userId, cate.Name, DateTime.Now);           
        }
    }
}
