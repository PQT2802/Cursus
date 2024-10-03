using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.Entities
{
    [Table("Transaction")]
    public class Transaction
    {
        [Key]
        public Guid TrId { get; set; }

        [Required]
        public Guid PaymentId { get; set; }
        public virtual Payment Payment { get; set; }

        [Required]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public double Amount { get; set; }
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }

}
