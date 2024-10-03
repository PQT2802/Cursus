using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Interfaces
{
    public interface IWalletService
    {
        Task<bool> RegisterUserWalletlAsync(string UserId);
        Task<dynamic> AvailableMoneyAllCourseOfInstructor(string instructorId);
        Task<dynamic> AvailableMoneyEachCourseOfInstructor(string instructorId);
        Task<dynamic> AvailableMoneyCanWithdrawOfInstructor(string userId);

        //Test
        Task<dynamic> WithdrawMoneyToWallet(string userId, double withdrawMoney);
        //Test

        Task<dynamic> DepositMoneyToWallet(string userId, double depositMoney);
    }
}
