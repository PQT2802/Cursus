using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.DTOs
{
    public class SearchDTO
    {
        public string search {  get; set; }
        public string filter { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
}
