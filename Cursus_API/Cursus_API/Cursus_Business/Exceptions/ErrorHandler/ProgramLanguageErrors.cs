using Cursus_Data.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Cursus_Business.Exceptions.ErrorHandler
{
    public static class ProgramLanguageErrors
    {
        public static Error NameIsEmpty() => new(
            "Name", "Name must not be empty.");
        public static Error NameTooLong(int maxLength) => new(
            "Name", $"Name must not exceed {maxLength} characters.");
        public static Error ParentIdNotExist() => new(
            "ParentId", "ParentId does not exist.");
        public static Error ProgramLanguageIdNotExist() => new(
            "ProgramLanguageID", "ProgramLanguage ID does not exist.");
        public static Error NameDuplicated() => new(
            "Name", $"This program language is already exists");
    }
}
