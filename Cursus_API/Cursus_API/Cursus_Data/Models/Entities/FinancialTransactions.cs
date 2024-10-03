using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.Entities
{
    [Table("FinancialTransactions")]
    public class FinancialTransactions
    {
        [Key]
        public int FTID { get; set; }
        public string UserID { get; set; }
        public double Deposit { get; set; } = 0;
        public double Withdraw { get; set; } = 0;
        public double Withdrawal { get; set; } = 0;
    }
}
