using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.DTOs
{
    public class ProgramLanguageDTO
    {
        public string ProgramLanguageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ParentId { get; set; }
    }
}
