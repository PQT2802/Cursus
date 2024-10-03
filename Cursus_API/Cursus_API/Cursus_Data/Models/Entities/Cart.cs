using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.Entities
{
    [Table("Cart")]
    public class Cart
    {
        [Key]
        public Guid CartId { get; set; }

        [Required]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
