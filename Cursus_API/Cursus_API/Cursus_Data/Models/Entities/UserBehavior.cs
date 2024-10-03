using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.Entities
{
    [Table("UserBehavior")]
    public class UserBehavior
    {
        [Key]
        public int UserBehaviorId { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public string? Key1 { get; set; }
        public DateTime? Date1 { get; set; }
        public string? Key2 { get; set; }
        public DateTime? Date2 { get; set; }
        public string? Key3 {  get; set; }
        public DateTime? Date3 { get; set; }
    }
}
