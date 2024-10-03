using Cursus_Data.Context;
using Cursus_Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using Cursus_Data.Models.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Cursus_Data.Repositories.Implements
{
    public class WalletRepository : IWalletRepository
    {
        private readonly LMS_CursusDbContext _context;

        public WalletRepository(LMS_CursusDbContext context)
        {
            _context = context;
        }

        public async Task AddUserWalletAsync(Wallet wallet)
        {
            await _context.Wallets.AddAsync(wallet);
            await _context.SaveChangesAsync();
        }

        public async Task<string> AutoGenerateWalletID()
        {
            int count = await _context.Wallets.CountAsync() + 1;
            string Wl = "WL";
            string paddedNumber = count.ToString().PadLeft(8, '0');
            string WalletId = Wl + paddedNumber;
            return WalletId;
        }

        public async Task<bool> CheckWallet(string userId)
        {
            return await _context.Wallets.AnyAsync(x => x.UserId == userId);
        }

        public async Task<Wallet> GetWalletByUserId(string userId)
        {
           return await _context.Wallets.Where(x => x.UserId == userId).FirstOrDefaultAsync();   
        }

        public async Task UpdateWallet(Wallet wallet)
        {
            _context.Wallets.Update(wallet);
            await _context.SaveChangesAsync();
        }
    }
}
