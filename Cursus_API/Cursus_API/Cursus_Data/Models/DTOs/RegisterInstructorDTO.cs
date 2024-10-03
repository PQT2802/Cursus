using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.DTOs
{
    public class RegisterInstructorDTO : RegisterUserDTO
    {
        public string? TaxNumber { get; set; }
        public string? CardNumber { get; set; }
        public string? CardName { get; set; }
        public string? CardProvider { get; set; }
        public IFormFile? Certification { get; set; }
    }
}
