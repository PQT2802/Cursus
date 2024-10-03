using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cursus_Data.Models.DTOs;

namespace Cursus_Business.Exceptions.ErrorHandler
{
    public static class UserErrors
    {
        public static Error EmailAlreadyUsed(string email) => new(
     "Email", $"The email:'{email}' already used!!");
        public static Error PhoneAlreadyUsed(string phone) => new(
     "Phone", $"The phone number:'{phone}' already used!!");
        public static Error FullnameAlreadyExist(string fullname) => new(
       "Fullname", $"The user with FullName '{fullname}' already exist!!");
        //email
        public static Error EmailIsEmpty => new Error("Email", $"Email should not be empty!");
        public static Error EmailIsInvalid(string email) => new Error("Email", $"Email {email} is invalid");
        //password
        public static Error PasswordIsEmpty => new Error("Password", $"Password should not be empty!");
        public static Error PasswordMinLength => new Error("Password", $"Password must be at least 6 characters long");
        public static Error PasswordIsInvalid => new Error("Password", $"Password must have at least one uppercase letter, one special character and be at least 6 characters long");
        public static Error PasswordNotMatched => new Error("Password", $"Old password is not matched");
        //name
        public static Error FullnameIsEmpty => new Error("Fullname", $"Full name should not be empty!");
        public static Error FullnameIsInvalid(string name) => new Error("Fullname", $"{name} is invalid name!");
        //DOB
        public static Error DOBIsEmpty => new Error("DOB", $"Date Of Birth should not be empty!");
        public static Error DOBIsInvalid => new Error("DOB", $"Date Of Birth is invalid!");
        public static Error DOBIsNotEnogh => new Error("DOB", $"Too young to study :(");
        //phone
        public static Error PhoneIsEmpty => new Error("Phone", $"Phone should not be empty!");
        public static Error PhoneIsInvalid => new Error("Phone", $"Invalid Phone number format!");
        public static Error PhoneLength => new Error("Phone", $"Phone must be 9 characters long!");
        public static Error PhoneIsInvalidVNFormat(string phone) => new Error("Phone", $"{phone} is invalid VN phone number format!");
        //address
        public static Error AddressIsEmpty => new Error("Address", $"Address should not be empty!");
        public static Error UserIsNotExist => new Error("User", $"User is not exist!");

    }
}
