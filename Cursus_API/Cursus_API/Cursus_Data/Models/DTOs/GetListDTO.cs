﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.DTOs
{
    public class GetListDTO
    {
        public string? searchBy { get; set; } 
        public string? search {  get; set; } 
        public string? sortBy {  get; set; } 
        public string? sort { get; set; }
        public int pageIndex {  get; set; }
        public int pageSize { get; set; }   
    }
}
