using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.DTOs
{
    public class CategoryUIDTO
    {
        public string CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ParentId { get; set; }
        public string? ParentName { get; set; } // This is to be filled with parent name if needed
        public string Status { get; set; }
    }
}
