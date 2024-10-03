using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.DTOs
{
    public class CurrentUserObject
    {
        public string UserId {  get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string Phone { get; set; }

    }
}
