using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Common
{
    public static class Status
    {
        public static string New { get; set; } = "New";
        public static string Activate { get; set; } = "Activate";
        public static string Deactivate { get; set; } = "Deactivate";
        public static string InProcess { get; set; } = "InProcess";
        public static string Closed { get; set; } = "Closed";
        public static string Submitted { get; set; } = "Submitted";
        public static string Pending { get; set; } = "Pending";//ko đc sửa
        public static string Withdraw { get; set; } = "Withdraw";
        public static string Deposit { get; set; } = "Deposit";
        public static string Purchased { get; set; } = "Purchased";
        public static string Enrolled { get; set; } = "Enrolled";

    }
}
