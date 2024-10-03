using Cursus_Data.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Exceptions.ErrorHandler
{
    public static class SignInErrors
    {
        public static Error InputFieldWrong() => new(
      "Input", $"Wrong email or password!!");

        public static Error InputEmpty() => new(
       "Input", $"You have to enter your email and password!!");
        public static Error AccountIsDelete() => new(
            "Account", $"Your account is deleted!!");
        public static Error AccountIsBan() => new(
            "Account", $"Your account is banned!!");

    }
}
