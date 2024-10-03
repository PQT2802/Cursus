using Cursus_Business.Common;
using Cursus_Business.Exceptions.ErrorHandler;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Models.DTOs;
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
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseVersionRepository _courseVersionRepository;
        private readonly ICourseVersionDetailRepository _courseVersionDetailRepository;
        private readonly IInstructorRepository _instructorRepository;
        private readonly IFinancialTransactionsRepository _financialTransactionsRepository;
        private readonly ISystemTransactionRepository _systemTransactionRepository;

        private readonly double minDepositMoney = 100000;



        public WalletService(IWalletRepository walletRepository, ICourseRepository courseRepository, ICourseVersionRepository courseVersionRepository,
            ICourseVersionDetailRepository courseVersionDetailRepository, IInstructorRepository instructorRepository, IFinancialTransactionsRepository financialTransactionsRepository,
            ISystemTransactionRepository systemTransactionRepository)
        {
            _walletRepository = walletRepository;
            _courseRepository = courseRepository;
            _courseVersionRepository = courseVersionRepository;
            _courseVersionDetailRepository = courseVersionDetailRepository;
            _instructorRepository = instructorRepository;
            _financialTransactionsRepository = financialTransactionsRepository;
            _systemTransactionRepository = systemTransactionRepository;
        }

        #region 26 Earning analytics - LDQ
        public async Task<dynamic> AvailableMoneyEachCourseOfInstructor(string instructorId)
        {
            List<string> courseList = await _courseRepository.GetCourseIdByInstructorId(instructorId);
            List<string> result = new List<string>();
            foreach (var course in courseList)
            {
                List<int> courseVersionList = await _courseVersionRepository.GetCourseVersionByCourseId(course);
                double availableMoney = 0;
                foreach (var cv in courseVersionList)
                {
                    availableMoney += await _courseVersionDetailRepository.GetPriceByCourseVersionId(cv);
                    availableMoney = Math.Round(availableMoney, 2);
                }
                result.Add($"Course: {course}, Total Money: {availableMoney}");
            }
            return Result.SuccessWithObject(result);
        }

        public async Task<dynamic> AvailableMoneyAllCourseOfInstructor(string instructorId)
        {
            List<string> courseList = await _courseRepository.GetCourseIdByInstructorId(instructorId);
            List<int> courseVersionList = new List<int>();
            foreach (var course in courseList)
            {
                List<int> a = await _courseVersionRepository.GetCourseVersionByCourseId(course);
                courseVersionList.AddRange(a);
            }
            double availableMoney = 0;
            foreach (var cv in courseVersionList)
            {
                availableMoney += await _courseVersionDetailRepository.GetPriceByCourseVersionId(cv);
            }
            availableMoney = Math.Round(availableMoney, 2);
            return Result.SuccessWithObject(availableMoney);
        }
        public async Task<dynamic> AvailableMoneyCanWithdrawOfInstructor(string userId)
        {
            double availableMoney = await AvailableMoneyOfInstructor(userId);
            return Result.SuccessWithObject(availableMoney);
        }

        //Test withdraw
        public async Task<dynamic> WithdrawMoneyToWallet(string userId, double withdrawMoney)
        {
            if(! await _walletRepository.CheckWallet(userId)) await RegisterUserWalletlAsync(userId);
            if (!await _financialTransactionsRepository.CheckFinancialTransactions(userId))
                await _financialTransactionsRepository.AddFinancialTransactions(userId);
            FinancialTransactions financialTransactions = await _financialTransactionsRepository.GetFinancialTransactions(userId);
            Wallet wallet = await _walletRepository.GetWalletByUserId(userId);
            double availableMoney = await AvailableMoneyOfInstructor(userId);
            if (availableMoney < withdrawMoney) return Result.Failure(WalletError.OverBalance);
            financialTransactions.Withdrawal += withdrawMoney;
            await _financialTransactionsRepository.UpdateFinancialTransactions(financialTransactions);
            wallet.Amount += withdrawMoney;
            wallet.UpdatedDate = DateTime.UtcNow;
            await _walletRepository.UpdateWallet(wallet);
            await _systemTransactionRepository.AddSystemTransaction(userId, wallet.WlId, withdrawMoney, Status.Withdraw);
            string result = $"Withdraw {withdrawMoney} sucessfully";
            return Result.SuccessWithObject(result);
        }
        //Test withdraw

        private async Task<double> AvailableMoneyOfInstructor(string userId)
        {
            if (!await _financialTransactionsRepository.CheckFinancialTransactions(userId))
                await _financialTransactionsRepository.AddFinancialTransactions(userId);
            FinancialTransactions financialTransactions = await _financialTransactionsRepository.GetFinancialTransactions(userId);
            string instructorId = await _instructorRepository.GetInstructorIdByUserId(userId);
            List<string> courseList = await _courseRepository.GetCourseIdByInstructorId(instructorId);
            List<int> courseVersionList = new List<int>();
            foreach (var course in courseList)
            {
                List<int> a = await _courseVersionRepository.GetCourseVersionByCourseId(course);
                courseVersionList.AddRange(a);
            }
            double availableMoney = 0;
            foreach (var cv in courseVersionList)
            {
                availableMoney += await _courseVersionDetailRepository.GetPriceByCourseVersionId(cv);
            }
            availableMoney -= financialTransactions.Withdrawal;
            availableMoney = Math.Round(availableMoney, 2);
            return availableMoney;
        }
        #endregion

        public async Task<bool> RegisterUserWalletlAsync(string UserId)
        {
            var walletId = await _walletRepository.AutoGenerateWalletID();
            var wallet = new Wallet
            {
                WlId = walletId,
                UserId = UserId,
                Amount = 0,
                CreatedDate = DateTime.Now,
                CreatedBy = "",
                UpdatedDate = DateTime.Now,
                UpdatedBy = ""
            };
            if (wallet != null)
            {
                await _walletRepository.AddUserWalletAsync(wallet);
                return true;
            }
            return false;
        }

        public async Task<dynamic> DepositMoneyToWallet(string userId, double depositMoney)
        {
            if (!await _walletRepository.CheckWallet(userId)) await RegisterUserWalletlAsync(userId);

            Wallet wallet = await _walletRepository.GetWalletByUserId(userId);
            if (depositMoney < minDepositMoney)
            {
                return Result.Failure(new Error("Wallet", "Deposit money must larger than 10000"));
            }
            wallet.Amount += depositMoney;
            wallet.UpdatedDate = DateTime.Now;
            await _walletRepository.UpdateWallet(wallet);
            await _systemTransactionRepository.AddSystemTransaction(userId, wallet.WlId, depositMoney, Status.Deposit);
            string result = $"Deposit {depositMoney} sucessfully";
            return Result.SuccessWithObject(result);
        }
    }

}
