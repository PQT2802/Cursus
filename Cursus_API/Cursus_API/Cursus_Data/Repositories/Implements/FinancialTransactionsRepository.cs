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
    public class FinancialTransactionsRepository : IFinancialTransactionsRepository
    {
        private readonly LMS_CursusDbContext _context;

        public FinancialTransactionsRepository(LMS_CursusDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CheckFinancialTransactions(string userId)
        {
            return await _context.FinancialTransactions.AnyAsync(x => x.UserID == userId);
        }

        public async Task AddFinancialTransactions(string userId)
        {
            FinancialTransactions financialTransactions = new FinancialTransactions();
            financialTransactions.UserID = userId;
            _context.FinancialTransactions.Add(financialTransactions);
            await _context.SaveChangesAsync();
        }

        public async Task<FinancialTransactions> GetFinancialTransactions(string userId)
        {
            return await _context.FinancialTransactions.FirstOrDefaultAsync(x => x.UserID == userId);
        }

        public async Task UpdateFinancialTransactions(FinancialTransactions financialTransactions)
        {
            _context.FinancialTransactions.Update(financialTransactions);
            await _context.SaveChangesAsync();
        }
    }
}
