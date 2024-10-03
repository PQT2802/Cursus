using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.Entities
{
    [Table("SystemTransaction")]
    public class SystemTransaction
    {
        [Key]
        public Guid StId { get; set; }
        public string FromId { get; set; }
        public string ToId { get; set; }
        public double Amount { get; set; }
        public string Type { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UpdateBy { get; set; }
    }

}
