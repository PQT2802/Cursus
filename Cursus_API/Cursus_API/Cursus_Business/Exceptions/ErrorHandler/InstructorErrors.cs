using Cursus_Data.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Exceptions.ErrorHandler
{
    public static class InstructorErrors
    {
        public static Error TaxNumberIsEmpty() => new(
     "TaxNumber", $"Tax number must not be empty!!");
        public static Error CardNumberIsEmpty() => new(
     "CardNumber", $"Card number must not be empty!!");
        public static Error CardNameIsEmpty() => new(
     "CardName", $"Card name must not be empty!!");
        public static Error CardProviderIsEmpty() => new(
     "CardProvider", $"Card provider must not be empty!!");
        public static Error CertificationIsEmpty() =>new(
     "Certification", "Certification is empty.") ;
        public static Error CertificationInvalidType() => new(
     "Certification", "Certification must be a PDF, PNG, or JPEG file.");
        public static Error CertificationTooLarge() => new(
     "Certification", "Certification file size must not exceed 5MB.");
        public static Error InstructorIdNotExist => new(
     "InstructorID", "Instructor Id Not Exist");
       
    }
}
