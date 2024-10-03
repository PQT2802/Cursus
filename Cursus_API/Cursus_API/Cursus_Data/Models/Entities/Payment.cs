using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.Entities
{
    [Table("Payment")]
    public class Payment
    {
        [Key]
        public Guid PaymentId { get; set; }

        [Required]
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }

        public double Amount { get; set; }

        [Required]
        public string Code { get; set; }

        public DateTime TransactionDate { get; set; }

        //[Required]
        //public string UserBankName { get; set; }

        //[Required]
        //public string UserBankAccount { get; set; }

        public Guid TransactionId { get; set; }
        public virtual Transaction Transaction { get; set; }
    }

}
