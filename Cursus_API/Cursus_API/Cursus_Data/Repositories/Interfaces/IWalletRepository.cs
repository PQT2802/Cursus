using Cursus_Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Repositories.Interfaces
{
    public interface IWalletRepository
    {
        Task AddUserWalletAsync(Wallet wallet);
        Task<string> AutoGenerateWalletID();
        Task<bool> CheckWallet(string userId);
        Task<Wallet> GetWalletByUserId(string userId);  
        Task UpdateWallet(Wallet wallet);
    }
}
