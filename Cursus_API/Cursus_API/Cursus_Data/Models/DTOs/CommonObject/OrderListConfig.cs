using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.DTOs.CommonObject
{
    public class OrderListConfig
    {
        public int? PageSize { get; set; } = 20;

        public int? PageIndex { get; set; } = 1;

        public SortDirection? SortDirection { get; set; }

        public OrderStatus? Status { get; set; }
    }
    public enum OrderStatus
    {
        Pending,
        Paid
    }
}
