using Cursus_Business.Common;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Implements
{
    public class CourseVersionDetailService : ICourseVersionDetailService
    {
        private readonly ICourseVersionDetailRepository _courseVersionDetailRepository;
        private readonly ICourseRepository _courseRepository;

        public CourseVersionDetailService(ICourseVersionDetailRepository courseVersionDetailRepository, ICourseRepository courseRepository)
        {
            _courseVersionDetailRepository = courseVersionDetailRepository;
            _courseRepository = courseRepository;
        }
        public async Task<dynamic> GetTopPurchasedCourse(int? year, int? month = null, int? quarter = null)
        {
            if (year == 0)
            {
                year = DateTime.Now.Year;
            }
            if (quarter.HasValue && (quarter < 1 || quarter > 4))
            {
                return Result.Failure(Result.CreateError("Quarter", "Quarter must be between 1 and 4"));
            }
            if (!await _courseVersionDetailRepository.YearExists(year.Value))
            {
                return Result.Failure(Result.CreateError("Year", "Year does not exist "));
            }
            if (month.HasValue && quarter.HasValue)
            {
                return Result.Failure(Result.CreateError("Input", "You can specify either month or quarter, but not both."));
            }

            if (month.HasValue && !await _courseVersionDetailRepository.MonthExists(month.Value))
            {
                return Result.Failure(Result.CreateError("Month", "Month does not exist "));
            }
            if (quarter.HasValue && !await _courseVersionDetailRepository.QuarterExists(year.Value, quarter.Value))
            {
                return Result.Failure(Result.CreateError("Quarter", "There is no month is exist in this quarter "));
            }
            var result = await _courseVersionDetailRepository.GetTopPurchasedCourse(year.Value, month, quarter);
            return Result.SuccessWithObject(result);
        }
        public async Task<dynamic> GetTopBadCourse(int? year, int? month = null, int? quarter = null)
        {
            if (year == 0)
            {
                year = DateTime.Now.Year;
            }
            if (quarter.HasValue && (quarter < 1 || quarter > 4))
            {
                return Result.Failure(Result.CreateError("Quarter", "Quarter must be between 1 and 4"));
            }
            if (!await _courseVersionDetailRepository.YearExists(year.Value))
            {
                return Result.Failure(Result.CreateError("Year", "Year does not exist "));
            }
            if (month.HasValue && quarter.HasValue)
            {
                return Result.Failure(Result.CreateError("Input", "You can specify either month or quarter, but not both."));
            }
            if (month.HasValue && !await _courseVersionDetailRepository.MonthExists(month.Value))
            {
                return Result.Failure(Result.CreateError("Month", "Month does not exist "));
            }
            if (quarter.HasValue && !await _courseVersionDetailRepository.QuarterExists(year.Value, quarter.Value))
            {
                return Result.Failure(Result.CreateError("Quarter", "There is no month is exist in this quarter "));
            }
            var result = await _courseVersionDetailRepository.GetTopBadCourse(year.Value, month, quarter);
            return Result.SuccessWithObject(result);
        }
    }
}