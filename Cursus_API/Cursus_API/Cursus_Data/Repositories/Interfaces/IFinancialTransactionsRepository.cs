using Cursus_Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Repositories.Interfaces
{
    public interface IFinancialTransactionsRepository
    {
        Task<bool> CheckFinancialTransactions(string userId);
        Task AddFinancialTransactions(string userId);
        Task<FinancialTransactions> GetFinancialTransactions(string userId);
        Task UpdateFinancialTransactions(FinancialTransactions financialTransactions);
    }
}
