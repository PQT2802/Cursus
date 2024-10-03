using Cursus_Data.Models.DTOs.CommonObject;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.Entities
{
    [Table("Order")]
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public DateTime OrderDate { get; set; }

        public string Status { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        [NotMapped]
        public double TotalAmount => OrderItems.Where(item => !item.IsDeleted).Sum(item => item.Amount);
    }

}
