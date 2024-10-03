using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.Entities
{
    [Table("Wallet")]
    public class Wallet
    {
        [Key]
        public string WlId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public double Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        // Navigation property
        public virtual User User { get; set; }
    }

}
