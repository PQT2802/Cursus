using Cursus_Data.Context;
using Cursus_Data.Models.Entities;
using Cursus_Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Repositories.Implements
{
    public class SystemTransactionRepository : ISystemTransactionRepository
    {
        private readonly LMS_CursusDbContext _context;
        public SystemTransactionRepository(LMS_CursusDbContext context)
        {
            _context = context;
        }
        public async Task AddSystemTransaction(string fromId, string toId, double amount, string type)
        {
             
            SystemTransaction systemTransaction = new SystemTransaction()
            {
                StId = Guid.NewGuid(),
                FromId = fromId,
                ToId = toId,
                Amount = amount,
                Type = type,
                CreateDate = DateTime.Now,
                CreateBy = fromId,
                UpdateDate = DateTime.Now,
                UpdateBy = fromId
            };
            _context.SystemTransactions.Add(systemTransaction);
            _context.SaveChanges();
        }
    }
}
