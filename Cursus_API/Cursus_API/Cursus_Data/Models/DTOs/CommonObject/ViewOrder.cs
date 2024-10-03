using Cursus_Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.DTOs.CommonObject
{
    public class ViewOrder
    {
        public Guid OrderId { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; }
        public double TotalAmount { get; set; }
        public List<ViewOrderItem> ViewOrderItems { get; set; }
    }

    public class ViewOrderItem
    {
        public Guid OrderItemId { get; set; }
        public string CourseId { get; set; }
        public double Amount { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
