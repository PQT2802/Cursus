using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.Entities
{
    [Table("Bank")]
    public class Bank
    {
        [Key]
        public string BankId { get; set; }
        public string BankName { get; set; }
    }
}
