using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Repositories.Interfaces
{
    public interface ISystemTransactionRepository
    {
        Task AddSystemTransaction(string fromId, string toId, double amount, string type);
    }
}
