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
    public class PaymentRepository : IPaymentRepository
    {
        private readonly LMS_CursusDbContext _context;
        public PaymentRepository(LMS_CursusDbContext context)
        {
            _context = context;
        }

        public async Task CreatePayment(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
             _context.SaveChangesAsync();
                
        }
    }
}
