using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.DTOs.UI.UIAdmin
{
    public class UICourseListDetailAdminDTO
    {
        public int NumberOfStudent { get; set; }
        public double TotalPurchasedMoney { get; set; }
        public double Price { get; set; }
        public decimal Rating { get; set; }
    }
}
