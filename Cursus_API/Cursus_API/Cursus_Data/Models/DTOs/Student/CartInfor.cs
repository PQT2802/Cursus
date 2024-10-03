using Cursus_Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.DTOs.Student
{
    public class CartInfor
    {
        public Guid CartId { get; set; }
        public string UserId { get; set; }
        public DateTime UpdateDate { get; set; }
        public List<CartItemInfo> CartItems { get; set; }
    }

    public class CartItemInfo
    {
        public Guid CartItemId { get; set; }
        public string CourseId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
