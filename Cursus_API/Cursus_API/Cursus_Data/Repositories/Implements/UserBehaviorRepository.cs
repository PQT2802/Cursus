using Cursus_Data.Context;
using Cursus_Data.Models.Entities;
using Cursus_Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Repositories.Implements
{
    public class UserBehaviorRepository : IUserBehaviorRepository
    {
        private readonly LMS_CursusDbContext _context;

        public UserBehaviorRepository(LMS_CursusDbContext context)
        {
            _context = context;
        }
        #region 8 Homepage View - LDQ
        public async Task AddUserBehavior(string UserId, string Key, DateTime dateTime)
        {
            UserBehavior userBehavior = new UserBehavior()
            {
                UserId = UserId,
                Key1 = Key,
                Date1 = dateTime
            };
            await _context.UserBehaviors.AddAsync(userBehavior);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserBehavior(string UserId, string Key, DateTime dateTime)
        {
            UserBehavior userBehavior = await _context.UserBehaviors.FirstOrDefaultAsync(x => x.UserId == UserId);
            var keys = new[] { userBehavior.Key1, userBehavior.Key2, userBehavior.Key3 };
            var dates = new[] { userBehavior.Date1, userBehavior.Date2, userBehavior.Date3 };

            int oldestIndex = -1;
            DateTime? oldestDate = DateTime.MaxValue;
            for (int i = 0; i < dates.Length; i++)
            {
                if (dates[i] == null)
                {
                    oldestIndex = i;
                    break;
                }
                if (dates[i] < oldestDate)
                {
                    oldestDate = dates[i];
                    oldestIndex = i;
                }
            }
            if (oldestIndex != -1)
            {
                keys[oldestIndex] = Key;
                dates[oldestIndex] = dateTime;
            }
            userBehavior.Key1 = keys[0];
            userBehavior.Date1 = dates[0];
            userBehavior.Key2 = keys[1];
            userBehavior.Date2 = dates[1];
            userBehavior.Key3 = keys[2];
            userBehavior.Date3 = dates[2];
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckUserBehaviorExist(string UserId)
        {
            return await _context.UserBehaviors.AnyAsync(x => x.UserId == UserId);
        }

        public async Task<UserBehavior> GetUserBehavior(string UserId)
        {
            return await _context.UserBehaviors.FirstOrDefaultAsync(x => x.UserId == UserId);
        }

        public async Task<List<string>> GetBehaviorOfUser(string UserId)
        {
            UserBehavior userBehavior = await _context.UserBehaviors.FirstOrDefaultAsync(x => x.UserId == UserId);
            if (userBehavior == null) return new List<string>();
            var behaviors = new List<(string Key, DateTime? Date)>
            {
                (userBehavior.Key1, userBehavior.Date1),
                (userBehavior.Key2, userBehavior.Date2),
                (userBehavior.Key3, userBehavior.Date3)
            };
            behaviors.Sort((x, y) => Nullable.Compare(y.Date, x.Date));
            var result = behaviors.Where(x => x.Key != null).Select(x => x.Key).ToList();
            return result;
        }
        #endregion
    }
}
