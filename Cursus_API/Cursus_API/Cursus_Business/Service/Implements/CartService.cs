using Cursus_Business.Common;
using Cursus_Business.Exceptions.ErrorHandler;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Implements
{
    public class CartService : ICartService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICartRepository _cartRepository;
        public CartService (ICourseRepository courseRepository, ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
            _courseRepository = courseRepository;
        }
        public async Task<dynamic> AddCourseToCart(string userId, string courseId)
        {
            var course = await _courseRepository.GetCourseIsUsed(courseId);
            if (course == null)
            {
                return Result.Failure(CourseError.CourseIsNotExist);
            }
            var addCourse = await _cartRepository.AddCourseToCart(userId, courseId);
            return Result.SuccessWithObject(addCourse);

        }

        public async Task<Result> ViewCart(string userId)
        {
            var result = await _cartRepository.ViewCart(userId);
            if(result == null)
            {
                return Result.SuccessWithObject(new { Message = "Cart is empty!!!" });
            }
            return Result.SuccessWithObject(result);
        }
    }
}
