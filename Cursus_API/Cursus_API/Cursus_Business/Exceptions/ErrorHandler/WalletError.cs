using Cursus_Data.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Exceptions.ErrorHandler
{
    public class WalletError
    {
        public static Error OverBalance => new Error("Withdraw money", $"Amount exceeding available balance");
    }
}
