using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.Entities
{
    [Table("Instructor")]
    public class Instructor
    {
        [Key]
        public string InstructorId { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public string TaxNumber { get; set; }
        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public string CardProvider { get; set; }
        public bool IsAccepted { get; set; }
        public string Certification { get; set; }

        // Navigation property
        public virtual ICollection<Course> Courses { get; set; }

    }

}
